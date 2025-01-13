using System;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Diagnostics;

namespace Interactive_Desktop.Injecter
{
    internal class InjecterTools
    {
        public static bool FindWorker(IntPtr wnd, ref IntPtr lp)
        {
            var p = NativeFunctions.FindWindowEx(wnd, IntPtr.Zero, "SHELLDLL_DefView", null);
            Console.WriteLine("SHELLDLL_DefView : " + p);
            if (p != IntPtr.Zero)
            {
                lp = NativeFunctions.FindWindowEx(IntPtr.Zero, wnd, "WorkerW", null);
                Console.WriteLine("WorkerW : " + lp);
            }
            return true;
        }
        public static bool IsChild(IntPtr parent, IntPtr child)
        {
            var current = child;
            while (current != IntPtr.Zero)
            {
                if (current == parent)
                {
                    return true;
                }
                current = NativeFunctions.GetParent(current);
            }
            return false;
        }

        public static bool UpdateWindowStyles(IntPtr wnd, long and, long exAnd, long or,
            long exOr)
        {
            long style = 0;
            long exStyle = 0;

            NativeFunctions.SetLastError(IntPtr.Zero);

            style = NativeFunctions.GetWindowLong(wnd, -16);
            if (style != 0)
            {
                exStyle = NativeFunctions.GetWindowLong(wnd, -20);
            }

            var gle = NativeFunctions.GetLastError();
            if ((style == 0 || exStyle == 0) && gle != 0)
            {
                Console.WriteLine($"Failed to get window styles");
                return false;
            }

            style = (style & and) | or;
            exStyle = (exStyle & exAnd) | exOr;

            NativeFunctions.SetLastError(IntPtr.Zero);

            if (NativeFunctions.SetWindowLong(wnd, -16, style) == 0 || NativeFunctions.SetWindowLong(wnd, -20, exStyle) == 0)
            {
                gle = NativeFunctions.GetLastError();
                if (gle != 0)
                {
                    Console.WriteLine($"Failed to set window styles");
                    return false;
                }
            }
            return true;
        }

        private static bool GetRect(IntPtr wnd, ref NativeFunctions.Rect rect)
        {
            if (!NativeFunctions.GetWindowRect(wnd, ref rect))
            {
                Console.WriteLine("Failed to get window rect");
                return false;
            }

            return true;
        }

        public static bool MapRect(IntPtr wnd, IntPtr worker, ref NativeFunctions.Rect mapped)
        {
            if (GetRect(wnd, ref mapped))
            {
                return false;
            }
            NativeFunctions.MapWindowPoints(0, worker, ref mapped, 2);
            return true;
        }

        public static bool Move(IntPtr wnd, long left, long top, long right, long bottom)
        {
            var succ = NativeFunctions.SetWindowPos(wnd, IntPtr.Zero, left, top, right - left, bottom - top, 0);
            if (!succ)
            {
                Console.WriteLine("Failed to move window");
            }
            return succ;

        }

        public static bool Fullscreen(IntPtr wnd, IntPtr worker)
        {
            var r = new NativeFunctions.Rect();

            if (!GetRect(wnd, ref r))
            {
                return false;
            }

            NativeFunctions.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMonitor, IntPtr hdcMonitor, ref NativeFunctions.Rect lprcMonitor, IntPtr dwData) =>
            {
                Console.WriteLine("Monitor : " + hMonitor + " Name : " + dwData);
                var mi = new NativeFunctions.MonitorInfo();
                mi.CbSize = Marshal.SizeOf(mi);

                if (!NativeFunctions.GetMonitorInfo(hMonitor, ref mi))
                {
                    Console.WriteLine("Failed to get monitor info");
                    return false;
                }

                // name of the monitor
                Console.WriteLine("Monitor : " + dwData);
                return true;
            }, IntPtr.Zero);

            IntPtr mon = NativeFunctions.MonitorFromPoint(new NativeFunctions.Point { X = r.Left, Y = r.Top }, 1);
            Console.WriteLine("Monitor : " + mon);
            if (mon == IntPtr.Zero)
            {
                Console.WriteLine("Failed to get monitor");
                return false;
            }

            var mi = new NativeFunctions.MonitorInfo();
            mi.CbSize = Marshal.SizeOf(mi);

            if (!NativeFunctions.GetMonitorInfo(mon, ref mi))
            {
                Console.WriteLine("Failed to get monitor info");
                return false;
            }

            NativeFunctions.MapWindowPoints(0, worker, ref mi.RcMonitor, 2);

            return Move(wnd, mi.RcMonitor.Left, mi.RcMonitor.Top, mi.RcMonitor.Right, mi.RcMonitor.Bottom);
        }
    }
}
