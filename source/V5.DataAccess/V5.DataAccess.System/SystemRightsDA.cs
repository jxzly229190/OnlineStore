namespace V5.DataAccess.System
{
    using global::System.Collections.Generic;
    using global::System.Data;
    using global::System.Data.SqlClient;
    using global::System.Linq;
    using global::System;

    using V5.DataContract.System;
    using V5.Library.Storage.DB;

    public class SystemRightsDA:ISystemRightsDA
    {
        private SqlServer SqlServer;

        public SystemRightsDA()
        {
            this.SqlServer = new SqlServer();
        }

        /// <summary>
        /// The select by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SelectByUserID(int userID)
        {
            List<System_Rights> list =
                this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_System_Rights_SelectByUserID",
                    new List<SqlParameter>
                        {
                            this.SqlServer.CreateSqlParameter(
                                "UserID",
                                SqlDbType.Int,
                                userID,
                                ParameterDirection.Input)
                        },
                    null).ToList<System_Rights>();

            if (list.Count < 1)
            {
                return string.Empty;
            }

            char[] tempArr = list[0].UserRights.ToCharArray();
            
            for (var i = 1; i < list.Count; i++)
            {
                char[] arr_rights2 = list[1].UserRights.ToCharArray();
                if (arr_rights2.Length > tempArr.Length)
                {
                    for (var j = 0; j < tempArr.Length; j++)
                    {
                        if (tempArr[j] == '1' && arr_rights2[j] == '0')
                        {
                            arr_rights2[j] = '1';
                        }
                    }

                    tempArr = arr_rights2;
                }
                else
                {
                    for (var j = 0; j < arr_rights2.Length; j++)
                    {
                        if (arr_rights2[j] == '1' && tempArr[j] == '0')
                        {
                            tempArr[j] = '1';
                        }
                    }
                }
            }

            return new string(tempArr);
        }

        /// <summary>
        /// The select by role id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SelectByRoleID(int roleID)
        {
            List<System_Rights> list =
                this.SqlServer.ExecuteDataReader(
                    CommandType.StoredProcedure,
                    "sp_System_Rights_SelectByRoleID",
                    new List<SqlParameter>
                        {
                            this.SqlServer.CreateSqlParameter(
                                "RoleID",
                                SqlDbType.Int,
                                roleID,
                                ParameterDirection.Input)
                        },
                    null).ToList<System_Rights>();
            
            if (list == null || list.Count == 0) return "";

            return list[0].UserRights;            
        }

        /// <summary>
        /// 新增系统权限
        /// </summary>
        /// <param name="rights">
        /// 系统权限对象
        /// </param>
        public int Insert(System_Rights rights)
        {
            if (rights == null)
            {
                throw new ArgumentNullException("rights");
            }

            int id;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         rights.UserID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         rights.RoleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserRights",
                                         SqlDbType.VarChar,
                                         rights.UserRights,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "ReferenceID",
                                         SqlDbType.Int,
                                         null,
                                         ParameterDirection.Output)
                                 };

            try
            {
                this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Rights_Insert", parameters, null);
                id = (int)parameters.Find(parameter => parameter.ParameterName == "ReferenceID").Value;
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemRightsDA - Insert", exception);
            }

            return id;
        }

        /// <summary>
        /// 修改系统权限
        /// </summary>
        /// <param name="rights">
        /// 系统权限对象
        /// </param>
        public int Update(System_Rights rights)
        {
            if (rights == null)
            {
                throw new ArgumentNullException("rights");
            }

            int result = 0;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "UserID",
                                         SqlDbType.Int,
                                         rights.UserID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         rights.RoleID,
                                         ParameterDirection.Input),
                                     this.SqlServer.CreateSqlParameter(
                                         "UserRights",
                                         SqlDbType.VarChar,
                                         rights.UserRights,
                                         ParameterDirection.Input)
                                 };

            try
            {
               result = this.SqlServer.ExecuteNonQuery(CommandType.StoredProcedure, "sp_System_Rights_Update", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemRightsDA - Update", exception);
            }

            return result;
        }

        /// <summary>
        /// 判断角色或用户是否具有系统权限
        /// </summary>
        /// <param name="rights">
        /// 系统权限对象
        /// </param>
        public bool Exists(System_Rights rights)
        {
            if (rights == null)
            {
                throw new ArgumentNullException("rights");
            }

            object result = null;
            var parameters = new List<SqlParameter>
                                 {
                                     this.SqlServer.CreateSqlParameter(
                                         "RoleID",
                                         SqlDbType.Int,
                                         rights.RoleID,
                                         ParameterDirection.Input)
                                 };

            try
            {
                result = this.SqlServer.ExecuteScalar(CommandType.StoredProcedure, "sp_System_Rights_Exists", parameters, null);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception - SystemRightsDA - Exists", exception);
            }

            return Convert.ToInt32(result) == 1 ? true : false;
        }
    }
}