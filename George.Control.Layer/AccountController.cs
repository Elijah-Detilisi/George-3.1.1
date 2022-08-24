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
    using George.Services.Layer.EncryptionService;

    public class AccountController
    {
        #region Instances
        private readonly string _key;
        private readonly DataAccess _dataAccess;
        private readonly EmailInbox _emailInbox;
        private readonly EmailOutBox _emailOutbox;
        #endregion

        public AccountController()
        {
            _dataAccess = new DataAccess();
            _emailInbox = new EmailInbox();
            _emailOutbox = new EmailOutBox();
            _key = TripleDES_Encryption.GetEncryptionKey("Detilisi");
        }

        #region Authentication Service Methods
        public Boolean LoginToInbox(string emailAddress, string password)
        {
            var isSuccess = true;
            var credentials = GetCredentials(emailAddress, password);
            try
            {
                _emailInbox.LoginToEmail(credentials);
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public Boolean LoginToOutbox(string emailAddress, string password)
        {
            var isSuccess = true;
            var credentials = GetCredentials(emailAddress, password);
            try
            {
                _emailOutbox.LoginToEmail(credentials);
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
        private UserAccount GetCredentials(string emailAddress, string password)
        {
            var emailSettings = _dataAccess.GetEmailSettingsAsync(emailAddress);
            var userAccount = new UserAccount()
            {
                EmailAddress = emailAddress,
                EmailPassword = password,
                SmtpHostName = emailSettings.SmtpHostName,
                SmtpPortNumber = emailSettings.SmtpPortNumber,
                Pop3HostName = emailSettings.Pop3HostName,
                Pop3PortNumber = emailSettings.Pop3PortNumber
            };

            return userAccount;
        }
        #endregion

        #region Data Ops Service Methods
        public async void CreateNewAccount(string emailAddress, string password)
        {
            password = TripleDES_Encryption.Encrypt(password, _key);
            await _dataAccess.SaveUserAccountAsync(emailAddress, password);
        }

        public async Task<UserAccount> GetUserAccount(int accountId)
        {
            var userAccount = await _dataAccess.GetUserAccountAsync(accountId);
            userAccount.EmailPassword = TripleDES_Encryption.Decrypt(userAccount.EmailPassword, _key);

            return userAccount;
        }
        #endregion
    }
}
