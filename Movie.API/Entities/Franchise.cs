using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Movie.API.Entities
{

    public class Franchise
    {
        public string Name { get; set; }

        public List<string> MovieTitles { get; set; }
    }
}
