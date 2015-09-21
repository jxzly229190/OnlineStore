using System.Collections.Generic;
using V5.DataContract.User;
using V5.Library.Storage.DB;

namespace V5.DataAccess.User
{
    public interface IFeedBackDA
    {
        int Insert(FeedBack feedBack);
        List<FeedBack> Paging(Paging paging, out int pageCount, out int totalCount);

        FeedBack QueryFeedBackByID(int id);
    }

}
