using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, float> { }
public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public CustomGameEvent Response;
    private void OnEnable()
    { Event.RegisterListener(this); }
    private void OnDisable()
    { Event.UnregisterListener(this); }
    public void OnEventRaised(Component sender, float data)
    { Response.Invoke(sender,data); }

}
