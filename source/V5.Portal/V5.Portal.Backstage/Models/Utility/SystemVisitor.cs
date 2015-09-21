using System;

namespace V5.Portal.Backstage.Models.Utility
{
    public class SystemVisitor
    {
        public int Id { get; set; }
        public int DepartDate { get; set; }
        public int VisitorCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UserName { get; set; }
        public string IP4Address { get; set; }
        public string IP6Address { get; set; }

    }
}
