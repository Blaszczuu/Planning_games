
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{

    public class CardPacksResponse
    {
        public int Cards { get; set; }
        public State state { get; set; }
        public int CardResult { get; set; }
    }

}
