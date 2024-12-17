using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IntensityEvent : MonoBehaviour
{
    // After how much time should this event get triggered
    public float StartDelay{ private get; set; }

    // If the trigger count should be infinite
    public bool InfiniteTriggers { private get; set; }

    // How many times should this event private get triggered
    public int TriggerCount { private get; set; }

    // How big should the delay be between each trigger of the event
    public float TriggerDelay { private get; set; }

    
    private float _nextTriggerTime;
    private int _remainingTriggers;

    public void Prepare()
    {
        this._nextTriggerTime = Time.time + StartDelay;
        this._remainingTriggers = InfiniteTriggers ? 999999 : TriggerCount;
    }
    public void TriggerEvent()
    {
        if(_remainingTriggers != 0)
        {
            Destroy(gameObject);
        }

        if (Time.time >= _nextTriggerTime)
        {
            Trigger();

            TriggerCount--;
            _nextTriggerTime = Time.time + TriggerDelay;
        }
    }

    public abstract void Trigger();



}
