
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
    public partial class SecurityInput : UserControl
    {
        #region Instances
        private Object _emailClient;
        private string _emailAddress;
        private string _password;
        private Action _nextAction;
        #endregion

        public SecurityInput()
        {
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
        private void IsEmailValid()
        {
            ExtractCredentials();
        }
        #endregion

        #region Event Handlers
        private void nextbutton_Click(object sender, EventArgs e)
        {

            _nextAction();
        }
        #endregion
    }
}