using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Helper;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CMS.Core.Service.Implementation
{
    public class EmailSenderServiceImpl : EmailSenderService
    {
        private readonly TransactionManager _transactionManager;
        private readonly ReceivedEmailMaker _receivedEmailMaker;
        private readonly ReceivedEmailRepository _receivedEmailRepo;
        private readonly SetupRepository _setupRepo;

        public EmailSenderServiceImpl(TransactionManager transactionManager, ReceivedEmailMaker receivedEmailMaker, ReceivedEmailRepository receivedEmailRepo, SetupRepository setupRepo)
        {
            _transactionManager = transactionManager;
            _receivedEmailMaker = receivedEmailMaker;
            _receivedEmailRepo = receivedEmailRepo;
            _setupRepo = setupRepo;
        }

        public void send(ReceivedEmailDto email_detail,string email_password,string email_host, string email_port)
        {
            try
            {
                _transactionManager.beginTransaction();

                try
                {
                    sendEmail(email_detail,email_password,email_host,email_port);
                }
                catch (Exception ex)
                {
                    throw new EmailSendFailureException(ex.Message);
                }


                ReceivedEmail receivedEmail = new ReceivedEmail();
                _receivedEmailMaker.copy(ref receivedEmail, email_detail);

                _receivedEmailRepo.insert(receivedEmail);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }

        }

        private void sendEmail(ReceivedEmailDto email_detail, string email_password, string email_host, string email_port)
        {
            if (!email_detail.isReceiverEmailValid())
            {
                throw new InvalidValueException("Receiver email is not valid.");
            }


            var mailMessage = new MailMessage(email_detail.sender_email,
               email_detail.receiver_email);


            mailMessage.Subject = email_detail.subject;
            mailMessage.Body = email_detail.message;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host =email_host;
                smtp.Port = Convert.ToInt32(email_port);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(email_detail.sender_email, email_password);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            
                smtp.Send(mailMessage);
            }
        }
    }
}
