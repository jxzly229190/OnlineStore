namespace V5.Library.Serializer
{
    using System;
    using System.IO;

    public class XmlSerializer<T>
    {
        private readonly System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

        public void Serialize(string fileFullName, T item)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                throw new ArgumentNullException("fileFullName");
            }

            if (item.Equals(default(T)))
            {
                throw new ArgumentNullException("item");
            }

            try
            {
                if (File.Exists(fileFullName))
                {
                    File.Delete(fileFullName);
                }

                using (var fileStream = new FileStream(fileFullName, FileMode.Create))
                {
                    this.xmlSerializer.Serialize(fileStream, item);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public T Deserialize(string fileFullName)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                throw new ArgumentNullException("fileFullName");
            }

            try
            {
                if (!File.Exists(fileFullName))
                {
                    throw new IOException("没有找到对应文件");
                }

                using (var fileStream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return (T)this.xmlSerializer.Deserialize(fileStream);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }
    }
}