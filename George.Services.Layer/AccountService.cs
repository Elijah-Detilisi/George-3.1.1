using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Services.Layer
{
    using George.Email.Stream;
    using George.Data.Layer.DataModel;
    using George.Data.Layer.DataAccess;

    public class AccountService
    {
        #region Instances
        private readonly Inbox _emailInbox;
        private readonly OutBox _emailOutbox;
        private readonly DataAccess _dataAccess;
        #endregion

        public AccountService()
        {
            _dataAccess = new DataAccess();
            _emailInbox = new Inbox();
            _emailOutbox = new OutBox();   
        }

        #region Authentication Service Methods
        public Boolean LoginToInbox(string emailAddress, string password)
        {
            var isSuccess = true;
            try
            {
                _emailInbox.LoginToEmail(emailAddress, password);
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
            try
            {
                _emailOutbox.LoginToEmail(emailAddress, password);
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
