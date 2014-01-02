using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSpade.Revamped.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FacebookId { get; set; }
        public string FacebookLink { get; set; }

        public Profile(int profileId, string name, string email, string facebookId, string facebookLink)
        {
            this.ProfileId = profileId;
            this.Name = name;
            this.Email = email;
            this.FacebookId = facebookId;
            this.FacebookLink = facebookLink;
        }

        public Profile()
        {
        }
    }
}