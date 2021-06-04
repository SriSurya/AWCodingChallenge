using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.DataAccess.Models
{
    [DataContract(Name = "Franchise")]
    public class Franchise
    {
        public string Name { get; set; }

        public List<string> MovieTitles { get; set; }
    }
}
