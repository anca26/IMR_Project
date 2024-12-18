using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IntensityEvent : MonoBehaviour
{
    [SerializeField]
    protected bool _destroyOnNoTriggers;
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

    /*
     * Function called by the IntensityManager.
     * Its purpose is to prepare the event for triggering.
     */
    public void Prepare()
    {
        this._nextTriggerTime = Time.time + StartDelay;
        this._remainingTriggers = InfiniteTriggers ? 999999 : TriggerCount;
    }
    /*
     * This is the function that is called in the loop of the IntensityManager.
     */
    public void TriggerEvent()
    {
        if(_remainingTriggers == 0)
        {
            if (_destroyOnNoTriggers)
                Destroy(gameObject);
            else
                this.enabled = false;
        }

        if (Time.time >= _nextTriggerTime)
        {
            Trigger();

            TriggerCount--;
            _nextTriggerTime = Time.time + TriggerDelay;
        }
    }

    /*
     * This is the abstract function, specific for each type of event. Imagine it as a one time thing.
     * For example, for a sound effect, the trigger function could make a sound play.
     * For animation, it could also make it play, or start a chain of animations.
     * For an entity that must move randomly, it could make it active.
     */
    public abstract void Trigger();



}
