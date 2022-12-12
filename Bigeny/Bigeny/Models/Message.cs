using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    public class Message
    {
        public int id { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }
        public int ownerId { get; set; }
    }
}
