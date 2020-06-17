using GlobalHook.Core;
using GlobalHook.Core.Keyboard;
using GlobalHook.Core.MessageLoop;
using GlobalHook.Core.Mouse;
using System;
using System.Linq;
using System.Threading;

namespace GlobalHook.Demo.Console
{
    using Console = System.Console;

    public class Program
    {
        public static void Main()
        {
            // Manual loading
            /*IMouseHook mouseHook = new Core.Windows.Mouse.MouseHook();
            // IMouseHook mouseHook = new Core.Windows.Mouse.RawMouseHook();
            IKeyboardHook keyboardHook = new Core.Windows.Keyboard.KeyboardHook();
            IMessageLoop loop = new Core.Windows.MessageLoop.MessageLoop();*/

            // Dynamic loading
            IHook[] hooks = IHook
                .Load(Environment.CurrentDirectory)     // Load all hooks from current directory's DLLs
                .Where(x => x.CanBeInstalled)           // Select only those hooks that are supported by the current OS 
                .Where(x => x.CanPreventDefault)        // We're interested in hooks that can prevent default event actions
                .ToArray();

            IMouseHook mouseHook = hooks.OfType<IMouseHook>().First();
            IKeyboardHook keyboardHook = hooks.OfType<IKeyboardHook>().First();
            IMessageLoop loop = IMessageLoop.Load(Environment.CurrentDirectory).First(x => x.CanBeRunned);

            CancellationTokenSource source = new CancellationTokenSource();

            loop.OnEvent += Log;

            // Stop message loop on Ctrl + Shift + S
            keyboardHook.OnEvent += (_, e) =>
            {
                if (e.Key.HasFlags(Keys.Control, Keys.Shift, Keys.S))
                    source.Cancel();
            };

            // Disable 'M'
            keyboardHook.OnEvent += (_, e) =>
            {
                if (e.CanPreventDefault && e.Key.HasFlag(Keys.M))
                    e.PreventDefault();
            };

            // Disable middle mouse button
            mouseHook.OnEvent += (_, e) =>
            {
                if (e.CanPreventDefault && e.MouseEventType == MouseEventType.Key && e.Key == MouseButtons.Middle)
                    e.PreventDefault();
            };

            Console.WriteLine("Hook!");
            Console.WriteLine("Press CTRL + SHIFT + S to stop!");

            loop.Run(source.Token, mouseHook, keyboardHook);

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }

        private static void Log(object? sender, IHookEventArgs e)
        {
            if (e is IKeyboardEventArgs keyboard)
            {
                Console.WriteLine("[{0}] Keyboard: {1}, {2}, {3}", e.Time, keyboard.Key, keyboard.KeyState, keyboard.KeyChar);
            }
            else if (e is IMouseEventArgs mouse)
            {
                Console.WriteLine("[{0}] Mouse: ({1}; {2}){3}, {4}, {5}, {6}, {7}, {8}", 
                    e.Time,
                    mouse.Coords.X, mouse.Coords.Y, mouse.Coords.IsRelative ? "R" : string.Empty, 
                    mouse.MouseEventType, 
                    mouse.Key, 
                    mouse.KeyState, 
                    mouse.IsDoubleClick,
                    mouse.Delta);
            }
            else
            {
                Console.WriteLine("[{0}] Unknown", e.Time);
            }
        }
    }
}
