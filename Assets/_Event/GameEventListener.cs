using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class DataUnityEvent : UnityEvent<Component,object> { }
public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;
    public DataUnityEvent ResponseData;

    private void OnEnable()
    { Event.RegisterListener(this); }
    private void OnDisable()
    { Event.UnregisterListener(this); }
    public void OnEventRaised()
    { Response.Invoke(); }
    public void OnEventRaised(Component component, object data)
    { ResponseData.Invoke(component, data); }


}
