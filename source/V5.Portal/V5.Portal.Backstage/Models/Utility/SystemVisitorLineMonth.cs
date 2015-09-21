using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V5.Portal.Backstage.Models.Utility
{
    public class SystemVisitorLineMonth
    {
        public string name { get; set; }
        public string vpTitle { get; set; }
        public List<int> value { get; set; }
        public int[] labels { get; set; }
        public string color { get; set; }
        public string line_width { get; set; }
    }
}