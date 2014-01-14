using Mvc.Mailer;

namespace BookSpade.Revamped.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage Welcome()
		{
			
            return Populate(x =>
            {
                x.Subject = "Welcome";
                x.ViewName = "Welcome";
                x.To.Add("asma.patel@hotmail.com");
            });
		}
 
		public virtual MvcMailMessage GoodBye()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "GoodBye";
				x.ViewName = "GoodBye";
				x.To.Add("some-email@example.com");
			});
		}
 	}
}