namespace George.GUI.CustomBuilds.SecurityForm.UtilityControls
{
    partial class SecurityInput
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputPanel = new System.Windows.Forms.Panel();
            this.Errorlabel = new System.Windows.Forms.Label();
            this.nextbutton = new System.Windows.Forms.Button();
            this.pwPictureBox = new System.Windows.Forms.PictureBox();
            this.dividerLabel2 = new System.Windows.Forms.Label();
            this.pwLabel = new System.Windows.Forms.Label();
            this.pwTextBox = new System.Windows.Forms.TextBox();
            this.emailPictureBox = new System.Windows.Forms.PictureBox();
            this.dividerLabel1 = new System.Windows.Forms.Label();
            this.emailAddressLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.inputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pwPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPanel
            // 
            this.inputPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputPanel.Controls.Add(this.Errorlabel);
            this.inputPanel.Controls.Add(this.nextbutton);
            this.inputPanel.Controls.Add(this.pwPictureBox);
            this.inputPanel.Controls.Add(this.dividerLabel2);
            this.inputPanel.Controls.Add(this.pwLabel);
            this.inputPanel.Controls.Add(this.pwTextBox);
            this.inputPanel.Controls.Add(this.emailPictureBox);
            this.inputPanel.Controls.Add(this.dividerLabel1);
            this.inputPanel.Controls.Add(this.emailAddressLabel);
            this.inputPanel.Controls.Add(this.emailTextBox);
            this.inputPanel.Location = new System.Drawing.Point(42, 17);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(334, 319);
            this.inputPanel.TabIndex = 0;
            // 
            // Errorlabel
            // 
            this.Errorlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Errorlabel.AutoSize = true;
            this.Errorlabel.BackColor = System.Drawing.Color.Maroon;
            this.Errorlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Errorlabel.ForeColor = System.Drawing.Color.Silver;
            this.Errorlabel.Location = new System.Drawing.Point(43, 206);
            this.Errorlabel.Name = "Errorlabel";
            this.Errorlabel.Size = new System.Drawing.Size(212, 24);
            this.Errorlabel.TabIndex = 20;
            this.Errorlabel.Text = "Invalid Email Credintials!";
            this.Errorlabel.Visible = false;
            // 
            // nextbutton
            // 
            this.nextbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextbutton.BackColor = System.Drawing.Color.SteelBlue;
            this.nextbutton.FlatAppearance.BorderSize = 0;
            this.nextbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextbutton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nextbutton.ForeColor = System.Drawing.Color.Silver;
            this.nextbutton.Location = new System.Drawing.Point(44, 250);
            this.nextbutton.Name = "nextbutton";
            this.nextbutton.Size = new System.Drawing.Size(266, 47);
            this.nextbutton.TabIndex = 19;
            this.nextbutton.Text = "NEXT";
            this.nextbutton.UseVisualStyleBackColor = false;
            this.nextbutton.Click += new System.EventHandler(this.nextbutton_Click);
            // 
            // pwPictureBox
            // 
            this.pwPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pwPictureBox.Image = global::George.GUI.Properties.Resources._lock;
            this.pwPictureBox.Location = new System.Drawing.Point(44, 99);
            this.pwPictureBox.Name = "pwPictureBox";
            this.pwPictureBox.Size = new System.Drawing.Size(43, 37);
            this.pwPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pwPictureBox.TabIndex = 18;
            this.pwPictureBox.TabStop = false;
            // 
            // dividerLabel2
            // 
            this.dividerLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dividerLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dividerLabel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dividerLabel2.ForeColor = System.Drawing.Color.LightGray;
            this.dividerLabel2.Location = new System.Drawing.Point(44, 168);
            this.dividerLabel2.Name = "dividerLabel2";
            this.dividerLabel2.Size = new System.Drawing.Size(266, 2);
            this.dividerLabel2.TabIndex = 17;
            // 
            // pwLabel
            // 
            this.pwLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pwLabel.AutoSize = true;
            this.pwLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pwLabel.ForeColor = System.Drawing.Color.Silver;
            this.pwLabel.Location = new System.Drawing.Point(89, 106);
            this.pwLabel.Name = "pwLabel";
            this.pwLabel.Size = new System.Drawing.Size(106, 25);
            this.pwLabel.TabIndex = 16;
            this.pwLabel.Text = "Password";
            // 
            // pwTextBox
            // 
            this.pwTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pwTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(98)))), ((int)(((byte)(155)))));
            this.pwTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pwTextBox.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.pwTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(192)))), ((int)(((byte)(204)))));
            this.pwTextBox.Location = new System.Drawing.Point(44, 147);
            this.pwTextBox.Name = "pwTextBox";
            this.pwTextBox.PasswordChar = '*';
            this.pwTextBox.Size = new System.Drawing.Size(266, 18);
            this.pwTextBox.TabIndex = 15;
            this.pwTextBox.Text = "password";
            // 
            // emailPictureBox
            // 
            this.emailPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailPictureBox.Image = global::George.GUI.Properties.Resources.arroba;
            this.emailPictureBox.Location = new System.Drawing.Point(44, 13);
            this.emailPictureBox.Name = "emailPictureBox";
            this.emailPictureBox.Size = new System.Drawing.Size(43, 37);
            this.emailPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.emailPictureBox.TabIndex = 14;
            this.emailPictureBox.TabStop = false;
            // 
            // dividerLabel1
            // 
            this.dividerLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dividerLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dividerLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dividerLabel1.ForeColor = System.Drawing.Color.LightGray;
            this.dividerLabel1.Location = new System.Drawing.Point(44, 83);
            this.dividerLabel1.Name = "dividerLabel1";
            this.dividerLabel1.Size = new System.Drawing.Size(266, 2);
            this.dividerLabel1.TabIndex = 13;
            // 
            // emailAddressLabel
            // 
            this.emailAddressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailAddressLabel.AutoSize = true;
            this.emailAddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.emailAddressLabel.ForeColor = System.Drawing.Color.Silver;
            this.emailAddressLabel.Location = new System.Drawing.Point(89, 20);
            this.emailAddressLabel.Name = "emailAddressLabel";
            this.emailAddressLabel.Size = new System.Drawing.Size(150, 25);
            this.emailAddressLabel.TabIndex = 12;
            this.emailAddressLabel.Text = "Email Address";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emailTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(98)))), ((int)(((byte)(155)))));
            this.emailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.emailTextBox.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.emailTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(192)))), ((int)(((byte)(204)))));
            this.emailTextBox.Location = new System.Drawing.Point(44, 58);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(266, 18);
            this.emailTextBox.TabIndex = 11;
            this.emailTextBox.Text = "user@gmail.com";
            // 
            // SecurityInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(98)))), ((int)(((byte)(155)))));
            this.Controls.Add(this.inputPanel);
            this.Name = "SecurityInput";
            this.Size = new System.Drawing.Size(417, 410);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pwPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel inputPanel;
        private Button nextbutton;
        private PictureBox pwPictureBox;
        private Label dividerLabel2;
        private Label pwLabel;
        private TextBox pwTextBox;
        private PictureBox emailPictureBox;
        private Label dividerLabel1;
        private Label emailAddressLabel;
        private TextBox emailTextBox;
        private Label Errorlabel;
    }
}
