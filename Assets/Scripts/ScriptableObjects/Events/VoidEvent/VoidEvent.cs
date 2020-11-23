using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Ballcade/Void Game Event")]
public class VoidEvent : BaseGameEvent<VoidData>
{
    public void Raise() => Raise(new VoidData());
}