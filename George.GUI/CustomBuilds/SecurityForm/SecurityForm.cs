using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace George.GUI.CustomBuilds.SecurityForm
{
    using George.GUI.CustomBuilds.SecurityForm.UtilityControls;

    public partial class SecurityForm : Form
    {
        #region Instances
        private SecurityInput _securityInput;
        private VideoDisplay _videoDisplay;
        private bool _isLogin;
        #endregion

        public SecurityForm()
        {
            _isLogin = true;
            _videoDisplay = new VideoDisplay();
            _securityInput = new SecurityInput();

            _securityInput.SetNextAction(DisplaySignUpVideoDisplay);
            InitializeComponent();
            DisplayLoginForm();

        }

        #region Event Handlers Methods
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void utilityButton_Click(object sender, EventArgs e)
        {
            if (_isLogin)
            {
                DisplaySignUpBanner();
                DisplaySecurityInput();
            }
            else
            {
                DisplayLoginForm();
            }
            Console.WriteLine("Move");
        }
        #endregion

        #region Banner Display Methods
        public void DisplaySignUpBanner()
        {
            _isLogin = false;

            this.utilityButton.Text = "Login->";
            this.pictureBox.Image = global::George.GUI.Properties.Resources.signUp;
            this.bannerPanel.BackColor = Color.FromArgb(
                ((int)(((byte)(19)))), 
                ((int)(((byte)(25)))), 
                ((int)(((byte)(40))))
            );
        }
        public void DisplayLoginBanner()
        {
            _isLogin = true;

            this.utilityButton.Text = "SignUp->";
            this.pictureBox.Image = global::George.GUI.Properties.Resources.ai;
        }
        #endregion

        #region Utility Display Methods
        private void DisplaySecurityInput()
        {
            StopVideoDisplay();
            _securityInput.Dock = DockStyle.None;
            _securityInput.Location = new System.Drawing.Point(0, 100);
            _securityInput.Size = new System.Drawing.Size(420, 350);
            _securityInput.TabIndex = 2;
            _securityInput.Show();

            this.Controls.Add(this._securityInput);
        }
        private void DisplaySignUpVideoDisplay()
        {
            //_videoDisplay.SetProgressText("Scanning:");
            DisplayVideoFeed();
        }
        private void DisplayLoginVideoDisplay()
        {
            //_videoDisplay.SetProgressText("Verification:");
            DisplayVideoFeed();
        }
        #endregion

        #region Video Display Methods
        private void DisplayVideoFeed()
        {
            _securityInput.Hide();
            _videoDisplay.ResumeVideoFeed();

            _videoDisplay.Dock = DockStyle.Bottom;
            _videoDisplay.Location = new System.Drawing.Point(0, 100);
            _videoDisplay.Size = new System.Drawing.Size(420, 340);
            _videoDisplay.TabIndex = 2;
            _videoDisplay.Show();

            this.Controls.Add(this._videoDisplay);

        }
        private void StopVideoDisplay()
        {
            _videoDisplay.StopVideoFeed();
            _videoDisplay.Hide();
        }
        #endregion

        public void DisplayLoginForm()
        {
            DisplayLoginBanner();
            DisplayLoginVideoDisplay();
        }
    }
}
