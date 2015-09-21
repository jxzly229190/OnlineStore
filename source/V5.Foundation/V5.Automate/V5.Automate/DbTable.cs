namespace V5.Automate
{
    using System.Collections.Generic;

    public class DbTable
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<DbColumn> DbColumns { get; set; }

        public string Summary { get; set; }
    }
}