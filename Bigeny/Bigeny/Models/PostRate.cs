using System;
using System.Collections.Generic;
using System.Text;

namespace Bigeny.Models
{
    [Serializable]
    public class PostRate
    {
        public int rate { get; set; }
        public bool userRate { get; set; }
    }
}
