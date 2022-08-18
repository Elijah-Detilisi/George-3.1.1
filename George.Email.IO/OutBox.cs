using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace George.Email.Stream
{
    using System.Net;
    using System.Net.Mail;
    using System.Diagnostics;
    using System.ComponentModel;

    public class OutBox
    {
        #region Instances
        private SmtpClient _smtpClient;
        private MailAddress _senderEmailAddress;
        private List<MailAddress> _recipientEmailAddresses;
        private MailMessage _mailMessage;
        #endregion

        public OutBox()
        {
            _smtpClient = new SmtpClient();
            _recipientEmailAddresses = new List<MailAddress>();
            _mailMessage = new MailMessage();
        }

        #region Authentication Methods
        private void SetSmtpClient(string hostName, int portNumber, string emailAddress, string passWord)
        {
            _smtpClient.Host = hostName;
            _smtpClient.Port = portNumber;
            _senderEmailAddress = new MailAddress(emailAddress);
            _smtpClient.Credentials = new NetworkCredential()
            {
                UserName = emailAddress,
                Password = passWord
            };

            _smtpClient.EnableSsl = true;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.UseDefaultCredentials = false;
        }
        public void LoginToEmail(string emailAddress, string passWord)
        {
            try
            {
                int port = 587;
                string host = "smtp.gmail.com";
                SetSmtpClient(host, port, emailAddress, passWord);
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
        #endregion

        #region Message Initialization Methods
        public void SetRecipientEmails(List<string> recipientsList)
        {
            foreach (string recipientEmail in recipientsList)
            {
                _recipientEmailAddresses.Add(new MailAddress(recipientEmail));
            }
        }
        public void SetEmailMessage(string messageSubject, string messageBody)
        {
            _mailMessage.From = _senderEmailAddress;
            _mailMessage.Subject = messageSubject;
            _mailMessage.Body = messageBody;

            foreach (var recipientEmailAddress in _recipientEmailAddresses)
            {
                _mailMessage.To.Add(recipientEmailAddress);
            }
        }
        #endregion

        #region Message Sending Methods
        private void _SmtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var errorValue = e.Error;

            if (errorValue != null)
            {
                throw new Exception($"[INFO]: Async email failed to send: {errorValue.Message}");
            }
        }
        public void SendEmail()
        {
            try
            {
                _smtpClient.Send(_mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"[INFO]: Normal email failed to send: {ex.Message}");
            }
        }
        public async Task SendEmailAsync()
        {
            _smtpClient.SendCompleted += _SmtpClient_SendCompleted;
            await _smtpClient.SendMailAsync(_mailMessage);
        }
        #endregion
    }
}
