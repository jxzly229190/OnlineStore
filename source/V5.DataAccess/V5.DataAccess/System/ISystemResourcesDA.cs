namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;

    using V5.DataContract.System;

    public interface ISystemResourcesDA
    {
        List<System_Resources> SelectAll();
    }
}