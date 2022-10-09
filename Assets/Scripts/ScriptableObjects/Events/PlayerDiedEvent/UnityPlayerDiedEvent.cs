using UnityEngine.Events;

namespace Ballcade
{
    [System.Serializable]
    public class UnityPlayerDiedEvent : UnityEvent<PlayerDiedData> { }
}