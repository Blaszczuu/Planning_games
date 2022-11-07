using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class targetEntity
    {
        public object rel { get; set; }
        public object source { get; set; }
        public Target target { get; set; }
    }
    
}
