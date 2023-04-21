using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    [Serializable]
    public class Tokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
