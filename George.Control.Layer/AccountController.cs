using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Control.Layer
{
    using George.Data.Layer.DataModel;
    using George.Data.Layer.DataAccess;
    using George.Services.Layer.EmailService;

    public class AccountController
    {
        #region Instances
        private readonly DataAccess _dataAccess;
        private readonly EmailInbox _emailInbox;
        private readonly EmailOutBox _emailOutbox;
        
        #endregion

        public AccountController()
        {
            _dataAccess = new DataAccess();
            _emailInbox = new EmailInbox();
            _emailOutbox = new EmailOutBox();
        }

        #region Authentication Service Methods
        public async Task<Boolean> LoginToInbox(string emailAddress, string password)
        {
            var isSuccess = true;
            var emailSettings = await _dataAccess.GetEmailSettingsAsync(emailAddress);
            var userAccount = new UserAccount() {
                EmailAddress = emailAddress,
                EmailPassword = password,
                SmtpHostName = emailSettings.SmtpHostName,
                SmtpPortNumber = emailSettings.SmtpPortNumber,
                Pop3HostName = emailSettings.Pop3HostName,
                Pop3PortNumber = emailSettings.Pop3PortNumber
            };
            
            try
            {
                _emailInbox.LoginToEmail(userAccount);
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public async Task<Boolean> LoginToOutbox(string emailAddress, string password)
        {
            var isSuccess = true;
            var emailSettings = await _dataAccess.GetEmailSettingsAsync(emailAddress);
            var userAccount = new UserAccount()
            {
                EmailAddress = emailAddress,
                EmailPassword = password,
                SmtpHostName = emailSettings.SmtpHostName,
                SmtpPortNumber = emailSettings.SmtpPortNumber,
                Pop3HostName = emailSettings.Pop3HostName,
                Pop3PortNumber = emailSettings.Pop3PortNumber
            };

            try
            {
                _emailOutbox.LoginToEmail(userAccount);
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
        #endregion

        #region Data Ops Service Methods
        public void CreateNewAccount(string emailAddress, string password)
        {
            _ = _dataAccess.SaveUserAccountAsync(emailAddress, password);
        }

        public async Task<UserAccount> GetUserAccount(int accountId)
        {
            return await _dataAccess.GetUserAccountAsync(accountId);
        }
        #endregion
    }
}
