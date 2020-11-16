using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Ballcade/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Raise(GameObject go)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised(go);
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        _listeners.Remove(listener);
    }
}