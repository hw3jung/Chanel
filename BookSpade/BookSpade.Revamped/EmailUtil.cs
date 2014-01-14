using System.Web.Helpers;
using System;
using System.Web.Mvc;

namespace BookSpade.Revamped
{
    public class EmailUtil : Controller
    {
        public EmailUtil()
        {
            
        }
        public EmailUtil(string email, string Name, string subject, string body)
        {
            Mail(email, Name, subject, body);
        }

        // TOEmail, TOName, EmailSubject, EmailBody
        public static void Mail(string email, string Name, string subject, string body)
        {
            try
            {
                string fBody = "Hi " + Name + ", ";
                fBody += "<br/>";
                fBody += "<br/>";
                fBody += body;
                fBody += "<br/>";
                fBody += "<br/>";
                fBody += "Thank You,";
                fBody += "<br/>";
                fBody += "BookSpade Team";
                //Should we do a logo signature?

                WebMail.EnableSsl = true;
                WebMail.SmtpServer = "smtpout.secureserver.net";
                WebMail.SmtpPort = 80;
                WebMail.UserName = "info@bookspade.com";
                WebMail.Password = "SpadeIt";
                WebMail.Send(
                    email,
                    subject,
                    fBody,
                    from: "info@bookspade.com"
                );

            } catch (Exception ex) {
                Console.Write("nooo " + ex.StackTrace + "<br/>" + ex.Message); 
            }
        }

    }
}