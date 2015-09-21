namespace V5.Automate
{
    using System;

    public class DbColumn
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DbColumnType { get; set; }

        public Type Type { get; set; }

        public string Summary { get; set; }
    }
}