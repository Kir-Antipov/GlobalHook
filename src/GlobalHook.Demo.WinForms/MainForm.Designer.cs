namespace GlobalHook.Demo.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMousePosition = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.lblWheel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bDetach = new System.Windows.Forms.Button();
            this.lblProcess = new System.Windows.Forms.Label();
            this.bAttach = new System.Windows.Forms.Button();
            this.chLockM = new System.Windows.Forms.CheckBox();
            this.chLockKB = new System.Windows.Forms.CheckBox();
            this.chKeyUp = new System.Windows.Forms.CheckBox();
            this.chKeyPress = new System.Windows.Forms.CheckBox();
            this.chKeyDown = new System.Windows.Forms.CheckBox();
            this.chMouseWheel = new System.Windows.Forms.CheckBox();
            this.chMouseDoubleClick = new System.Windows.Forms.CheckBox();
            this.chMouseUp = new System.Windows.Forms.CheckBox();
            this.chMouseDown = new System.Windows.Forms.CheckBox();
            this.chMouseClick = new System.Windows.Forms.CheckBox();
            this.chMouseMove = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMousePosition
            // 
            this.lblMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMousePosition.AutoSize = true;
            this.lblMousePosition.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMousePosition.Location = new System.Drawing.Point(252, 158);
            this.lblMousePosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMousePosition.Name = "lblMousePosition";
            this.lblMousePosition.Size = new System.Drawing.Size(63, 16);
            this.lblMousePosition.TabIndex = 2;
            this.lblMousePosition.Text = "x=?; y=?";
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(0, 291);
            this.tbLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(463, 230);
            this.tbLog.TabIndex = 12;
            this.tbLog.WordWrap = false;
            // 
            // lblWheel
            // 
            this.lblWheel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWheel.AutoSize = true;
            this.lblWheel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWheel.Location = new System.Drawing.Point(252, 181);
            this.lblWheel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWheel.Name = "lblWheel";
            this.lblWheel.Size = new System.Drawing.Size(56, 16);
            this.lblWheel.TabIndex = 6;
            this.lblWheel.Text = "Wheel=?";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bDetach);
            this.groupBox2.Controls.Add(this.lblProcess);
            this.groupBox2.Controls.Add(this.bAttach);
            this.groupBox2.Controls.Add(this.chLockM);
            this.groupBox2.Controls.Add(this.chLockKB);
            this.groupBox2.Controls.Add(this.chKeyUp);
            this.groupBox2.Controls.Add(this.lblWheel);
            this.groupBox2.Controls.Add(this.chKeyPress);
            this.groupBox2.Controls.Add(this.lblMousePosition);
            this.groupBox2.Controls.Add(this.chKeyDown);
            this.groupBox2.Controls.Add(this.chMouseWheel);
            this.groupBox2.Controls.Add(this.chMouseDoubleClick);
            this.groupBox2.Controls.Add(this.chMouseUp);
            this.groupBox2.Controls.Add(this.chMouseDown);
            this.groupBox2.Controls.Add(this.chMouseClick);
            this.groupBox2.Controls.Add(this.chMouseMove);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(463, 291);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // bDetach
            // 
            this.bDetach.Enabled = false;
            this.bDetach.Location = new System.Drawing.Point(303, 234);
            this.bDetach.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bDetach.Name = "bDetach";
            this.bDetach.Size = new System.Drawing.Size(115, 27);
            this.bDetach.TabIndex = 15;
            this.bDetach.Text = "Detach";
            this.bDetach.UseVisualStyleBackColor = true;
            this.bDetach.Click += new System.EventHandler(this.bDetach_Click);
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(30, 264);
            this.lblProcess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(79, 15);
            this.lblProcess.TabIndex = 14;
            this.lblProcess.Text = "Status: Global";
            // 
            // bAttach
            // 
            this.bAttach.Location = new System.Drawing.Point(14, 234);
            this.bAttach.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bAttach.Name = "bAttach";
            this.bAttach.Size = new System.Drawing.Size(282, 27);
            this.bAttach.TabIndex = 13;
            this.bAttach.Text = "Attach to the window";
            this.bAttach.UseVisualStyleBackColor = true;
            this.bAttach.Click += new System.EventHandler(this.bAttach_Click);
            // 
            // chLockM
            // 
            this.chLockM.AutoSize = true;
            this.chLockM.ForeColor = System.Drawing.Color.Maroon;
            this.chLockM.Location = new System.Drawing.Point(15, 183);
            this.chLockM.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chLockM.Name = "chLockM";
            this.chLockM.Size = new System.Drawing.Size(185, 19);
            this.chLockM.TabIndex = 7;
            this.chLockM.Text = "LockMouse (Selected options)";
            this.chLockM.UseVisualStyleBackColor = true;
            // 
            // chLockKB
            // 
            this.chLockKB.AutoSize = true;
            this.chLockKB.ForeColor = System.Drawing.Color.Maroon;
            this.chLockKB.Location = new System.Drawing.Point(222, 105);
            this.chLockKB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chLockKB.Name = "chLockKB";
            this.chLockKB.Size = new System.Drawing.Size(199, 19);
            this.chLockKB.TabIndex = 11;
            this.chLockKB.Text = "LockKeyboard (Selected options)";
            this.chLockKB.UseVisualStyleBackColor = true;
            // 
            // chKeyUp
            // 
            this.chKeyUp.AutoSize = true;
            this.chKeyUp.Location = new System.Drawing.Point(222, 77);
            this.chKeyUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chKeyUp.Name = "chKeyUp";
            this.chKeyUp.Size = new System.Drawing.Size(60, 19);
            this.chKeyUp.TabIndex = 10;
            this.chKeyUp.Text = "KeyUp";
            this.chKeyUp.UseVisualStyleBackColor = true;
            this.chKeyUp.CheckedChanged += new System.EventHandler(this.chKeyUp_CheckedChanged);
            // 
            // chKeyPress
            // 
            this.chKeyPress.AutoSize = true;
            this.chKeyPress.Location = new System.Drawing.Point(222, 50);
            this.chKeyPress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chKeyPress.Name = "chKeyPress";
            this.chKeyPress.Size = new System.Drawing.Size(72, 19);
            this.chKeyPress.TabIndex = 9;
            this.chKeyPress.Text = "KeyPress";
            this.chKeyPress.UseVisualStyleBackColor = true;
            this.chKeyPress.CheckedChanged += new System.EventHandler(this.chKeyPress_CheckedChanged);
            // 
            // chKeyDown
            // 
            this.chKeyDown.AutoSize = true;
            this.chKeyDown.Location = new System.Drawing.Point(222, 23);
            this.chKeyDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chKeyDown.Name = "chKeyDown";
            this.chKeyDown.Size = new System.Drawing.Size(76, 19);
            this.chKeyDown.TabIndex = 8;
            this.chKeyDown.Text = "KeyDown";
            this.chKeyDown.UseVisualStyleBackColor = true;
            this.chKeyDown.CheckedChanged += new System.EventHandler(this.chKeyDown_CheckedChanged);
            // 
            // chMouseWheel
            // 
            this.chMouseWheel.AutoSize = true;
            this.chMouseWheel.Location = new System.Drawing.Point(15, 157);
            this.chMouseWheel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chMouseWheel.Name = "chMouseWheel";
            this.chMouseWheel.Size = new System.Drawing.Size(95, 19);
            this.chMouseWheel.TabIndex = 6;
            this.chMouseWheel.Text = "MouseWheel";
            this.chMouseWheel.UseVisualStyleBackColor = true;
            this.chMouseWheel.CheckedChanged += new System.EventHandler(this.chMouseWheel_CheckedChanged);
            // 
            // chMouseDoubleClick
            // 
            this.chMouseDoubleClick.AutoSize = true;
            this.chMouseDoubleClick.Location = new System.Drawing.Point(15, 103);
            this.chMouseDoubleClick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chMouseDoubleClick.Name = "chMouseDoubleClick";
            this.chMouseDoubleClick.Size = new System.Drawing.Size(126, 19);
            this.chMouseDoubleClick.TabIndex = 4;
            this.chMouseDoubleClick.Text = "MouseDoubleClick";
            this.chMouseDoubleClick.UseVisualStyleBackColor = true;
            this.chMouseDoubleClick.CheckedChanged += new System.EventHandler(this.chMouseDoubleClick_CheckedChanged);
            // 
            // chOnMouseUp
            // 
            this.chMouseUp.AutoSize = true;
            this.chMouseUp.Location = new System.Drawing.Point(15, 78);
            this.chMouseUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chMouseUp.Name = "chOnMouseUp";
            this.chMouseUp.Size = new System.Drawing.Size(77, 19);
            this.chMouseUp.TabIndex = 3;
            this.chMouseUp.Text = "MouseUp";
            this.chMouseUp.UseVisualStyleBackColor = true;
            this.chMouseUp.CheckedChanged += new System.EventHandler(this.chMouseUp_CheckedChanged);
            // 
            // chOnMouseDown
            // 
            this.chMouseDown.AutoSize = true;
            this.chMouseDown.Location = new System.Drawing.Point(15, 51);
            this.chMouseDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chMouseDown.Name = "chOnMouseDown";
            this.chMouseDown.Size = new System.Drawing.Size(93, 19);
            this.chMouseDown.TabIndex = 2;
            this.chMouseDown.Text = "MouseDown";
            this.chMouseDown.UseVisualStyleBackColor = true;
            this.chMouseDown.CheckedChanged += new System.EventHandler(this.chMouseDown_CheckedChanged);
            // 
            // chOnMouseClick
            // 
            this.chMouseClick.AutoSize = true;
            this.chMouseClick.Location = new System.Drawing.Point(15, 23);
            this.chMouseClick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chMouseClick.Name = "chOnMouseClick";
            this.chMouseClick.Size = new System.Drawing.Size(88, 19);
            this.chMouseClick.TabIndex = 1;
            this.chMouseClick.Text = "MouseClick";
            this.chMouseClick.UseVisualStyleBackColor = true;
            this.chMouseClick.CheckedChanged += new System.EventHandler(this.chMouseClick_CheckedChanged);
            // 
            // chOnMouseMove
            // 
            this.chMouseMove.AutoSize = true;
            this.chMouseMove.Location = new System.Drawing.Point(15, 130);
            this.chMouseMove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chMouseMove.Name = "chOnMouseMove";
            this.chMouseMove.Size = new System.Drawing.Size(92, 19);
            this.chMouseMove.TabIndex = 5;
            this.chMouseMove.Text = "MouseMove";
            this.chMouseMove.UseVisualStyleBackColor = true;
            this.chMouseMove.CheckedChanged += new System.EventHandler(this.chMouseMove_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 521);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "GlobalHook WinForms Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMousePosition;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Label lblWheel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chKeyUp;
        private System.Windows.Forms.CheckBox chKeyPress;
        private System.Windows.Forms.CheckBox chKeyDown;
        private System.Windows.Forms.CheckBox chMouseWheel;
        private System.Windows.Forms.CheckBox chMouseDoubleClick;
        private System.Windows.Forms.CheckBox chMouseUp;
        private System.Windows.Forms.CheckBox chMouseDown;
        private System.Windows.Forms.CheckBox chMouseClick;
        private System.Windows.Forms.CheckBox chMouseMove;
        private System.Windows.Forms.CheckBox chLockKB;
        private System.Windows.Forms.CheckBox chLockM;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Button bAttach;
        private System.Windows.Forms.Button bDetach;
    }
}

