using UnityEngine;

namespace Ballcade.BallScoredEvent
{
    [CreateAssetMenu(fileName = "New Ball Scored Event", menuName = "Ballcade/Game Events/Ball Scored Event")]
    public class BallScoredEvent : BaseGameEvent<BallScoredData> { }
}