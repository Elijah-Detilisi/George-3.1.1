
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace George.GUI.CustomBuilds.SecurityForm.UtilityControls
{
    using George.Email.Stream;

    public partial class SecurityInput : UserControl
    {
        #region Instances
        private string _emailAddress;
        private string _password;
        private Action _nextAction;
        private readonly Inbox _emailClient;
        #endregion

        public SecurityInput()
        {
            _emailClient = new Inbox();
            InitializeComponent();
        }

        #region Setter and Getter Methods
        public void SetNextAction(Action action)
        {
            _nextAction = action;
        }
        private void ExtractCredentials()
        {
            _emailAddress = emailTextBox.Text;
            _password = pwTextBox.Text;
        }
        #endregion

        #region Registration Methods
        private Boolean IsEmailValid()
        {
            var result = true;
            try
            {
                ExtractCredentials();
                _emailClient.LoginToEmail(_emailAddress, _password);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
        #endregion

        #region Event Handlers
        private void nextbutton_Click(object sender, EventArgs e)
        {
            if (IsEmailValid())
            {
                //store credentials into database
                Errorlabel.Hide();
                _nextAction();
            }
            else
            {
                Errorlabel.Show();
                //repeat ask email prompt
            }
        }
        #endregion
    }
}