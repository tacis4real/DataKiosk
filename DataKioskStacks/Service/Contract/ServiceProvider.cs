namespace DataKioskStacks.Service.Contract
{

	public class ServiceProvider
	{
		 private IServiceProvider _iServices = null;
		 private static ServiceProvider _newInstance;
		public ServiceProvider()
		{
			 _iServices = new StandardServiceProvider();
		}

		 public static IServiceProvider Instance()
		{
			 if(_newInstance == null)
			 {
				 _newInstance = new ServiceProvider();
			 }
			 return _newInstance._iServices;
		}
	}

}
