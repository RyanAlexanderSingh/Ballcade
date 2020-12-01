using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoSingleton<App>
{
	private GameManager _gameManager;
	private AIPlayerMananger _aiPlayerMananger;
	
	public static ObjectPoolManager objectPooler { get; private set; }
	
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		
		SetAppSettings();

		CreateManagers();

		StartGame();
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

	private void CreateManagers()
	{
		_gameManager = CreateManagerPrefab<GameManager>("GameManager");
		
		_aiPlayerMananger = CreateManagerPrefab<AIPlayerMananger>("AIPlayerManager");
		
		objectPooler = CreateManagerPrefab<ObjectPoolManager>("ObjectPoolManager");
	}

	private T CreateManagerPrefab<T>(string prefabName) where T : Object
	{
		var original = Resources.Load<T>($"Managers/{prefabName}");
		return Instantiate(original, transform, true);
	}

	private void StartGame()
	{
		_gameManager.Initialise(_aiPlayerMananger);

		_gameManager.StartGame(true);
	}
}