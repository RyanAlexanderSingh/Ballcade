using UnityEngine;
 using UnityEngine.Events;
 
 public interface IGameEventListener<T>
 {
     void OnEventRaised(T item);

 }