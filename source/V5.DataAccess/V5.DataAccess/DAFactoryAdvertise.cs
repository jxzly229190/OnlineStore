using V5.DataAccess.Advertise;

namespace V5.DataAccess
{
    public class DAFactoryAdvertise : DataAccess
    {
        public DAFactoryAdvertise()
        {
            this.AssemblyPath = this.AssemblyPath + ".Advertise";
        }

        public IAdvertiseConfigDA CreateAdvertiseConfigDA()
        {
            string nameSpace = AssemblyPath + ".AdvertiseConfigDA";
            object advertiseConfigDA = Create(AssemblyPath, nameSpace);
            return (IAdvertiseConfigDA)advertiseConfigDA;
        }
    }
}
