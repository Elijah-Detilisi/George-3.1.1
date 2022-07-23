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
        public string GetEmailAddress()
        {
            _emailAddress = emailTextBox.Text;
            return _emailAddress;
        }
        public string GetPassword()
        {
            _password = pwTextBox.Text;
            return _password;
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
