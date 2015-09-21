// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailService.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The email service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library
{
    using System;
    using System.Net.Mail;
    using System.Threading;

    /// <summary>
    /// The email service.
    /// </summary>
    public class EmailService
    {
        /// <summary>
        /// The email.
        /// </summary>
        private readonly Email email;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        public EmailService(Email email)
        {
            this.email = email;
        }

        /// <summary>
        /// The send by smtp.
        /// </summary>
        public void SendBySmtp()
        {
            var smtpClient = new SmtpClient(this.email.Host)
                                 {
                                     DeliveryMethod = this.email.SmtpDeliveryMethod,
                                     Credentials =
                                         new System.Net.NetworkCredential(
                                         this.email.From,
                                         this.email.Password)
                                 };

            var thread = new Thread(this.SendEmailForeach) { IsBackground = true };
            thread.Start(smtpClient);
        }

        /// <summary>
        /// The send email foreach.
        /// </summary>
        /// <param name="smtpClient">
        /// The smtp client.
        /// </param>
        private void SendEmailForeach(object smtpClient)
        {
            var smtp = smtpClient as SmtpClient;

            foreach (var to in this.email.ToList)
            {
                var mailMessage = new MailMessage(this.email.From, to)
                                      {
                                          IsBodyHtml = this.email.IsBodyHtml,
                                          Subject = this.email.Subject,
                                          SubjectEncoding =
                                              System.Text.Encoding.GetEncoding(
                                                  this.email.SubjectEncoding),
                                          Body = this.email.Body,
                                          BodyEncoding =
                                              System.Text.Encoding.GetEncoding(
                                                  this.email.BodyEncoding)
                                      };

                if (smtp != null)
                {
                    smtp.SendCompleted += this.SmtpClient_SendCompleted;
                    smtp.Send(mailMessage);
                }
            }
        }

        /// <summary>
        /// The smtp client_ send completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SmtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw new Exception("电子邮件发送失败，异常信息：" + e);
            }
        }
    }
}