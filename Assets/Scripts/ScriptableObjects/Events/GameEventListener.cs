using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventGameObject : UnityEvent<GameObject> { }

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private EventGameObject _response;

    private void OnEnable()
    {
        _gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        _gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject go)
    {
        _response.Invoke(go);
    }
}