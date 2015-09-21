namespace V5.Library.Serializer
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Soap;

    public class SoapSerializer
    {
        private readonly SoapFormatter soapFormatter = new SoapFormatter();

        public void Serialize(string fileFullName, object item)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                throw new ArgumentNullException("fileFullName");
            }

            if (item == null)
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
                    this.soapFormatter.Serialize(fileStream, item);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        }

        public object Deserialize(string fileFullName)
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

                using (Stream stream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return this.soapFormatter.Deserialize(stream);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
        } 
    }
}