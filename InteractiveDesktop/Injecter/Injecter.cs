using System;

namespace Interactive_Desktop.Injecter
{
    internal class Injecter
    {
        private readonly IntPtr _workerW;
        private readonly IntPtr _window;
        private IntPtr _previousParent;

        public Injecter(IntPtr window)
        {
            _window = window;
            _workerW = InjecterTools.GetWorkerW();
            if (_workerW == IntPtr.Zero)
            {
                throw new Exception("Failed to get WorkerW");
            }
        }

        public bool Attach(IntPtr monitor)
        {
            if (NativeFunctions.GetParent(_window) == _workerW)
            {
                return false;
            }

            const long and = (long)
                ~(
                    WindowStyleFlags.Caption
                    | WindowStyleFlags.ThickFrame
                    | WindowStyleFlags.SystemMenu
                    | WindowStyleFlags.MaximizeBox
                    | WindowStyleFlags.MinimizeBox
                );
            const long exAnd = (long)
                ~(
                    WindowStyleFlags.ExtendedDlgModalFrame
                    | WindowStyleFlags.ExtendedComposited
                    | WindowStyleFlags.ExtendedWindowEdge
                    | WindowStyleFlags.ExtendedClientEdge
                    | WindowStyleFlags.ExtendedLayered
                    | WindowStyleFlags.ExtendedStaticEdge
                    | WindowStyleFlags.ExtendedToolWindow
                    | WindowStyleFlags.ExtendedAppWindow
                );

            if (
                !InjecterTools.UpdateWindowStyles(
                    _window,
                    and,
                    exAnd,
                    (long)WindowStyleFlags.Child,
                    0
                )
            )
            {
                Console.Error.WriteLine("W: failed to update window styles");
                return false;
            }

            _previousParent = NativeFunctions.SetParent(_window, _workerW);
            InjecterTools.SetMonitor(_workerW, _window, monitor);
            NativeFunctions.ShowWindow(_window, 5);
            return true;
        }

        public bool Detach()
        {
            if (NativeFunctions.GetParent(_window) != _workerW)
            {
                return false;
            }
            NativeFunctions.SetParent(_window, _previousParent);
            NativeFunctions.ShowWindow(_window, 0);
            return true;
        }
    }
}
