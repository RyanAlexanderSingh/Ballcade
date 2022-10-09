using System.Collections.Generic;
using UnityEngine;

namespace Ballcade
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Ballcade/Game Event")]
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> _listeners = new List<IGameEventListener<T>>();

        public void Raise(T item)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }
    }
}