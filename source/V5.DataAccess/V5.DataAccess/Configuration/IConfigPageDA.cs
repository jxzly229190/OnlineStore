using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V5.DataContract.Configuration;
using V5.Library.Storage.DB;

namespace V5.DataAccess.Configuration
{
    public interface IConfigPageDA
    {
        List<Config_Page> Query(int type);

        Config_Page QueryByID(int id);

        int UpdateContent(int id, string content, string name);

        int Insert(Config_Page model);
        List<Config_Page> Paging(Paging paging, out int pageCount, out int totalCount);

        int DeleteRow(int id);
        int UpdateLink(Config_Page config);
        int Delete(int id);
    }
}
