

namespace Server.Entities
{
    public class WorkTitle
    {

            public int id { get; set; }
            public int rev { get; set; }
            public Fields fields { get; set; }
            public _Links ?Links { get; set; }
            public Uri url { get; set; }
        

    }
}
