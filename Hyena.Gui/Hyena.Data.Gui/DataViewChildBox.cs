//
// DataViewChildBox.cs
//
// Author:
//   Gabriel Burt <gburt@novell.com>
//
// Copyright 2010 Novell, Inc.
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

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using Hyena.Gui.Canvas;

namespace Hyena.Data.Gui
{
    public class DataViewChildBox : DataViewChild
    {
        public bool Horizontal { get; set; }

        private List<DataViewChild> children = new List<DataViewChild> ();
        public IEnumerable<DataViewChild> Children { get { return children; } }

        public DataViewChildBox (params DataViewChild [] children)
        {
            Add (children);
        }

        public void Add (params DataViewChild [] children)
        {
            this.children.AddRange (children);
            ForEach (child => child.Parent = this);
        }

        private void ForEach (Action<DataViewChild> action)
        {
            foreach (var child in Children) {
                action (child);
            }
        }

        public override void BindDataItem (object item)
        {
            base.BindDataItem (item);
            ForEach (child => child.BindDataItem (item));
        }

        protected override void RenderCore (CellContext context)
        {
            var itr = Horizontal && context.IsRtl ? Children.Reverse () : Children;
            foreach (var child in itr) {
                RenderChild (child, context);
            }
        }

        private void RenderChild (DataViewChild child, CellContext context)
        {
            var cairo_context = context.Context;
            var child_allocation = child.Allocation;

            context.Area = (Gdk.Rectangle) TopLevelAllocation;
            cairo_context.Save ();
            cairo_context.Translate (child_allocation.X, child_allocation.Y);
            //cairo_context.Rectangle (0, 0, child_allocation.Width, child_allocation.Height);
            //cairo_context.Clip ();
            child.Render (context);
            cairo_context.Restore ();
        }

        public override void Arrange ()
        {
            ForEach (child => child.Arrange ());
        }

        public override Size Measure (Size available)
        {
            double x = Padding.Left, y = Padding.Top;
            double width = 0, height = 0;
            foreach (var child in Children) {
                var size = child.Measure (available);
                child.Allocation = new Rect (x, y, size.Width, size.Height);

                // TODO account for childrens' padding/margin
                if (Horizontal) {
                    width  += size.Width;
                    height = Math.Max (height, size.Height);
                    x += size.Width;
                } else {
                    width  = Math.Max (width, size.Width);
                    height += size.Height;
                    y += size.Height;
                }
            }

            foreach (var child in Children) {
                var a = child.Allocation;
                if (Horizontal) {
                    child.Allocation = new Rect (a.X, a.Y, a.Width, height);
                } else {
                    child.Allocation = new Rect (a.X, a.Y, width, a.Height);
                }
            }

            return new Size (width + Padding.X, height + Padding.Y);
        }

        public override bool ButtonEvent (Point cursor, bool pressed, uint button)
        {
            var child = FindChildAt (cursor);
            return child == null ? false : child.ButtonEvent (ChildCoord (cursor, child), pressed, button);
        }

        DataViewChild last_motion_child;
        public override bool CursorMotionEvent (Point cursor)
        {
            var child = FindChildAt (cursor);
            
            bool new_child = child != last_motion_child;
            if (new_child) {
                if (last_motion_child != null) {
                    last_motion_child.CursorLeaveEvent ();
                }
                last_motion_child = child;
            }

            if (child == null) {
                return false;
            }

            if (new_child) {
                child.CursorEnterEvent ();
            }

            child.CursorMotionEvent (ChildCoord (cursor, child));
            return true;
        }

        public override bool CursorLeaveEvent ()
        {
            bool ret = base.CursorLeaveEvent ();

            if (last_motion_child != null) {
                ret |= last_motion_child.CursorLeaveEvent ();
                last_motion_child = null;
            }

            return ret;
        }

        private DataViewChild FindChildAt (Point pt)
        {
            return Children.LastOrDefault (c => pt.X >= c.Allocation.X && pt.Y >= c.Allocation.Y);
        }

        private Point ChildCoord (Point pt, DataViewChild child)
        {
            return new Point (pt.X - child.Allocation.X, pt.Y - child.Allocation.Y);
        }
    }
}
