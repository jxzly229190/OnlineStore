// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAFactoryChannel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The da factory channel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
	using V5.DataAccess.Channel;

	/// <summary>
	/// The da factory channel.
	/// </summary>
	public class DAFactoryChannel : DataAccess
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DAFactoryChannel"/> class.
		/// </summary>
		public DAFactoryChannel()
		{
			this.AssemblyPath = this.AssemblyPath + ".Channel";
		}

		/// <summary>
		/// The create ChannelGroupBuyDA department da.
		/// </summary>
		/// <returns>
		/// The <see cref="IChannelGroupBuyDA"/>.
		/// </returns>
		public IChannelGroupBuyDA CreateChannelGroupBuyDA()
		{
			string nameSpace = AssemblyPath + ".ChannelGroupBuyDA";
			object systemDepartmentDA = Create(AssemblyPath, nameSpace);
			return (IChannelGroupBuyDA)systemDepartmentDA;
		}
	}
}
