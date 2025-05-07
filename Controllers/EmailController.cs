using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using CPRCMAPI.Models;
using System.Net.Mail;
using System.Net;

namespace CPRCMAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class EmailController : Controller
    {
        

        private readonly IConfiguration _configuration;

        const string _mailServer = "smtp.gmail.com"; 
        const string _userName = "clientsupport@claimpower.com";
        const string _password = "support123#";

        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpPost]
        [Route("SendEmail")]
        public string SendEmail(Email obj)
        {
            MailMessage objMail = new MailMessage()
            {
                From = new MailAddress("ClientSupport@Claimpower.com", "ClientSupport@Claimpower.com"),
                IsBodyHtml = true,
                Subject = obj.subject,
                Body = obj.emailbody
            };

            if (obj.attachfile != "")
                objMail.Attachments.Add(new Attachment(obj.attachfile));

            var toelements = obj.toaddress.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string items in toelements)
            {
                objMail.To.Add(new MailAddress(items));
            }

            var ccelements  = obj.ccaddress.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string items in ccelements)
            {
                objMail.CC.Add(new MailAddress(items));
            }

            // Smtpclient to send the mail message
            SmtpClient SmtpMail = new SmtpClient(_mailServer)
            {
                Credentials = new NetworkCredential(_userName, _password)
            };
            SmtpMail.Port = 587;
            SmtpMail.EnableSsl = true;
            SmtpMail.Send(objMail);

            return "OK";
        }

    }
}
