using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IntensityEvent : MonoBehaviour
{
    // After how much time should this event get triggered
    public float StartDelay{ get; private set; }

    // If the trigger count should be infinite
    public bool InfiniteTriggers { get; private set; }

    // How many times should this event get triggered
    public float TriggerCount { get; private set; }

    // How big should the delay be between each trigger of the event
    public float EventDelay { get; private set; }

    public abstract void Trigger();





}
