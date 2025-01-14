using System;
using System.Runtime.InteropServices;

namespace Interactive_Desktop.Injecter
{
    internal class InjecterTools
    {
        public static IntPtr GetWorkerW()
        {
            var progman = NativeFunctions.FindWindow("Progman", null);
            if (progman == nint.Zero)
            {
                Console.Error.WriteLine("Failed to find window");
                return IntPtr.Zero;
            }

            _ = NativeFunctions.SendMessageTimeout(
                progman,
                0x052C,
                0x0000000D,
                0x00000001,
                0,
                1000,
                0
            );

            var hWorkerW = IntPtr.Zero;
            NativeFunctions.EnumWindows(
                (topHandle, topParamHandle) =>
                {
                    var shellDllDefView = NativeFunctions.FindWindowEx(
                        topHandle,
                        IntPtr.Zero,
                        "SHELLDLL_DefView",
                        null
                    );
                    if (shellDllDefView != IntPtr.Zero)
                    {
                        hWorkerW = NativeFunctions.FindWindowEx(
                            topHandle,
                            IntPtr.Zero,
                            "WorkerW",
                            null
                        );
                    }
                    return true;
                },
                IntPtr.Zero
            );
            return hWorkerW;
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

        public static bool UpdateWindowStyles(IntPtr wnd, long and, long exAnd, long or, long exOr)
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

            if (
                NativeFunctions.SetWindowLong(wnd, -16, style) == 0
                || NativeFunctions.SetWindowLong(wnd, -20, exStyle) == 0
            )
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
            if (NativeFunctions.GetWindowRect(wnd, ref rect))
                return true;
            Console.WriteLine("Failed to get window rect");
            return false;
        }

        public static bool Move(IntPtr wnd, long left, long top, long right, long bottom)
        {
            var succ = NativeFunctions.SetWindowPos(
                wnd,
                IntPtr.Zero,
                left,
                top,
                right - left,
                bottom - top,
                0
            );
            if (!succ)
            {
                Console.Error.WriteLine("Failed to move window");
            }
            return succ;
        }

        public static NativeFunctions.MonitorInfo? GetMonitorInfo(IntPtr hMonitor)
        {
            var mi = new NativeFunctions.MonitorInfo();
            mi.CbSize = Marshal.SizeOf(mi);

            if (NativeFunctions.GetMonitorInfo(hMonitor, ref mi))
                return mi;
            Console.Error.WriteLine("Failed to get monitor info");
            return null;
        }

        public static IntPtr GetMonitorFromPoint(IntPtr wnd)
        {
            var r = new NativeFunctions.Rect();
            if (!GetRect(wnd, ref r))
            {
                return IntPtr.Zero;
            }

            IntPtr mon = NativeFunctions.MonitorFromPoint(
                new NativeFunctions.Point { X = r.Left, Y = r.Top },
                1
            );
            if (mon != IntPtr.Zero)
                return mon;
            Console.Error.WriteLine("Failed to get monitor");
            return IntPtr.Zero;
        }

        public static bool SetMonitor(IntPtr worker, IntPtr wnd, IntPtr monitor)
        {
            if (GetMonitorInfo(monitor) is { } mInfo)
            {
                NativeFunctions.MapWindowPoints(0, worker, ref mInfo.RcMonitor, 2);

                return Move(
                    wnd,
                    mInfo.RcMonitor.Left,
                    mInfo.RcMonitor.Top,
                    mInfo.RcMonitor.Right,
                    mInfo.RcMonitor.Bottom
                );
            }
            return false;
        }
    }
}
