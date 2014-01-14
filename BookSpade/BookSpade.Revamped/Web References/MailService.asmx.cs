using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net.Mail; 

namespace MailService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
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
            MailAddress replytoaddr = new MailAddress("info@bookspade.com");
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = addrfrom;
            msg.To.Add(addrto);
            msg.ReplyTo = replytoaddr;
            msg.IsBodyHtml = true;
            msg.Body = Body; 
            SmtpClient smtp = new SmtpClient("smtpout.secureserver.net");
            smtp.Port = 80; 
            smtp.Credentials = new System.Net.NetworkCredential("info@bookspade.com", "SpadeIt");
            smtp.Send(msg);

            return;
        }

    }
}