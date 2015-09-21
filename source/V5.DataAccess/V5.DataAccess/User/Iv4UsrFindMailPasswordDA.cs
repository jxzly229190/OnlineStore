using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V5.DataContract.User;

namespace V5.DataAccess.User
{
    public interface Iv4UsrFindMailPasswordDA
    {
        v4_Usr_FindMailPassword GetByUserId(int userId);
        int UpdateStateByUserId(int userId, int state);
        int Insert(v4_Usr_FindMailPassword model);

        v4_Usr_FindMailPassword GetById(int id);

        int UpdateValidateCount(int userId, int validateCount);

    }
}
