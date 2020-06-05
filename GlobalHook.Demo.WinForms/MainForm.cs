using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Mouse;
using GlobalHook.Core.Windows.Keyboard;
using GlobalHook.Core.Windows.Mouse;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GlobalHook.Demo.WinForms
{
    public partial class MainForm : Form
    {
        private readonly IMouseHook mouseHook;
        private readonly IKeyboardHook keyboardHook;

        public MainForm()
        {
            InitializeComponent();

            mouseHook = new MouseHook();
            keyboardHook = new KeyboardHook();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            mouseHook.Install();
            keyboardHook.Install();
        }

        #region Check boxes to set or remove particular event handlers.

        private void chMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            if (chMouseMove.Checked)
            {
                mouseHook.MouseMove += HookManager_MouseMove;
            }
            else
            {
                mouseHook.MouseMove -= HookManager_MouseMove;
                lblMousePosition.Text = "x=?; y=?";
            }
        }

        private void chMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            if (chMouseWheel.Checked)
            {
                mouseHook.MouseWheel += HookManager_MouseWheel;
            }
            else
            {
                mouseHook.MouseWheel -= HookManager_MouseWheel;
                lblWheel.Text = "Wheel=?";
            }
        }

        private void chMouseClick_CheckedChanged(object sender, EventArgs e)
        {
            if (chMouseClick.Checked)
            {
                mouseHook.MouseClick += HookManager_MouseClick;
            }
            else
            {
                mouseHook.MouseClick -= HookManager_MouseClick;
            }
        }

        private void chMouseUp_CheckedChanged(object sender, EventArgs e)
        {
            if (chMouseUp.Checked)
            {
                mouseHook.MouseUp += HookManager_MouseUp;
            }
            else
            {
                mouseHook.MouseUp -= HookManager_MouseUp;
            }
        }

        private void chMouseDown_CheckedChanged(object sender, EventArgs e)
        {
            if (chMouseDown.Checked)
            {
                mouseHook.MouseDown += HookManager_MouseDown;
            }
            else
            {
                mouseHook.MouseDown -= HookManager_MouseDown;
            }
        }

        private void chMouseDoubleClick_CheckedChanged(object sender, EventArgs e)
        {
            if (chMouseDoubleClick.Checked)
            {
                mouseHook.MouseDoubleClick += HookManager_MouseDoubleClick;
            }
            else
            {
                mouseHook.MouseDoubleClick -= HookManager_MouseDoubleClick;
            }
        }

        private void chKeyDown_CheckedChanged(object sender, EventArgs e)
        {
            if (chKeyDown.Checked)
            {
                keyboardHook.KeyDown += HookManager_KeyDown;
            }
            else
            {
                keyboardHook.KeyDown -= HookManager_KeyDown;
            }
        }


        private void chKeyUp_CheckedChanged(object sender, EventArgs e)
        {
            if (chKeyUp.Checked)
            {
                keyboardHook.KeyUp += HookManager_KeyUp;
            }
            else
            {
                keyboardHook.KeyUp -= HookManager_KeyUp;
            }
        }

        private void chKeyPress_CheckedChanged(object sender, EventArgs e)
        {
            if (chKeyPress.Checked)
            {
                keyboardHook.KeyPress += HookManager_KeyPress;
            }
            else
            {
                keyboardHook.KeyPress -= HookManager_KeyPress;
            }
        }

        #endregion

        #region Event handlers of particular events. They will be activated when an appropriate checkbox is checked.

        private void HookManager_KeyDown(object sender, IKeyboardEventArgs e)
        {
            if (chLockKB.Checked)
            {
                e.PreventDefault();
            }
            WriteLog(e.Key.ToString(), e.DefaultPrevented);
        }

        private void HookManager_KeyUp(object sender, IKeyboardEventArgs e)
        {
            if (chLockKB.Checked)
            {
                e.PreventDefault();
            }
            WriteLog(e.Key.ToString(), e.DefaultPrevented);
        }

        private void HookManager_KeyPress(object sender, IKeyboardEventArgs e)
        {
            if (chLockKB.Checked)
            {
                e.PreventDefault();
            }
            WriteLog(e.Key.ToString(), e.DefaultPrevented);
        }

        private void HookManager_MouseMove(object sender, IMouseEventArgs e)
        {
            if (chLockM.Checked)
            {
                e.PreventDefault();
            }
            lblMousePosition.Text = $"x={e.Coords.X:0000}; y={e.Coords.Y:0000}" + GetLockedStr(e.DefaultPrevented);
        }

        private void HookManager_MouseClick(object sender, IMouseEventArgs e)
        {
            if (chLockM.Checked)
            {
                e.PreventDefault();
            }
            WriteLog(e.Key.ToString(), e.DefaultPrevented);
        }

        private void HookManager_MouseUp(object sender, IMouseEventArgs e)
        {
            if (chLockM.Checked)
            {
                e.PreventDefault();
            }
            WriteLog(e.Key.ToString(), e.DefaultPrevented);
        }

        private void HookManager_MouseDown(object sender, IMouseEventArgs e)
        {
            if (chLockM.Checked)
            {
                e.PreventDefault();
            }
            WriteLog(e.Key.ToString(), e.DefaultPrevented);
        }

        private void HookManager_MouseDoubleClick(object sender, IMouseEventArgs e)
        {
            if (chLockM.Checked)
            {
                e.PreventDefault();
            }
            WriteLog(e.Key.ToString(), e.DefaultPrevented);
        }

        private void HookManager_MouseWheel(object sender, IMouseEventArgs e)
        {
            if (chLockM.Checked)
            {
                e.PreventDefault();
            }
            lblWheel.Text = $"Wheel={e.Delta:000}" + GetLockedStr(e.DefaultPrevented);
        }

        void WriteLog(string context, bool isLocked, [CallerMemberName] string eventName = null)
        {
            eventName = eventName.Substring(eventName.IndexOf("_") + 1);

            string lockedStr = GetLockedStr(isLocked);
            tbLog.AppendText($"{eventName} - {context}{lockedStr}{Environment.NewLine}");
            tbLog.ScrollToCaret();
        }

        private static string GetLockedStr(bool isLocked) =>  isLocked ? " (Locked)" : string.Empty;

        #endregion

        private void bAttach_Click(object sender, EventArgs e)
        {
            ProcessesForm pl = new ProcessesForm();
            if (pl.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    mouseHook.Install(pl.SelectedProcessInfo.Id);
                    keyboardHook.Install(pl.SelectedProcessInfo.Id);
                    lblProcess.Text = "Status: Attached to " + pl.SelectedProcessInfo.Title;
                    bDetach.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Unable to attach this window");
                    Detach();
                }
            }
        }

        private void bDetach_Click(object sender, EventArgs e)
        {
            Detach();
        }

        void Detach()
        {
            bDetach.Enabled = false;
            mouseHook.Install();
            keyboardHook.Install();
            lblProcess.Text = "Status: Global";
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mouseHook?.Dispose();
            keyboardHook?.Dispose();
        }
    }
}
