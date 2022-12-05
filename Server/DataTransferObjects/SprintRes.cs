using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class SprintRes
    {
        public int Id { get; set; }
        public string SystemTitle { get; set; }
        public List<string> SystemDescription { get; set; }
    }
}
