using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    public class Message
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string text { get; set; }
        public int from_id { get; set; }
        public int dialogId { get; set; }
    }
}
