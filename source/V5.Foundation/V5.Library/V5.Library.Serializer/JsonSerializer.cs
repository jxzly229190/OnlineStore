namespace V5.Library.Serializer
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public class JsonSerializer<T>
    {
        private readonly DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));

        public string Serialize(T item)
        {
            if (item.Equals(default(T)))
            {
                throw new ArgumentNullException("item");
            }

            using (var memoryStream = new MemoryStream())
            {
                this.dataContractJsonSerializer.WriteObject(memoryStream, item);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public T Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException("json");
            }

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)this.dataContractJsonSerializer.ReadObject(memoryStream);
            }
        }
    }
}