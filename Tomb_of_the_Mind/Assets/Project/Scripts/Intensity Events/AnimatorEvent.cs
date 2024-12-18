using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : IntensityEvent
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private List<string> _animationNames;

    private int _currentAnimation = 0;

    public override void Trigger()
    {
        _animator.Play(_animationNames[_currentAnimation]);
        _currentAnimation += 1;
        _currentAnimation %= _animationNames.Count;
    }
}
