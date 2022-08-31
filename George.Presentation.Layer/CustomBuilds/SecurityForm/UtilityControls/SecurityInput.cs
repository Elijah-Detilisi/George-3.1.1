
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
        #endregion

        #region Event Handlers
        private void nextbutton_Click(object sender, EventArgs e)
        {
            var emailAddress = emailTextBox.Text;
            var password = pwTextBox.Text;

            this.nextbutton.Hide();
            this.progressBar.Show();

            Task.Run(() =>
            {
                if (_accountController.LoginToInbox(emailAddress, password))
                {
                    _accountController.CreateNewAccount(emailAddress, password);
                    
                    this.progressBar.Invoke((MethodInvoker)(() =>
                    {
                        this.progressBar.Hide();
                        Errorlabel.Hide();
                    }));

                    _nextAction();
                }
                else
                {
                    this.progressBar.Invoke((MethodInvoker)(() =>
                    {
                        this.progressBar.Hide();
                        Errorlabel.Show();
                    }));
                    
                    //repeat ask email prompt
                }

                this.nextbutton.Invoke((MethodInvoker)(() =>
                {
                    this.nextbutton.Show();
                }));
                
            });
        }
        #endregion
    }
}