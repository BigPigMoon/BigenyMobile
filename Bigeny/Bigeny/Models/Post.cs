using System;

namespace Bigeny.Models
{
    public class Post
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public string content { get; set; }
        public string image { get; set; }
        public int channelId { get; set; }
    }
}
