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

        public Profile(int ProfileId, string Name, string Email)
        {
            this.ProfileId = ProfileId;
            this.Name = Name;
            this.Email = Email; 
        }

        public Profile()
        {

        }

    }
}