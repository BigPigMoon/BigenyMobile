using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    [Serializable]
    public class Channel
    {
        public int id { get; set; }
        public string avatar { get; set; }
        public string description { get; set; }
        public int ownerId { get; set; }
        public string name { get; set; }
        public Post lastPost { get; set; }
        public bool subscribe { get; set; }
    }
}
