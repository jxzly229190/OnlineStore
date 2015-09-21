// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUtilityDA.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
// The ChannelGroupBuyDA interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace V5.DataAccess.Utility
{
    using V5.DataContract.Utility;

    public interface ICodeDA
    {
        #region Public Methods and Operators

        int Insert(Code code);

        List<Code> FindById(int id);

        int UpdateIterator(int iterator, int id);
        int UpdateStartTime(int time, int id);
        int UpdateUserIterator(int iterator, int id);
        List<Code> FindByUserCode(string userCode);

        int Update(Code code);

        #endregion
    }
}
