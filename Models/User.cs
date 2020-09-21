using System;
using System.Collections.Generic;

namespace mitfahr.Models
{
    public partial class User
    {
        public User()
        {
            Journey = new HashSet<Journey>();
            JourneyHasUser = new HashSet<JourneyHasUser>();
        }

        public int IdUser { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<Journey> Journey { get; set; }
        public virtual ICollection<JourneyHasUser> JourneyHasUser { get; set; }
    }
}
