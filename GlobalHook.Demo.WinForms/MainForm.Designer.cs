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
            this.labelMousePosition = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.labelWheel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chbLockM = new System.Windows.Forms.CheckBox();
            this.chbLockKB = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyUp = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyPress = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyDown = new System.Windows.Forms.CheckBox();
            this.checkBoxMouseWheel = new System.Windows.Forms.CheckBox();
            this.checkBoxMouseDoubleClick = new System.Windows.Forms.CheckBox();
            this.checkBoxOnMouseUp = new System.Windows.Forms.CheckBox();
            this.checkBoxOnMouseDown = new System.Windows.Forms.CheckBox();
            this.checkBoxOnMouseClick = new System.Windows.Forms.CheckBox();
            this.checkBoxOnMouseMove = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.AutoSize = true;
            this.labelMousePosition.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMousePosition.Location = new System.Drawing.Point(222, 158);
            this.labelMousePosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(63, 16);
            this.labelMousePosition.TabIndex = 2;
            this.labelMousePosition.Text = "x=?; y=?";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(0, 291);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(433, 209);
            this.textBoxLog.TabIndex = 12;
            this.textBoxLog.WordWrap = false;
            // 
            // labelWheel
            // 
            this.labelWheel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWheel.AutoSize = true;
            this.labelWheel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelWheel.Location = new System.Drawing.Point(222, 181);
            this.labelWheel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelWheel.Name = "labelWheel";
            this.labelWheel.Size = new System.Drawing.Size(56, 16);
            this.labelWheel.TabIndex = 6;
            this.labelWheel.Text = "Wheel=?";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.chbLockM);
            this.groupBox2.Controls.Add(this.chbLockKB);
            this.groupBox2.Controls.Add(this.checkBoxKeyUp);
            this.groupBox2.Controls.Add(this.labelWheel);
            this.groupBox2.Controls.Add(this.checkBoxKeyPress);
            this.groupBox2.Controls.Add(this.labelMousePosition);
            this.groupBox2.Controls.Add(this.checkBoxKeyDown);
            this.groupBox2.Controls.Add(this.checkBoxMouseWheel);
            this.groupBox2.Controls.Add(this.checkBoxMouseDoubleClick);
            this.groupBox2.Controls.Add(this.checkBoxOnMouseUp);
            this.groupBox2.Controls.Add(this.checkBoxOnMouseDown);
            this.groupBox2.Controls.Add(this.checkBoxOnMouseClick);
            this.groupBox2.Controls.Add(this.checkBoxOnMouseMove);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(433, 291);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(303, 234);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 27);
            this.button2.TabIndex = 15;
            this.button2.Text = "Detach";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 264);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Status: Global";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 234);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(282, 27);
            this.button1.TabIndex = 13;
            this.button1.Text = "Attach to the window";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbLockM
            // 
            this.chbLockM.AutoSize = true;
            this.chbLockM.ForeColor = System.Drawing.Color.Maroon;
            this.chbLockM.Location = new System.Drawing.Point(15, 183);
            this.chbLockM.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chbLockM.Name = "chbLockM";
            this.chbLockM.Size = new System.Drawing.Size(185, 19);
            this.chbLockM.TabIndex = 7;
            this.chbLockM.Text = "LockMouse (Selected options)";
            this.chbLockM.UseVisualStyleBackColor = true;
            // 
            // chbLockKB
            // 
            this.chbLockKB.AutoSize = true;
            this.chbLockKB.ForeColor = System.Drawing.Color.Maroon;
            this.chbLockKB.Location = new System.Drawing.Point(222, 105);
            this.chbLockKB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chbLockKB.Name = "chbLockKB";
            this.chbLockKB.Size = new System.Drawing.Size(199, 19);
            this.chbLockKB.TabIndex = 11;
            this.chbLockKB.Text = "LockKeyboard (Selected options)";
            this.chbLockKB.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyUp
            // 
            this.checkBoxKeyUp.AutoSize = true;
            this.checkBoxKeyUp.Location = new System.Drawing.Point(222, 77);
            this.checkBoxKeyUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxKeyUp.Name = "checkBoxKeyUp";
            this.checkBoxKeyUp.Size = new System.Drawing.Size(60, 19);
            this.checkBoxKeyUp.TabIndex = 10;
            this.checkBoxKeyUp.Text = "KeyUp";
            this.checkBoxKeyUp.UseVisualStyleBackColor = true;
            this.checkBoxKeyUp.CheckedChanged += new System.EventHandler(this.checkBoxKeyUp_CheckedChanged);
            // 
            // checkBoxKeyPress
            // 
            this.checkBoxKeyPress.AutoSize = true;
            this.checkBoxKeyPress.Location = new System.Drawing.Point(222, 50);
            this.checkBoxKeyPress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxKeyPress.Name = "checkBoxKeyPress";
            this.checkBoxKeyPress.Size = new System.Drawing.Size(72, 19);
            this.checkBoxKeyPress.TabIndex = 9;
            this.checkBoxKeyPress.Text = "KeyPress";
            this.checkBoxKeyPress.UseVisualStyleBackColor = true;
            this.checkBoxKeyPress.CheckedChanged += new System.EventHandler(this.checkBoxKeyPress_CheckedChanged);
            // 
            // checkBoxKeyDown
            // 
            this.checkBoxKeyDown.AutoSize = true;
            this.checkBoxKeyDown.Location = new System.Drawing.Point(222, 23);
            this.checkBoxKeyDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxKeyDown.Name = "checkBoxKeyDown";
            this.checkBoxKeyDown.Size = new System.Drawing.Size(76, 19);
            this.checkBoxKeyDown.TabIndex = 8;
            this.checkBoxKeyDown.Text = "KeyDown";
            this.checkBoxKeyDown.UseVisualStyleBackColor = true;
            this.checkBoxKeyDown.CheckedChanged += new System.EventHandler(this.checkBoxKeyDown_CheckedChanged);
            // 
            // checkBoxMouseWheel
            // 
            this.checkBoxMouseWheel.AutoSize = true;
            this.checkBoxMouseWheel.Location = new System.Drawing.Point(15, 157);
            this.checkBoxMouseWheel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxMouseWheel.Name = "checkBoxMouseWheel";
            this.checkBoxMouseWheel.Size = new System.Drawing.Size(95, 19);
            this.checkBoxMouseWheel.TabIndex = 6;
            this.checkBoxMouseWheel.Text = "MouseWheel";
            this.checkBoxMouseWheel.UseVisualStyleBackColor = true;
            this.checkBoxMouseWheel.CheckedChanged += new System.EventHandler(this.checkBoxMouseWheel_CheckedChanged);
            // 
            // checkBoxMouseDoubleClick
            // 
            this.checkBoxMouseDoubleClick.AutoSize = true;
            this.checkBoxMouseDoubleClick.Location = new System.Drawing.Point(15, 103);
            this.checkBoxMouseDoubleClick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxMouseDoubleClick.Name = "checkBoxMouseDoubleClick";
            this.checkBoxMouseDoubleClick.Size = new System.Drawing.Size(126, 19);
            this.checkBoxMouseDoubleClick.TabIndex = 4;
            this.checkBoxMouseDoubleClick.Text = "MouseDoubleClick";
            this.checkBoxMouseDoubleClick.UseVisualStyleBackColor = true;
            this.checkBoxMouseDoubleClick.CheckedChanged += new System.EventHandler(this.checkBoxMouseDoubleClick_CheckedChanged);
            // 
            // checkBoxOnMouseUp
            // 
            this.checkBoxOnMouseUp.AutoSize = true;
            this.checkBoxOnMouseUp.Location = new System.Drawing.Point(15, 78);
            this.checkBoxOnMouseUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxOnMouseUp.Name = "checkBoxOnMouseUp";
            this.checkBoxOnMouseUp.Size = new System.Drawing.Size(77, 19);
            this.checkBoxOnMouseUp.TabIndex = 3;
            this.checkBoxOnMouseUp.Text = "MouseUp";
            this.checkBoxOnMouseUp.UseVisualStyleBackColor = true;
            this.checkBoxOnMouseUp.CheckedChanged += new System.EventHandler(this.checkBoxOnMouseUp_CheckedChanged);
            // 
            // checkBoxOnMouseDown
            // 
            this.checkBoxOnMouseDown.AutoSize = true;
            this.checkBoxOnMouseDown.Location = new System.Drawing.Point(15, 51);
            this.checkBoxOnMouseDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxOnMouseDown.Name = "checkBoxOnMouseDown";
            this.checkBoxOnMouseDown.Size = new System.Drawing.Size(93, 19);
            this.checkBoxOnMouseDown.TabIndex = 2;
            this.checkBoxOnMouseDown.Text = "MouseDown";
            this.checkBoxOnMouseDown.UseVisualStyleBackColor = true;
            this.checkBoxOnMouseDown.CheckedChanged += new System.EventHandler(this.checkBoxOnMouseDown_CheckedChanged);
            // 
            // checkBoxOnMouseClick
            // 
            this.checkBoxOnMouseClick.AutoSize = true;
            this.checkBoxOnMouseClick.Location = new System.Drawing.Point(15, 23);
            this.checkBoxOnMouseClick.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxOnMouseClick.Name = "checkBoxOnMouseClick";
            this.checkBoxOnMouseClick.Size = new System.Drawing.Size(88, 19);
            this.checkBoxOnMouseClick.TabIndex = 1;
            this.checkBoxOnMouseClick.Text = "MouseClick";
            this.checkBoxOnMouseClick.UseVisualStyleBackColor = true;
            this.checkBoxOnMouseClick.CheckedChanged += new System.EventHandler(this.checkBoxOnMouseClick_CheckedChanged);
            // 
            // checkBoxOnMouseMove
            // 
            this.checkBoxOnMouseMove.AutoSize = true;
            this.checkBoxOnMouseMove.Location = new System.Drawing.Point(15, 130);
            this.checkBoxOnMouseMove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxOnMouseMove.Name = "checkBoxOnMouseMove";
            this.checkBoxOnMouseMove.Size = new System.Drawing.Size(92, 19);
            this.checkBoxOnMouseMove.TabIndex = 5;
            this.checkBoxOnMouseMove.Text = "MouseMove";
            this.checkBoxOnMouseMove.UseVisualStyleBackColor = true;
            this.checkBoxOnMouseMove.CheckedChanged += new System.EventHandler(this.checkBoxOnMouseMove_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 500);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Test for the class HookManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMousePosition;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelWheel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxKeyUp;
        private System.Windows.Forms.CheckBox checkBoxKeyPress;
        private System.Windows.Forms.CheckBox checkBoxKeyDown;
        private System.Windows.Forms.CheckBox checkBoxMouseWheel;
        private System.Windows.Forms.CheckBox checkBoxMouseDoubleClick;
        private System.Windows.Forms.CheckBox checkBoxOnMouseUp;
        private System.Windows.Forms.CheckBox checkBoxOnMouseDown;
        private System.Windows.Forms.CheckBox checkBoxOnMouseClick;
        private System.Windows.Forms.CheckBox checkBoxOnMouseMove;
        private System.Windows.Forms.CheckBox chbLockKB;
        private System.Windows.Forms.CheckBox chbLockM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

