using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    public class Channel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string description { get; set; }
        public int ownerId { get; set; }
        public List<Post> posts { get; set; }
    }
}
