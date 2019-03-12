using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Infrastructure.Services
{
    public class LocalMailService : IMailService
    {
        //private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        //private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        private string _mailTo;
        private string _mailFrom;

        public LocalMailService(IOptions<EmailSettings> mailOptions)
        {
            _mailFrom = mailOptions.Value.MailFromAddress;
            _mailTo = mailOptions.Value.MailToAddress;
        }

        public void Send(string subject, string message)
        {
            // send mail - output to the debug window.
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
