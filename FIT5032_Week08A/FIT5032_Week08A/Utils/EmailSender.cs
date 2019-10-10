using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents, String path, HttpPostedFileBase postedFile)
        { 

            var client = new SendGridClient(API_KEY); 
            var from = new EmailAddress("noreply@localhost.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            //create the mail message
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);


            if (postedFile != null && postedFile.ContentLength > 0)
            { 
                var bytes = File.ReadAllBytes(path);
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment(postedFile.FileName, file); 
            } 
            var response = client.SendEmailAsync(msg);  
        }  
    }
}