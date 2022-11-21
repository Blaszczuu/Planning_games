using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class SprintReq
    {
        public string SprintName { get; set; } 
        public int SprintId { get; set; }
        public State state { get; set; }
    }
}
