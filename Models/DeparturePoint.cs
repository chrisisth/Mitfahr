using System;
using System.Collections.Generic;

namespace mitfahr.Models
{
    public partial class DeparturePoint
    {
        public DeparturePoint()
        {
            Journey = new HashSet<Journey>();
        }

        public int Postcode { get; set; }
        public string City { get; set; }

        public virtual ICollection<Journey> Journey { get; set; }
    }
}
