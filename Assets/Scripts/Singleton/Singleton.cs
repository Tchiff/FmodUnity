public abstract class Singleton<T> : ISingleton 
	where T : class, ISingleton, new()
{
	private static T _instance = null;

	public static T Instance
	{
		get
		{
			if (_instance != null)
				return _instance;
			return CreateSingleton();
		}
	}
	
	private static T CreateSingleton()
	{
		_instance = new T();
		_instance.Init();
		return _instance;
	}

	public virtual void Init()
	{
	}
}

public abstract class Singleton<IT, T> : Singleton<T> where T : class, ISingleton, IT, new()
{
	public new static IT Instance => Singleton<T>.Instance;
}