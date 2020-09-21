using System;
using System.Collections.Generic;

namespace mitfahr.Models
{
    public partial class Journey
    {
        public Journey()
        {
            JourneyHasUser = new HashSet<JourneyHasUser>();
        }

        public int Idjourney { get; set; }
        public string DepartureTime { get; set; }
        public string Regularly { get; set; }
        public sbyte Smoker { get; set; }
        public int DeparturePointPostcode { get; set; }
        public int UserIdUser { get; set; }

        public virtual DeparturePoint DeparturePointPostcodeNavigation { get; set; }
        public virtual User UserIdUserNavigation { get; set; }
        public virtual ICollection<JourneyHasUser> JourneyHasUser { get; set; }
    }
}
