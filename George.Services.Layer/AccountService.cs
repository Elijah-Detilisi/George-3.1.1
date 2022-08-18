using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Services.Layer
{
    using George.Email.Stream;
    using George.Data.Layer.DataAccess;

    public class AccountService
    {
        #region Instances
        private readonly DataAccess _dataAccess;
        private readonly Inbox _emailInbox;
        private readonly OutBox _emailOutbox;
        #endregion

        public AccountService()
        {
            _dataAccess = new DataAccess();
            _emailInbox = new Inbox();
            _emailOutbox = new OutBox();   
        }

        #region Services
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

        public void CreateNewAccount(string emailAddress, string password)
        {
            //_dataAccess.SaveUserAccountAsync(emailAddress, password);
        }
        #endregion
    }
}
