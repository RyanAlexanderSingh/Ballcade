using UnityEngine;

namespace Ballcade
{
    [CreateAssetMenu(fileName = "New Player Died Event", menuName = "Ballcade/Game Events/Player Died Event")]
    public class PlayerDiedEvent : BaseGameEvent<PlayerDiedData> { }
}