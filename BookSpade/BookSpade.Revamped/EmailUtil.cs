using System.Web.Helpers;

namespace BookSpade.Revamped
{
    public class EmailUtil
    {

        public EmailUtil(string email, string subject, string body)
        {
            Mail(email, subject, body);
        }

        public static void Mail(string email, string subject, string body)
        {
            WebMail.EnableSsl = false;
            WebMail.SmtpServer = "smtpout.secureserver.net";
            WebMail.SmtpPort = 80;
            WebMail.UserName = "info@bookspade.com";
            WebMail.Password = "SpadeIt";
            WebMail.Send(
                email,
                subject,
                body,
                from: "info@bookspade.com"
            );
        }
    }
}