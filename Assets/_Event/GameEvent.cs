using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> Listeners = new List<GameEventListener>();

    public void Raise(Component sender, float data)
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i].OnEventRaised(sender,data);
        }
    }
    public void RegisterListener(GameEventListener listener)
    { Listeners.Add(listener); }
    public void UnregisterListener(GameEventListener listener)
    { Listeners.Remove(listener); }
}
