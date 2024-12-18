using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEvent : IntensityEvent
{
    /// <summary>
    /// Objects to be enabled over time
    /// </summary>
    [SerializeField]
    private List<GameObject> _targetObjects;
    /// <summary>
    /// If it should enable all objects at once instead
    /// </summary>
    [SerializeField]
    private bool _enableAtOnce;
    /// <summary>
    /// If the trigger should work like a switch instead, and it would enable and then disable each object.
    /// </summary>
    [SerializeField]
    private bool _triggerSwitch;
    /// <summary>
    /// If it should disable all objects on Awake.
    /// </summary>
    [SerializeField]
    private bool _disableOnLoad;

    private int _currentIndex = -1;
    private void Awake()
    {
        foreach (var item in _targetObjects)
        {
            item.SetActive(false);
        }
    }
    public override void Trigger()
    {
        if (_enableAtOnce)
        {
            foreach (var obj in _targetObjects)
            {
                if (_triggerSwitch)
                    obj.SetActive(!obj.activeSelf);
                else
                    obj.SetActive(true);
            }
        }
        else
        {
            if (_triggerSwitch && _currentIndex != -1)
                _targetObjects[_currentIndex].SetActive(false);
            _currentIndex += 1;
            _currentIndex %= _targetObjects.Count;

            _targetObjects[_currentIndex].SetActive(true);
        }
    }
}
