using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace George.Presentation.Layer.CustomBuilds.SecurityForm
{
    using George.Presentation.Layer.CustomBuilds.SecurityForm.UtilityControls;
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
            Shown += SecurityForm_Shown;
        }

        private void SecurityForm_Shown(object? sender, EventArgs e)
        {
            InitializeSecurityForm();
        }

        #region Final Display Methods
        public void DisplayLoginForm()
        {
            DisplayLoginBanner();
            DisplayLoginVideoDisplay();
        }
        public void DisplaySignUpForm()
        {
            DisplaySignUpBanner();
            DisplaySecurityInput();
            _securityInput.ExecuteProtocol();
        }
        public void InitializeSecurityForm()
        {
            if (_videoDisplay.GetIsTrained())
            {
                DisplayLoginForm();
            }
            else
            {
                this.utilityButton.Enabled = false;
                DisplaySignUpForm();
            }
        }
        #endregion

        #region Banner Display Methods
        private void DisplaySignUpBanner()
        {
            _isLogin = false;
            this.utilityButton.Text = "Login->";
            this.pictureBox.Image = global::George.Presentation.Layer.Properties.Resources.signUp;
            this.bannerPanel.BackColor = Color.FromArgb(
                ((int)(((byte)(19)))),
                ((int)(((byte)(25)))),
                ((int)(((byte)(40))))
            );
        }
        private void DisplayLoginBanner()
        {
            _isLogin = true;
            this.utilityButton.Text = "SignUp->";
            this.pictureBox.Image = global::George.Presentation.Layer.Properties.Resources.ai;
            this.bannerPanel.BackColor = Color.FromArgb(
                ((int)(((byte)(29)))),
                ((int)(((byte)(98)))),
                ((int)(((byte)(155))))
            );
        }
        #endregion

        #region Video Display Methods
        private void DisplayVideoFeed()
        {
            _securityInput.Hide();
            _videoDisplay.ClearFeedDisplay();
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

        #region Utility Display Methods
        private void DisplaySecurityInput()
        {
            StopVideoDisplay();
            _securityInput.Dock = DockStyle.None;
            _securityInput.Location = new Point(0, 100);
            _securityInput.Size = new Size(420, 350);
            _securityInput.TabIndex = 2;
            _securityInput.Show();

            this.Controls.Add(this._securityInput);
        }
        private void DisplaySignUpVideoDisplay()
        {
            _videoDisplay.SetProgressText("Face Scanning:");
            _videoDisplay.SetVideoProcedureStep(1);
            DisplayVideoFeed();
        }
        private void DisplayLoginVideoDisplay()
        {
            _videoDisplay.SetProgressText("Login Verification:");
            _videoDisplay.SetVideoProcedureStep(2);
            DisplayVideoFeed();
        }
        #endregion

        #region Event Handlers Methods
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void utilityButton_Click(object sender, EventArgs e)
        {
            if (_isLogin)
            {
                DisplaySignUpForm();
            }
            else
            {
                DisplayLoginForm();
            }
        }
        #endregion

    }
}