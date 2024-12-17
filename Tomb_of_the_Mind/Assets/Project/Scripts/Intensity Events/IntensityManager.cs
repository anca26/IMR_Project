using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IntensityManager : MonoBehaviour
{
    [SerializeField]
    private List<EventInterface> _intensityEvents;

    [System.Serializable]
    private struct EventInterface : IComparable<EventInterface>
    {
        public IntensityEvent instance;
        public float StartDelay;
        public bool  InfiniteTriggers;
        public int   TriggerCount;
        public float TriggerDelay;


        [HideInInspector]
        public bool Found;
        public EventInterface(IntensityEvent inst, string name)
        {
            instance = inst;
            StartDelay = 0;
            InfiniteTriggers = false;
            TriggerCount = 0;
            TriggerDelay = 0;

            Found = true;
        }

        public int CompareTo(EventInterface other)
        {
            return (int)(this.StartDelay - other.StartDelay);
        }
    }

    

    private void Awake()
    {

        foreach (EventInterface intensityEvent in this._intensityEvents)
        {
            InitEvent(intensityEvent);
        }
    }

    private void Update()
    {
       foreach(EventInterface intensityEvent in this._intensityEvents)
       {
            intensityEvent.instance.TriggerEvent();
       }
    }

    private void InitEvent(EventInterface ev)
    {
        ev.instance.StartDelay = ev.StartDelay;
        ev.instance.InfiniteTriggers = ev.InfiniteTriggers;
        ev.instance.TriggerCount = ev.TriggerCount;
        ev.instance.TriggerDelay = ev.TriggerDelay;
        ev.instance.Prepare();
    }

    public void AddEventChildren()
    {
        IntensityEvent[] events = transform.GetComponentsInChildren<IntensityEvent>();

        for (int i = 0; i < _intensityEvents.Count; ++i)
        {
            var elem = _intensityEvents[i];
            elem.Found = false;
        }

        foreach (var ev in events)
        {
            
            if (_intensityEvents.Exists(evi => evi.instance == ev) == true)
            {
                EventInterface a = _intensityEvents.Find(evi => evi.instance == ev);
            }
            else
            {
                _intensityEvents.Add(new EventInterface(ev, ev.gameObject.name));
            }
            
        }
        for (int i = 0; i < _intensityEvents.Count; ++i)
        {
            var elem = _intensityEvents[i];
            if (elem.Found == false)
            {
                _intensityEvents.Remove(elem);
                --i;
            }
        }
        Order();

    }
    public void Order()
    {
        _intensityEvents.Sort();
    }
}
