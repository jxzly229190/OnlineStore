namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;

    using V5.DataContract.System;

    public interface ISystemHomeDA
    {
        List<Province> SelectProvinces();

        List<City> SelectCities();

        List<County> SelectCounties();
    }
}