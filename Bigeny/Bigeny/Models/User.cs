using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    [Serializable]
    internal class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
    }
}
