// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Email.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The email.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library
{
    using System.Collections.Generic;
    using System.Net.Mail;

    /// <summary>
    /// The email.
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <param name="isBodyHtml">
        /// The is Body Html.
        /// </param>
        public Email(string host, string from, string password, string subject, string body, bool isBodyHtml)
        {
            this.Host = host;
            this.From = from;
            this.Password = password;
            this.Subject = subject;
            this.Body = body;

            this.ToList = new List<string>();
            this.IsBodyHtml = isBodyHtml;
            this.SubjectEncoding = "GB2312";
            this.BodyEncoding = "GB2312";
            this.SmtpDeliveryMethod = SmtpDeliveryMethod.Network;
        }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the from.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the to list.
        /// </summary>
        public List<string> ToList { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the subject encoding.
        /// </summary>
        public string SubjectEncoding { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is body html.
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the body encoding.
        /// </summary>
        public string BodyEncoding { get; set; }

        /// <summary>
        /// Gets or sets the smtp delivery method.
        /// </summary>
        public SmtpDeliveryMethod SmtpDeliveryMethod { get; set; }
    }
}