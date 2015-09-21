namespace V5.Portal.Models
{
    using System.Collections.Generic;

    public class AttributeModel
    {
        public int ID { get; set; }

        public string AttributeCode { get; set; }

        public string AttributeName { get; set; }

        public string InputType { get; set; }

        public string DataType { get; set; }

        public int DataLength { get; set; }

        public List<AttributeValueModel> AttributeValues { get; set; }
    }
}