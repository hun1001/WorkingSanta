using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    private static Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
  
    public static void StartListening(string eventName, Action listener)
    {
        Action thisEvent;
        if(eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Action listener){
        Action thisEvent;
        if(eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Remove(eventName);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        Action thisEvent = null;
        if(eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent?.Invoke();
        }
    }
}
