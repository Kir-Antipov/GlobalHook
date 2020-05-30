using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GlobalHook.Demo.WinForms
{
    public partial class ProcessesForm : Form
    {
        public ProcessesForm()
        {
            InitializeComponent();
        }

        public ProcessInfo SelectedProcessInfo { get; private set; }

        private void ProcessesForm_Load(object sender, EventArgs e)
        {
            IntPtr hWnd = IntPtr.Zero;
            string moduleName = string.Empty;
            Process[] localAll = Process.GetProcesses();
            foreach (Process p in localAll)
            {
                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    hWnd = p.MainWindowHandle;
                    try
                    {
                        moduleName = p.MainModule.ModuleName;
                    }
                    catch { }
                    string windowTitle = p.MainWindowTitle == "" ? moduleName : p.MainWindowTitle + (moduleName != "" ? $" ({moduleName})" : "");

                    lbProcess.Items.Add(new ProcessInfo(p.Id, windowTitle, hWnd));
                }
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (lbProcess.SelectedItem is ProcessInfo processInfo)
            {
                SelectedProcessInfo = processInfo;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please, choose process first.");
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    public class ProcessInfo
    {
        public long Id { get; }
        public string Title { get; }
        public IntPtr MainWindowHandle { get; }

        public ProcessInfo(int id, string title, IntPtr mainWindowHandle)
        {
            Id = id;
            Title = title;
            MainWindowHandle = mainWindowHandle;
        }

        public override string ToString() =>  Title;
    }
}
