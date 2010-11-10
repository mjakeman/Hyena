using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Hyena.Data.Sqlite
{
    public class Connection : IDisposable
    {
        IntPtr ptr;
        internal IntPtr Ptr { get { return ptr; } }

        public string DbPath { get; private set; }

        public long LastInsertRowId {
            get { return Native.sqlite3_last_insert_rowid (Ptr); }
        }

        public Connection (string dbPath)
        {
            DbPath = dbPath;
            CheckError (Native.sqlite3_open (Encoding.UTF8.GetBytes (dbPath), out ptr));
            if (ptr == IntPtr.Zero)
                throw new Exception ("Unable to open connection");
        }

        public void Dispose ()
        {
            if (ptr != IntPtr.Zero) {
                CheckError (Native.sqlite3_close (ptr));
                ptr = IntPtr.Zero;
            }
        }

        ~Connection ()
        {
            Dispose ();
        }

        internal void CheckError (int errorCode)
        {
            CheckError (errorCode, "");
        }

        internal void CheckError (int errorCode, string msg)
        {
            if (errorCode == 0 || errorCode == 100 || errorCode == 101)
                return;

            throw new Exception (msg + Native.sqlite3_errmsg16 (Ptr).PtrToString ());
        }

        public Statement CreateStatement (string sql)
        {
            return new Statement (this, sql);
        }

        public QueryReader Query (string sql)
        {
            return new Statement (this, sql) { ReaderDisposes = true }.Query ();
        }

        public object QueryScalar (string sql)
        {
            using (var stmt = new Statement (this, sql)) {
                return stmt.QueryScalar ();
            }
        }

        public void Execute (string sql)
        {
            using (var stmt = new Statement (this, sql)) {
                stmt.Execute ();
            }
        }

        const int UTF16 = 4;
        public void AddFunction<T> () where T : SqliteFunction
        {
            var type = typeof (T);
            var pr = (SqliteFunctionAttribute)type.GetCustomAttributes (typeof (SqliteFunctionAttribute), false).First ();
            var f = (SqliteFunction) Activator.CreateInstance (typeof (T));

            f._InvokeFunc = (pr.FuncType == FunctionType.Scalar) ? new SqliteCallback(f.ScalarCallback) : null;
            f._StepFunc = (pr.FuncType == FunctionType.Aggregate) ? new SqliteCallback(f.StepCallback) : null;
            f._FinalFunc = (pr.FuncType == FunctionType.Aggregate) ? new SqliteFinalCallback(f.FinalCallback) : null;
            f._CompareFunc = (pr.FuncType == FunctionType.Collation) ? new SqliteCollation(f.CompareCallback) : null;

            if (pr.FuncType != FunctionType.Collation) {
                CheckError (Native.sqlite3_create_function16 (
                    ptr, pr.Name, pr.Arguments, UTF16, IntPtr.Zero,
                    f._InvokeFunc, f._StepFunc, f._FinalFunc
                ));
            } else {
                CheckError (Native.sqlite3_create_collation16 (
                    ptr, pr.Name, UTF16, IntPtr.Zero, f._CompareFunc
                ));
            }
        }

        public void RemoveFunction<T> () where T : SqliteFunction
        {
            var type = typeof (T);
            var pr = (SqliteFunctionAttribute)type.GetCustomAttributes (typeof (SqliteFunctionAttribute), false).First ();
            if (pr.FuncType != FunctionType.Collation) {
                CheckError (Native.sqlite3_create_function16 (
                    ptr, pr.Name, pr.Arguments, UTF16, IntPtr.Zero,
                    null, null, null
                ));
            } else {
                CheckError (Native.sqlite3_create_collation16 (
                    ptr, pr.Name, UTF16, IntPtr.Zero, null
                ));
            }
        }
    }

    public interface IDataReader : IDisposable
    {
        bool Read ();
        object this[int i] { get; }
        object this[string columnName] { get; }
        int FieldCount { get; }
    }

    public class Statement : IDisposable, IEnumerable<IDataReader>
    {
        IntPtr ptr;
        Connection connection;
        bool bound;
        QueryReader reader;

        internal IntPtr Ptr { get { return ptr; } }
        internal bool Bound { get { return bound; } }
        internal Connection Connection { get { return connection; } }

        public string CommandText { get; private set; }
        public int ParameterCount { get; private set; }
        public bool ReaderDisposes { get; set; }

        internal event EventHandler Disposed;

        internal Statement (Connection connection, string sql)
        {
            CommandText = sql;
            this.connection = connection;

            IntPtr pzTail = IntPtr.Zero;
            connection.CheckError (Native.sqlite3_prepare16_v2 (connection.Ptr, sql, sql.Length * 2, out ptr, out pzTail), sql);
            if (pzTail != IntPtr.Zero && Marshal.ReadByte (pzTail) != 0) {
                Dispose ();
                throw new ArgumentException ("sql", String.Format ("This sqlite binding does not support multiple commands in one statement:\n  {0}", sql));
            }

            ParameterCount = Native.sqlite3_bind_parameter_count (ptr);
            reader = new QueryReader () { Statement = this };
        }

        public void Dispose ()
        {
            if (ptr != IntPtr.Zero) {
                connection.CheckError (Native.sqlite3_finalize (ptr));
                ptr = IntPtr.Zero;

                var h = Disposed;
                if (h != null) {
                    h (this, EventArgs.Empty);
                }
            }
        }

        ~Statement ()
        {
            Dispose ();
        }

        public Statement Bind (params object [] vals)
        {
            if (vals == null || vals.Length != ParameterCount || ParameterCount == 0)
                throw new ArgumentException ("vals", String.Format ("Statement has {0} parameters", ParameterCount));

            connection.CheckError (Native.sqlite3_reset (ptr));

            for (int i = 1; i <= vals.Length; i++) {
                int code = 0;
                object o = SqliteUtils.ToDbFormat (vals[i - 1]);

                if (o == null)
                    code = Native.sqlite3_bind_null (Ptr, i);
                else if (o is double || o is float)
                    code = Native.sqlite3_bind_double (Ptr, i, (double)o);
                else if (o is int || o is uint)
                    code = Native.sqlite3_bind_int (Ptr, i, (int)o);
                else if (o is long || o is ulong)
                    code = Native.sqlite3_bind_int64 (Ptr, i, (long)o);
                else if (o is byte[]) {
                    byte [] bytes = o as byte[];
                    code = Native.sqlite3_bind_blob (Ptr, i, bytes, bytes.Length, (IntPtr)(-1));
                } else {
                    // C# strings are UTF-16, so 2 bytes per char
                    // -1 for the last arg is the TRANSIENT destructor type so that sqlite will make its own copy of the string
                    string str = o.ToString ();
                    code = Native.sqlite3_bind_text16 (Ptr, i, str, str.Length * 2, (IntPtr)(-1));
                }

                connection.CheckError (code);
            }

            bound = true;
            return this;
        }

        public IEnumerator<IDataReader> GetEnumerator ()
        {
            connection.CheckError (Native.sqlite3_reset (ptr));
            while (reader.Read ()) {
                yield return reader;
            }
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

        public Statement Execute ()
        {
            reader.Read ();
            return this;
        }

        public object QueryScalar ()
        {
            return reader.Read () ? reader[0] : null;
        }

        public QueryReader Query ()
        {
            return reader;
        }
    }

    public class QueryReader : IDisposable, IDataReader
    {
        Dictionary<string, int> columns;
        int column_count = -1;

        internal Statement Statement { get; set; }
        IntPtr Ptr { get { return Statement.Ptr; } }

        public void Dispose ()
        {
            if (Statement.ReaderDisposes) {
                Statement.Dispose ();
            }
        }

        public int FieldCount {
            get {
                if (column_count == -1) {
                    column_count = Native.sqlite3_column_count (Ptr);
                }
                return column_count;
            }
        }

        public bool Read ()
        {
            if (Statement.ParameterCount > 0 && !Statement.Bound)
                throw new InvalidOperationException ("Statement not bound");

            int code = Native.sqlite3_step (Ptr);
            if (code == ROW) {
                return true;
            } else {
                Statement.Connection.CheckError (code);
                return false;
            }
        }

        public object this[int i] {
            get {
                int type = Native.sqlite3_column_type (Ptr, i);
                switch (type) {
                    case SQLITE_INTEGER:
                        return Native.sqlite3_column_int64 (Ptr, i);
                    case SQLITE_FLOAT:
                        return Native.sqlite3_column_double (Ptr, i);
                    case SQLITE3_TEXT:
                        return Native.sqlite3_column_text16 (Ptr, i).PtrToString ();
                    case SQLITE_BLOB:
                        return Native.sqlite3_column_blob (Ptr, i);
                    case SQLITE_NULL:
                        return null;
                    default:
                        throw new Exception (String.Format ("Column is of unknown type {0}", type));
                }
            }
        }

        public object this[string columnName] {
            get {
                if (columns == null) {
                    columns = new Dictionary<string, int> ();
                    for (int i = 0; i < FieldCount; i++) {
                        columns[Native.sqlite3_column_name16 (Ptr, i).PtrToString ()] = i;
                    }
                }

                int col = 0;
                if (!columns.TryGetValue (columnName, out col))
                    throw new ArgumentException ("columnName");

                return this[col];
            }
        }

        const int SQLITE_INTEGER = 1;
        const int SQLITE_FLOAT   = 2;
        const int SQLITE3_TEXT   = 3;
        const int SQLITE_BLOB    = 4;
        const int SQLITE_NULL    = 5;

        const int ROW = 100;
        const int DONE = 101;
    }

    internal static class Native
    {
        const string SQLITE_DLL = "sqlite3";

        // Connection functions
        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_open(byte [] utf8DbPath, out IntPtr db);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_close(IntPtr db);

        [DllImport(SQLITE_DLL)]
        internal static extern long sqlite3_last_insert_rowid (IntPtr db);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_busy_timeout(IntPtr db, int ms);

        [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
        internal static extern IntPtr sqlite3_errmsg16(IntPtr db);

        [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
        internal static extern int sqlite3_create_function16(IntPtr db, string strName, int nArgs, int eTextRep, IntPtr app, SqliteCallback func, SqliteCallback funcstep, SqliteFinalCallback funcfinal);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_aggregate_count(IntPtr context);

        [DllImport(SQLITE_DLL)]
        internal static extern IntPtr sqlite3_aggregate_context(IntPtr context, int nBytes);

        [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
        internal static extern int sqlite3_create_collation16(IntPtr db, string strName, int eTextRep, IntPtr ctx, SqliteCollation fcompare);

        // Statement functions
        [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
        internal static extern int sqlite3_prepare16_v2(IntPtr db, string pSql, int nBytes, out IntPtr stmt, out IntPtr ptrRemain);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_step(IntPtr stmt);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_column_count(IntPtr stmt);

        [DllImport(SQLITE_DLL)]
        internal static extern IntPtr sqlite3_column_name16(IntPtr stmt, int index);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_column_type(IntPtr stmt, int iCol);

        [DllImport(SQLITE_DLL)]
        internal static extern byte [] sqlite3_column_blob(IntPtr stmt, int iCol);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_column_bytes(IntPtr stmt, int iCol);

        [DllImport(SQLITE_DLL)]
        internal static extern double sqlite3_column_double(IntPtr stmt, int iCol);

        [DllImport(SQLITE_DLL)]
        internal static extern long sqlite3_column_int64(IntPtr stmt, int iCol);

        [DllImport(SQLITE_DLL)]
        internal static extern IntPtr sqlite3_column_text16(IntPtr stmt, int iCol);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_finalize(IntPtr stmt);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_reset(IntPtr stmt);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_bind_parameter_index(IntPtr stmt, byte [] paramName);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_bind_parameter_count(IntPtr stmt);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_bind_blob(IntPtr stmt, int param, byte[] val, int nBytes, IntPtr destructorType);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_bind_double(IntPtr stmt, int param, double val);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_bind_int(IntPtr stmt, int param, int val);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_bind_int64(IntPtr stmt, int param, long val);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_bind_null(IntPtr stmt, int param);

        [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
        internal static extern int sqlite3_bind_text16 (IntPtr stmt, int param, string val, int numBytes, IntPtr destructorType);

        //DllImport(SQLITE_DLL)]
        //internal static extern int sqlite3_bind_zeroblob(IntPtr stmt, int, int n);

        // Context functions
        [DllImport(SQLITE_DLL)]
        internal static extern void sqlite3_result_blob(IntPtr context, byte[] value, int nSize, IntPtr pvReserved);

        [DllImport(SQLITE_DLL)]
        internal static extern void sqlite3_result_double(IntPtr context, double value);

        [DllImport(SQLITE_DLL)]
        internal static extern void sqlite3_result_error(IntPtr context, byte[] strErr, int nLen);

        [DllImport(SQLITE_DLL)]
        internal static extern void sqlite3_result_int(IntPtr context, int value);

        [DllImport(SQLITE_DLL)]
        internal static extern void sqlite3_result_int64(IntPtr context, Int64 value);

        [DllImport(SQLITE_DLL)]
        internal static extern void sqlite3_result_null(IntPtr context);

        [DllImport(SQLITE_DLL)]
        internal static extern void sqlite3_result_text(IntPtr context, byte[] value, int nLen, IntPtr pvReserved);

        [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
        internal static extern void sqlite3_result_error16(IntPtr context, string strName, int nLen);

        [DllImport(SQLITE_DLL, CharSet = CharSet.Unicode)]
        internal static extern void sqlite3_result_text16(IntPtr context, string strName, int nLen, IntPtr pvReserved);

        // Value methods
        [DllImport(SQLITE_DLL)]
        internal static extern IntPtr sqlite3_value_blob(IntPtr p);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_value_bytes(IntPtr p);

        [DllImport(SQLITE_DLL)]
        internal static extern double sqlite3_value_double(IntPtr p);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_value_int(IntPtr p);

        [DllImport(SQLITE_DLL)]
        internal static extern Int64 sqlite3_value_int64(IntPtr p);

        [DllImport(SQLITE_DLL)]
        internal static extern int sqlite3_value_type(IntPtr p);

        [DllImport(SQLITE_DLL)]
        internal static extern IntPtr sqlite3_value_text16(IntPtr p);

        internal static string PtrToString (this IntPtr ptr)
        {
            return System.Runtime.InteropServices.Marshal.PtrToStringUni (ptr);
        }
    }
}
