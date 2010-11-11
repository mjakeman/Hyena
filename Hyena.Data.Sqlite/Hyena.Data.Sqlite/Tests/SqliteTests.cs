//
// SqliteTests.cs
//
// Author:
//   Gabriel Burt <gburt@novell.com>
//
// Copyright (C) 2010 Novell, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#if ENABLE_TESTS

using System;
using System.Linq;

using NUnit.Framework;
using Hyena.Data.Sqlite;

namespace Hyena.Data.Sqlite
{
    [TestFixture]
    public class SqliteTests
    {
        Connection con;
        string dbfile = "hyena-data-sqlite-test.db";

        [SetUp]
        public void Setup ()
        {
            con = new Connection (dbfile);
        }

        [TearDown]
        public void TearDown ()
        {
            Assert.AreEqual (0, con.Statements.Count);
            con.Dispose ();
            System.IO.File.Delete (dbfile);
        }

        [Test]
        public void Test ()
        {
            using (var stmt = con.CreateStatement ("SELECT 'foobar' as version")) {
                Assert.AreEqual ("foobar", stmt.QueryScalar ());
                Assert.AreEqual ("foobar", stmt.First ()[0]);
                Assert.AreEqual ("foobar", stmt.First ()["version"]);
            }

            using (var stmt = con.CreateStatement ("SELECT 2 + 5 as res")) {
                Assert.AreEqual (7, stmt.QueryScalar ());
                Assert.AreEqual (7, stmt.First ()[0]);
                Assert.AreEqual (7, stmt.First ()["res"]);

                try {
                    stmt.Bind ();
                    Assert.Fail ("should not be able to bind parameterless statement");
                } catch {}
            }
        }

        [Test]
        public void TestBinding ()
        {
            using (var stmt = con.CreateStatement ("SELECT ? as version")) {
                try {
                    stmt.First ();
                    Assert.Fail ("unbound statement should have thrown an exception");
                } catch {}

                try {
                    stmt.Bind (1, 2);
                    Assert.Fail ("bound statement with the wrong number of parameters");
                } catch {}

                try {
                    stmt.Bind ();
                    Assert.Fail ("bound statement with the wrong number of parameters");
                } catch {}

                stmt.Bind (21);
                Assert.AreEqual (21, stmt.First ()[0]);
                Assert.AreEqual (21, stmt.First ()["version"]);

                stmt.Bind ("ffoooo");
                Assert.AreEqual ("ffoooo", stmt.First ()[0]);
                Assert.AreEqual ("ffoooo", stmt.First ()["version"]);
            }

            using (var stmt = con.CreateStatement ("SELECT ? as a, ? as b, ?")) {
                stmt.Bind (1, "two", 3.3);
                Assert.AreEqual (1, stmt.QueryScalar ());
                Assert.AreEqual ("two", stmt.First ()["b"]);
                Assert.AreEqual (3.3, stmt.First ()[2]);
            }
        }

        [Test]
        public void CreateTable ()
        {
            CreateUsers (con);

            using (var stmt = con.CreateStatement ("SELECT COUNT(*) FROM Users")) {
                Assert.AreEqual (2, stmt.QueryScalar ());
            }

            using (var stmt = con.CreateStatement ("SELECT ID, Name FROM Users ORDER BY NAME")) {
                var row1 = stmt.First ();
                Assert.AreEqual ("Aaron", row1["Name"]);
                Assert.AreEqual ("Aaron", row1[1]);
                Assert.AreEqual (2, row1["ID"]);
                Assert.AreEqual (2, row1[0]);

                var row2 = stmt.Skip (1).First ();
                Assert.AreEqual ("Gabriel", row2["Name"]);
                Assert.AreEqual ("Gabriel", row2[1]);
                Assert.AreEqual (1, row2["ID"]);
                Assert.AreEqual (1, row2[0]);
            }
        }

        private void CreateUsers (Connection con)
        {
            using (var stmt = con.CreateStatement ("DROP TABLE IF EXISTS Users")) {
                stmt.Execute ();
            }

            using (var stmt = con.CreateStatement ("CREATE TABLE Users (ID INTEGER PRIMARY KEY, Name TEXT)")) {
                stmt.Execute ();
                try {
                    stmt.Execute ();
                    Assert.Fail ("Shouldn't be able to create table; already exists");
                } catch {}
            }

            using (var stmt = con.CreateStatement ("INSERT INTO Users (Name) VALUES (?)")) {
                stmt.Bind ("Gabriel").Execute ();
                stmt.Bind ("Aaron").Execute ();
            }
        }

        [Test]
        public void CheckInterleavedAccess ()
        {
            CreateUsers (con);

            var q1 = con.Query ("SELECT ID, Name FROM Users ORDER BY NAME");
            var q2 = con.Query ("SELECT ID, Name FROM Users ORDER BY ID");

            Assert.IsTrue (q1.Read ());
            Assert.IsTrue (q2.Read ());
            Assert.AreEqual ("Aaron", q1["Name"]);
            Assert.AreEqual ("Gabriel", q2["Name"]);

            Assert.IsTrue (q2.Read ());
            Assert.AreEqual ("Aaron", q2["Name"]);
            Assert.IsTrue (q1.Read ());
            Assert.AreEqual ("Gabriel", q1["Name"]);

            q1.Dispose ();
            q2.Dispose ();
        }

        [Test]
        public void ConnectionDisposesStatements ()
        {
            var stmt = con.CreateStatement ("SELECT 1");
            Assert.IsFalse (stmt.IsDisposed);
            con.Dispose ();
            Assert.IsTrue (stmt.IsDisposed);
        }

        [Test]
        public void MultipleCommands ()
        {
            try {
                using (var stmt = con.CreateStatement ("CREATE TABLE Lusers (ID INTEGER PRIMARY KEY, Name TEXT); INSERT INTO Lusers (Name) VALUES ('Foo')")) {
                    stmt.Execute ();
                }
                Assert.Fail ("Mutliple commands aren't supported in this sqlite binding");
            } catch {}
        }

        [Test]
        public void Query ()
        {
            using (var q = con.Query ("SELECT 7")) {
                int rows = 0;
                while (q.Read ()) {
                    Assert.AreEqual (7, q[0]);
                    rows++;
                }
                Assert.AreEqual (1, rows);
            }
        }

        [Test]
        public void QueryScalar ()
        {
            Assert.AreEqual (7, con.QueryScalar ("SELECT 7"));
        }

        [Test]
        public void Execute ()
        {
            try {
                con.QueryScalar ("SELECT COUNT(*) FROM Users");
                Assert.Fail ("Should have thrown an exception");
            } catch {}
            con.Execute ("CREATE TABLE Users (ID INTEGER PRIMARY KEY, Name TEXT)");
            Assert.AreEqual (0, con.QueryScalar ("SELECT COUNT(*) FROM Users"));
        }

        [Test]
        public void Functions ()
        {
            con.AddFunction<Md5Function> ();

            using (var stmt = con.CreateStatement ("SELECT HYENA_MD5(?, ?)")) {
                Assert.AreEqual ("ae2b1fca515949e5d54fb22b8ed95575", stmt.Bind (1, "testing").QueryScalar ());
                Assert.AreEqual (null, stmt.Bind (1, null).QueryScalar ());
            }

            using (var stmt = con.CreateStatement ("SELECT HYENA_MD5(?, ?, ?)")) {
                Assert.AreEqual ("ae2b1fca515949e5d54fb22b8ed95575", stmt.Bind (2, "test", "ing").QueryScalar ());
                Assert.AreEqual (null, stmt.Bind (2, null, null).QueryScalar ());
            }

            using (var stmt = con.CreateStatement ("SELECT HYENA_MD5(?, ?, ?, ?)")) {
                Assert.AreEqual (null, stmt.Bind (3, null, "", null).QueryScalar ());

                try {
                    con.RemoveFunction<Md5Function> ();
                    Assert.Fail ("Removed function while statement active");
                } catch (SqliteException e) {
                    Assert.AreEqual (5, e.ErrorCode);
                }
            }

            try {
                using (var stmt = con.CreateStatement ("SELECT HYENA_MD5(?, ?, ?, ?)")) {
                    Assert.AreEqual ("ae2b1fca515949e5d54fb22b8ed95575", stmt.QueryScalar ());
                    Assert.Fail ("Function HYENA_MD5 should no longer exist");
                }
            } catch {}
        }
    }
}

#endif
