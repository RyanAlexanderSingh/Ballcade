using UnityEngine;
using UnityEngine.Events;

namespace Ballcade
{
    [System.Serializable]
    public class EventGameObject : UnityEvent<GameObject> { }

    public abstract class BaseGameEventListener<T, GE, UER> : MonoBehaviour,
        IGameEventListener<T> where GE : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private GE baseGameEvent;
        [SerializeField] private UER _response;

        public GE GameEvent => baseGameEvent;
 
        private void OnEnable()
        {
            baseGameEvent.RegisterListener(this);
        }
 
        private void OnDisable()
        {
            baseGameEvent.UnregisterListener(this);
        }
 
        public void OnEventRaised(T item)
        {
            _response.Invoke(item);
        }
    }
}