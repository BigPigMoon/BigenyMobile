using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    public class Dialog
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Message> messages { get; set; }
        public string avatar { get; set; }
        public bool isReaded { get; set; }
    }
}
