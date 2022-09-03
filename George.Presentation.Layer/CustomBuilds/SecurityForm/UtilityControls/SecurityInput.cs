
namespace George.Presentation.Layer.CustomBuilds.SecurityForm.UtilityControls
{
    using George.Control.Layer;
    using System.ComponentModel;

    public partial class SecurityInput : UserControl
    {
        #region Instances
        private Action _nextAction;
        private readonly AccountController _accountController;
        private readonly AudioController _audioController;
        #endregion

        public SecurityInput()
        {
            _audioController = new AudioController();
            _accountController = new AccountController();

            InitializeComponent();
        }

        #region Event Handlers
        private void nextbutton_Click(object sender, EventArgs e)
        {
            this.VerifyUserAuthentication();
        }
        #endregion

        #region Setter and Getter Methods
        public void SetNextAction(Action action)
        {
            _nextAction = action;
        }
        #endregion

        #region Protocol methods
        private void GetUserAuthentication()
        {
            Invoke((MethodInvoker)(() =>
            {
                this.Errorlabel.Hide();
            }));

            _audioController.Speak("Setting: Greeting");
            _audioController.Speak("Setting: Sign-Up Intro");
            _audioController.Speak("Setting: Email request");
            //var emailTextBox.Text = _audioController.GetUserInput();
            _audioController.Speak("Setting: Password request");
            //var email = _audioController.GetUserInput();

            this.VerifyUserAuthentication();
        }

        private void VerifyUserAuthentication()
        {
            var emailAddress = emailTextBox.Text;
            var password = pwTextBox.Text;

            Task.Run(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    this.nextbutton.Hide();
                    this.progressBar.Show();
                }));

                _audioController.Speak("Setting: Authentication verification");

                if (_accountController.LoginToInbox(emailAddress, password))
                {
                    _accountController.CreateNewAccount(emailAddress, password);
                    Invoke((MethodInvoker)(() =>
                    {
                        this.progressBar.Hide();
                        this.Errorlabel.Hide();
                    }));

                    _nextAction();
                }
                else
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        this.progressBar.Hide();
                        this.Errorlabel.Show();
                        this.nextbutton.Show();
                    }));
                    
                    //repeat authentication prompt
                    _audioController.Speak("Setting: Invalid Authentication report");
                    this.GetUserAuthentication();
                }
            });
        }
        #endregion

        #region Background worker methods
        private void signUpbgWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.GetUserAuthentication();
        }
        public void ExecuteProtocol()
        {
            signUpbgWorker1.RunWorkerAsync();
        }
        #endregion

    }
}