namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;
    using global::System.Data;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    public class SystemResourcesDA: ISystemResourcesDA
    {
        /// <summary>
        /// The sql server.
        /// </summary>
        public SqlServer SqlServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemResourcesDA"/> class.
        /// </summary>
        public SystemResourcesDA()
        {
            this.SqlServer = new SqlServer();
        }

        /// <summary>
        /// The select all.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<System_Resources> SelectAll()
        {
            var dataReader = this.SqlServer.ExecuteDataReader(
                CommandType.StoredProcedure,
                "sp_System_Resources_SelectAll",
                null,
                null);
            return dataReader.ToList<System_Resources>();
        }
    }
}