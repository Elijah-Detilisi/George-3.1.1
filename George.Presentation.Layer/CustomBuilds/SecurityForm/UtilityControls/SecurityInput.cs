
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
    using George.Services.Layer;

    public partial class SecurityInput : UserControl
    {
        #region Instances
        private Action _nextAction;
        private string _password;
        private string _emailAddress;
        private readonly AccountService _accountService;
        #endregion

        public SecurityInput()
        {
            _accountService = new AccountService();
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
            
            if (_accountService.LoginToInbox(_emailAddress, _password)) //login success
            {
                _accountService.CreateNewAccount(_emailAddress, _password);
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