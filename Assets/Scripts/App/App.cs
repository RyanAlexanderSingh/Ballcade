using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ballcade
{
	public class App : MonoSingleton<App>
	{
		[Header("Dependencies")]
		[SerializeField] private ObjectPoolManager _objectPoolManager;
	
		public static ObjectPoolManager ObjectPool { get; private set; }

		public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
		{
			SceneManager.LoadScene(sceneName, loadSceneMode);
		}
	
		protected void Awake()
		{
			DontDestroyOnLoad(gameObject);
		
			SetAppSettings();

			CreateObjectPool();
		}

		private void SetAppSettings()
		{
			// Set app defaults
			Application.targetFrameRate = 30;
			Application.runInBackground = true;

			// Vsync must be off in editor to adhere to target frame rate
#if UNITY_EDITOR
		
			QualitySettings.vSyncCount = 0;
		
#endif
			// Disable screen dimming
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		private void CreateObjectPool()
		{
			ObjectPool = CreateManagerPrefab<ObjectPoolManager>(_objectPoolManager);
		}

		private T CreateManagerPrefab<T>(Object prefab) where T : Object
		{
			return Instantiate(prefab, transform, true) as T;
		}
	}
}