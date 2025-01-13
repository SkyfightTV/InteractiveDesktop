using System;
using static System.Console;

namespace Interactive_Desktop.Injecter
{
    internal class Injecter
    {

        public static void Attach(IntPtr win)
        {
            var progman = NativeFunctions.FindWindow("Progman", null);


            if (progman == nint.Zero)
            {
                WriteLine("Failed to find window");
                return;
            }

            _ = NativeFunctions.SendMessageTimeout(progman, 0x052C, 0x0000000D, 0x00000001, 0, 1000, 0);

            var hWorkerW = IntPtr.Zero;
            NativeFunctions.EnumWindows((topHandle, topParamHandle) =>
            {
                var shellDllDefView = NativeFunctions.FindWindowEx(topHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (shellDllDefView != IntPtr.Zero)
                {
                    hWorkerW = NativeFunctions.FindWindowEx(topHandle, IntPtr.Zero, "WorkerW", null);
                } 
                return true;
            }, IntPtr.Zero);
            WriteLine("Worker " + hWorkerW);

            
            var r = new NativeFunctions.Rect();
            InjecterTools.MapRect(win, hWorkerW,  ref r);

            const long and = (long)~(
                WindowStyleFlags.Caption |
                WindowStyleFlags.ThickFrame |
                WindowStyleFlags.SystemMenu |
                WindowStyleFlags.MaximizeBox |
                WindowStyleFlags.MinimizeBox
            );

            const long exAnd = (long)~(
                WindowStyleFlags.ExtendedDlgModalFrame
                | WindowStyleFlags.ExtendedComposited
                | WindowStyleFlags.ExtendedWindowEdge
                | WindowStyleFlags.ExtendedClientEdge
                | WindowStyleFlags.ExtendedLayered
                | WindowStyleFlags.ExtendedStaticEdge
                | WindowStyleFlags.ExtendedToolWindow
                | WindowStyleFlags.ExtendedAppWindow
            );

            if (!InjecterTools.UpdateWindowStyles(win, and, exAnd, (long)WindowStyleFlags.Child, 0))
            {
                WriteLine("W: failed to update window styles");
                return;
            }


            var t = NativeFunctions.SetParent(win, hWorkerW);
            WriteLine("SetParent " + t);
            var l = NativeFunctions.GetParent(win);
            WriteLine("SetParent " + l);
            NativeFunctions.ShowWindow(win, 5);
            WriteLine(r.Left + " : " +  r.Top + " : " + r.Right + " : " + r.Bottom);
            InjecterTools.Fullscreen(win, hWorkerW);
            WriteLine("good");

            //if (worker != nint.Zero) return worker;
            //WriteLine("W: couldnt spawn window behind icons, falling back to Progman");
            //return progman;
        }

        //private static bool AddProgram(nint wnd)
        //{
        //    var wndclass = new StringBuilder(256);
        //    var wallpaper = GetWorker();
        //    WriteLine("WallPaper : " + wallpaper);
        //    var r = new NativeFunctions.Rect();

        //    _ = NativeFunctions.GetClassName(wnd, wndclass, wndclass.Capacity);

        //    if (wallpaper == wnd || wndclass.ToString() == "Shell_TrayWnd")
        //    {
        //        WriteLine("can't add this window");
        //        return false;
        //    }

        //    if (InjecterTools.IsChild(wallpaper, wnd))
        //    {
        //        WriteLine("window is already a child of WorkerW");
        //        return false;
        //    }

        //    WriteLine("adding : " + wnd);

        //    const long and = (long) ~(
        //        WindowStyleFlags.Caption |
        //        WindowStyleFlags.ThickFrame |
        //        WindowStyleFlags.SystemMenu |
        //        WindowStyleFlags.MaximizeBox |
        //        WindowStyleFlags.MinimizeBox
        //    );

        //    const long exAnd = (long) ~(
        //        WindowStyleFlags.ExtendedDlgModalFrame
        //        | WindowStyleFlags.ExtendedComposited
        //        | WindowStyleFlags.ExtendedWindowEdge
        //        | WindowStyleFlags.ExtendedClientEdge
        //        | WindowStyleFlags.ExtendedLayered
        //        | WindowStyleFlags.ExtendedStaticEdge
        //        | WindowStyleFlags.ExtendedToolWindow
        //        | WindowStyleFlags.ExtendedAppWindow
        //    );

        //    if (!InjecterTools.UpdateWindowStyles(wnd, and, exAnd, (long)WindowStyleFlags.Child, 0))
        //    {
        //        WriteLine("W: failed to update window styles");
        //        return false;
        //    }

        //    InjecterTools.MapRect(wnd, ref r);

        //    NativeFunctions.SetParent(wnd, wallpaper);

        //    NativeFunctions.ShowWindow(wnd, 5);
        //    InjecterTools.Move(wnd, r.Left, r.Top, r.Right, r.Bottom);
        //    return true;
        //}

        public static void Inject(string processName, string message)
        {
            var media = NativeFunctions.FindWindow(null, "DemoVideo.mp4 - mpv.net");
            
            WriteLine(media);
            Attach(media);
        }
    }
}