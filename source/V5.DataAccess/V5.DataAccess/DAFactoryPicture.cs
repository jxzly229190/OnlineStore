// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAFactoryPicture.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The da factory picture.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
    /// <summary>
    /// The da factory picture.
    /// </summary>
    public class DAFactoryPicture : DataAccess
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="DAFactoryPicture"/> class.
        /// </summary>
        public DAFactoryPicture()
        {
            this.AssemblyPath = this.AssemblyPath + ".Picture";
        }
    }
}