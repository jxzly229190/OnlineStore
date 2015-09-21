namespace V5.Service.Transact
{
	using V5.DataAccess;
	using V5.DataAccess.Transact;
	using V5.DataContract.Transact;

	public class CpsLinkRecordService
	{
		private readonly ICpsLinkRecordDA cpsLinkRecordDa;

		public CpsLinkRecordService()
		{
			cpsLinkRecordDa = new DAFactoryTransact().CreateCpsLinkRecordDA();
		}

		public int Add(Cps_LinkRecord linkRecord)
		{
			return cpsLinkRecordDa.Insert(linkRecord, null);
		}
	}
}