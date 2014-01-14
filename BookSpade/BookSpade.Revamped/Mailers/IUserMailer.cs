using Mvc.Mailer;

namespace BookSpade.Revamped.Mailers
{ 
    public interface IUserMailer
    {
			MvcMailMessage Welcome();
			MvcMailMessage GoodBye();
	}
}