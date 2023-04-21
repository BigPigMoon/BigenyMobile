using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    [Serializable]
    public class Dialog
    {
        public int id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public bool isReaded { get; set; }
        public int countOfUser { get; set; }
        public Message lastMessage { get; set; }
    }
}
