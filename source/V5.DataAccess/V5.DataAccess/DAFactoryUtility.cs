// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUtilityDA.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The da factory channel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace V5.DataAccess
{
    using V5.DataAccess.Utility;

    public class DAFactoryUtility : DataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DAFactoryChannel"/> class.
        /// </summary>
        public DAFactoryUtility()
        {
            this.AssemblyPath = this.AssemblyPath + ".Utility";
        }

        /// <summary>
        /// The create ChannelGroupBuyDA department da.
        /// </summary>
        /// <returns>
        /// The <see cref="ICodeDA"/>.
        /// </returns>
        public ICodeDA CreateCodeDA()
        {
            string nameSpace = AssemblyPath + ".CodeDA";
            object systemDepartmentDA = Create(AssemblyPath, nameSpace);
            return (ICodeDA)systemDepartmentDA;
        }
        public ISystemVisitorDA CreateSystemVisitorDA()
        {
            string nameSpace = AssemblyPath + ".SystemVisitorDA";
            object systemDepartmentDA = Create(AssemblyPath, nameSpace);
            return (ISystemVisitorDA)systemDepartmentDA;
        }
    }
}
