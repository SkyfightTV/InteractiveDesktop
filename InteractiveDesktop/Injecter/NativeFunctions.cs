using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Interactive_Desktop.Injecter
{
    internal class NativeFunctions
    {
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SendMessage(nint hWnd, int wMsg, nint wParam, nint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern nint FindWindow(string? lpClassName, string? lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, long lParam);

        public delegate bool EnumWindowsProc(nint hWnd, long lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr ShowWindow(nint hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(
            IntPtr hWndParent,
            IntPtr hWndChildAfter,
            string lpClassName,
            string? lpWindowName
        );

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern long SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool MapWindowPoints(
            IntPtr hWndFrom,
            IntPtr hWndTo,
            ref Rect lpPoints,
            uint cPoints
        );

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            long X,
            long Y,
            long cx,
            long cy,
            uint uFlags
        );

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SendMessageTimeout(
            IntPtr hWnd,
            uint Msg,
            IntPtr wParam,
            IntPtr lParam,
            uint fuFlags,
            uint timeout,
            IntPtr hResult
        );

        public struct Point
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MonitorFromPoint(Point pt, int dwFlags);

        public struct MonitorInfo
        {
            public int CbSize;
            public Rect RcMonitor;
            public Rect RcWork;
            public int DwFlags;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool EnumDisplayMonitors(
            IntPtr hdc,
            IntPtr lprcClip,
            EnumMonitorsDelegate lpfnEnum,
            IntPtr dwData
        );

        public delegate bool EnumMonitorsDelegate(
            IntPtr hMonitor,
            IntPtr hdcMonitor,
            ref Rect lprcMonitor,
            IntPtr dwData
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern void SetLastError(IntPtr dwErrCode);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetLastError();
    }
}
