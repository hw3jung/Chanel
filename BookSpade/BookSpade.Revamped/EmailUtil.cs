using System.Web.Helpers;
using System;
using System.Web.Mvc;
using System.Net.Mail; 

namespace BookSpade.Revamped
{
    public class EmailUtil : Controller
    {
        public EmailUtil()
        {
            
        }
        public EmailUtil(string email, string Name, string subject, string body)
        {
            SendNetMailMessage(email, Name, subject, body);
        }

        public void SendNetMailMessage(string To, string Name, string Subject, string Body)
        {

            string fBody = "Hi " + Name + ", ";
            fBody += "<br/>";
            fBody += "<br/>";
            fBody += Body;
            fBody += "<br/>";
            fBody += "<br/>";
            fBody += "Thank You,";
            fBody += "<br/>";
            fBody += "BookSpade Team";

            MailAddress addrfrom = new MailAddress("info@bookspade.com", "BookSpade");
            MailAddress addrto = new MailAddress(To);
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = addrfrom;
            msg.To.Add(addrto);
            msg.IsBodyHtml = true;
            msg.Body = fBody;
            msg.Subject = Subject; 
            SmtpClient smtp = new SmtpClient("smtpout.secureserver.net");
            smtp.Port = 80;
            smtp.Credentials = new System.Net.NetworkCredential("info@bookspade.com", "SpadeIt");
            smtp.Send(msg);

            return;
        }

    }
}