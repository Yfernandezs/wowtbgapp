using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wowtbgapp.api.Models
{
    public class ItemCard
    {
        public string _id { get; set; }

        public string Name { get; set; }

        public List<string> Types { get; set; } = new List<string>();

        public List<Counter> Requirements { get; set; } = new List<Counter>();

        public List<string> Effects { get; set; } = new List<string>();

        public string TypeIconUrl { get; set; }

        public string IconUrl { get; set; }

        public int BagSpace { get; set; }

        public string Origin { get; set; }

        public decimal Version { get; set; }
    }
}
