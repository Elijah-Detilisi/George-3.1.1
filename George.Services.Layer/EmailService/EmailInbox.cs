using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace George.Services.Layer.EmailService
{
    using OpenPop.Pop3;
    using OpenPop.Mime;
    using System.Diagnostics;
    using George.Data.Layer.DataModel;

    public class EmailInbox
    {
        #region Instances
        private int _totalMessageCount;
        private Pop3Client _imapClient;
        private List<Message> _allMessages;
        #endregion

        public EmailInbox()
        {
            _totalMessageCount = 0;
            _imapClient = new Pop3Client();
            _allMessages = new List<Message>();
        }

        #region Authentication Methods
        public void LoginToEmail(UserAccount userAccount)
        {
            try
            {
                _imapClient.Connect(userAccount.Pop3HostName, userAccount.Pop3PortNumber, true);
                _imapClient.Authenticate(userAccount.EmailAddress, userAccount.EmailPassword);
                _totalMessageCount = _imapClient.GetMessageCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Email Message Methods
        public void LoadInboxMessagesAsync()
        {
            for (int i = _totalMessageCount; i > 0; i--)
            {
                try
                {
                    var message = _imapClient.GetMessage(i);
                    _allMessages.Add(message);
                }
                catch (Exception)
                {
                    Debug.WriteLine($"[INFO]: EMAIL MESSAGE {i} FAILED TO LOAD.");
                }
            }
        }

        public List<Message> GetAllMessages()
        {
            return _allMessages;
        }
        #endregion

    }
}
