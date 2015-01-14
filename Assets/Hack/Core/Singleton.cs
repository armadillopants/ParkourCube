namespace Hack.Core
{
	public abstract class Singleton<T>
	{
		protected static T instance;
		public static T Instance
		{
			get
			{
				if( instance == null )
				{
					instance = System.Activator.CreateInstance<T>();
				}
				return instance;
			}
		}
	}
}