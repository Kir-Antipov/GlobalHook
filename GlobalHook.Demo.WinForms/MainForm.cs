using GlobalHook.Core.Keyboard;
using GlobalHook.Core.Mouse;
using GlobalHook.Core.Windows.Keyboard;
using GlobalHook.Core.Windows.Mouse;
using System;
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

        private void checkBoxOnMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseMove.Checked)
            {
                mouseHook.MouseMove += HookManager_MouseMove;
            }
            else
            {
                mouseHook.MouseMove -= HookManager_MouseMove;
                labelMousePosition.Text = "x=?; y=?";
            }
        }

        private void checkBoxOnMouseClick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseClick.Checked)
            {
                mouseHook.MouseClick += HookManager_MouseClick;
            }
            else
            {
                mouseHook.MouseClick -= HookManager_MouseClick;
            }
        }

        private void checkBoxOnMouseUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseUp.Checked)
            {
                mouseHook.MouseUp += HookManager_MouseUp;
            }
            else
            {
                mouseHook.MouseUp -= HookManager_MouseUp;
            }
        }

        private void checkBoxOnMouseDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOnMouseDown.Checked)
            {
                mouseHook.MouseDown += HookManager_MouseDown;
            }
            else
            {
                mouseHook.MouseDown -= HookManager_MouseDown;
            }
        }

        private void checkBoxMouseDoubleClick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMouseDoubleClick.Checked)
            {
                mouseHook.MouseDoubleClick += HookManager_MouseDoubleClick;
            }
            else
            {
                mouseHook.MouseDoubleClick -= HookManager_MouseDoubleClick;
            }
        }

        private void checkBoxMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMouseWheel.Checked)
            {
                mouseHook.MouseWheel += HookManager_MouseWheel;
            }
            else
            {
                mouseHook.MouseWheel -= HookManager_MouseWheel;
                labelWheel.Text = "Wheel=?";
            }
        }

        private void checkBoxKeyDown_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyDown.Checked)
            {
                keyboardHook.KeyDown += HookManager_KeyDown;
            }
            else
            {
                keyboardHook.KeyDown -= HookManager_KeyDown;
            }
        }


        private void checkBoxKeyUp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyUp.Checked)
            {
                keyboardHook.KeyUp += HookManager_KeyUp;
            }
            else
            {
                keyboardHook.KeyUp -= HookManager_KeyUp;
            }
        }

        private void checkBoxKeyPress_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyPress.Checked)
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
            if (chbLockKB.Checked)
            {
                e.PreventDefault();
            }
            WriteLog("KeyDown - " + e.Key + (e.DefaultPrevented ? " (Locked)" : ""));
        }

        private void HookManager_KeyUp(object sender, IKeyboardEventArgs e)
        {
            if (chbLockKB.Checked)
            {
                e.PreventDefault();
            }
            WriteLog("KeyUp - " + e.Key + (e.DefaultPrevented ? " (Locked)" : ""));
        }

        private void HookManager_KeyPress(object sender, IKeyboardEventArgs e)
        {
            if (chbLockKB.Checked)
            {
                e.PreventDefault();
            }
            WriteLog("KeyPress - " + e.Key + (e.DefaultPrevented ? " (Locked)" : ""));
        }

        private void HookManager_MouseMove(object sender, IMouseEventArgs e)
        {
            if (chbLockM.Checked)
            {
                e.PreventDefault();
            }
            labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}" + (e.DefaultPrevented ? " (Locked)" : ""), e.Coords.X, e.Coords.Y);
        }

        private void HookManager_MouseClick(object sender, IMouseEventArgs e)
        {
            if (chbLockM.Checked)
            {
                e.PreventDefault();
            }
            WriteLog("MouseClick - " + e.Key + (e.DefaultPrevented ? " (Locked)" : ""));
        }

        private void HookManager_MouseUp(object sender, IMouseEventArgs e)
        {
            if (chbLockM.Checked)
            {
                e.PreventDefault();
            }
            WriteLog("MouseUp - " + e.Key + (e.DefaultPrevented ? " (Locked)" : ""));
        }

        private void HookManager_MouseDown(object sender, IMouseEventArgs e)
        {
            if (chbLockM.Checked)
            {
                e.PreventDefault();
            }
            WriteLog("MouseDown - " + e.Key + (e.DefaultPrevented ? " (Locked)" : ""));
        }

        private void HookManager_MouseDoubleClick(object sender, IMouseEventArgs e)
        {
            if (chbLockM.Checked)
            {
                e.PreventDefault();
            }
            WriteLog("MouseDoubleClick - " + e.Key + (e.DefaultPrevented ? " (Locked)" : ""));
        }

        private void HookManager_MouseWheel(object sender, IMouseEventArgs e)
        {
            if (chbLockM.Checked)
            {
                e.PreventDefault();
            }
            labelWheel.Text = string.Format("Wheel={0:000}{1}", e.Delta, e.DefaultPrevented ? " (Locked)" : "");
        }

        void WriteLog(string message)
        {
            textBoxLog.AppendText(message + Environment.NewLine);
            textBoxLog.ScrollToCaret();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

            ProcessesForm pl = new ProcessesForm();
            if (pl.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    mouseHook.Install(pl.SelectedProcessInfo.Id);
                    keyboardHook.Install(pl.SelectedProcessInfo.Id);
                    label1.Text = "Status: Attached to " + pl.SelectedProcessInfo.Title;
                    button2.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Unable to attach this window");
                    Detach();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Detach();
        }

        void Detach()
        {
            button2.Enabled = false;
            mouseHook.Install();
            keyboardHook.Install();
            label1.Text = "Status: Global";
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mouseHook?.Dispose();
            keyboardHook?.Dispose();
        }
    }
}
