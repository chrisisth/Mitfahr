using System;
using System.Collections.Generic;

namespace mitfahr.Models
{
    public partial class JourneyHasUser
    {
        public int JourneyIdjourney { get; set; }
        public int JourneyDeparturePointPostcode { get; set; }
        public int JourneyUserIdUser { get; set; }
        public int UserIdUser { get; set; }

        public virtual Journey Journey { get; set; }
        public virtual User UserIdUserNavigation { get; set; }
    }
}
