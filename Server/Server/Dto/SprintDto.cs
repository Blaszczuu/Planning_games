using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Dto
{
    public class SprintDto
    {
        public string Name { get; set; }

        public TimeFrame TimeFrame {get; set;}

        public Uri Uri { get; set; }
    }
}
