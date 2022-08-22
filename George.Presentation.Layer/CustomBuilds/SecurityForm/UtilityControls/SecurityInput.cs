
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace George.Presentation.Layer.CustomBuilds.SecurityForm.UtilityControls
{
    using George.Control.Layer;

    public partial class SecurityInput : UserControl
    {
        #region Instances
        private Action _nextAction;
        private string _password;
        private string _emailAddress;
        private readonly AccountController _accountController;
        #endregion

        public SecurityInput()
        {
            _accountController = new AccountController();
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

        #region Event Handlers
        private void nextbutton_Click(object sender, EventArgs e)
        {
            ExtractCredentials();
            
            if (_accountController.LoginToInbox(_emailAddress, _password)) //login success
            {
                _accountController.CreateNewAccount(_emailAddress, _password);
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