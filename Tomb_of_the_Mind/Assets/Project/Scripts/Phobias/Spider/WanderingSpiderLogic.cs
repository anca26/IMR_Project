using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class WanderingSpiderLogic : MonoBehaviour
{
    [SerializeField]
    private float _headingDirectionXOffset;
    [SerializeField]
    private float _headingDirectionWidth;
    [SerializeField]
    private float _headingDirectionYOffset;

    [SerializeField, Range(0,10)]
    private float _moveSpeed;
    [SerializeField, Range(0,10)]
    private float _maxMoveDuration;


    [SerializeField]
    private float _attemptDelay;
    [SerializeField, Range(0,1)]
    private float _attemptChance;

    [SerializeField,Range(0,360)]
    private float _rotationInterval;

    [SerializeField]
    private LayerMask _floorMask;


    private float _nextChangeTime;
    private float _stopTime;
    private Vector3 _forwardDirection;
    private bool _moving = false;
    private Animator _animator;
    private float _distanceToFloor;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _nextChangeTime = Time.time + _attemptDelay;

        RaycastHit hitPos;
        Vector3 rayOrigin = transform.position - transform.forward * _headingDirectionXOffset + Vector3.up * _headingDirectionYOffset;

        // if there is still a floor, we check the distance to it
        if (Physics.Raycast(rayOrigin, Vector3.down, out hitPos, Mathf.Infinity))
        {
            _distanceToFloor = hitPos.distance;
        }
    }
    private void Update()
    {
        if (Time.time >= _nextChangeTime)
        {
            _nextChangeTime = Time.time + _attemptDelay;

            float r = Random.Range(0.0f, 1.0f);

            if (r <= _attemptChance)
            {
                ChangeDirection();

                _stopTime = Time.time + Random.Range(0.1f, _maxMoveDuration);
                _nextChangeTime = _stopTime + _attemptDelay;
            }

        }
        
    }
    private void FixedUpdate()
    {

        if (Time.time <= _stopTime)
        {
            AttemptMove();
            if (!_moving)
                ChangeMoveState(true);
        }
        else
        {
            if(_moving)
                ChangeMoveState(false);
        }
    }
    private void ChangeDirection()
    {
        transform.Rotate(Vector3.up * Random.Range(-_rotationInterval, _rotationInterval));
        _forwardDirection = -transform.forward; // or, Mathf.cross(transform.up,transform.right)
    }
    private void ChangeMoveState(bool moveState)
    {
        _moving = moveState;
        _animator.SetBool("Moving", _moving);
        _animator.SetFloat("MoveOffset", Random.Range(0f, 1f));
    }
    private void AttemptMove()
    {
        if(CheckSurroundings())
            gameObject.transform.position += _forwardDirection * _moveSpeed * Time.fixedDeltaTime;
    }
    private bool CheckSurroundings()
    {
        RaycastHit hitPos;
        Vector3 rayOrigin = transform.position - transform.forward * _headingDirectionXOffset + Vector3.up * _headingDirectionYOffset;

        // if there is still a floor, we check the distance to it
        if (Physics.Raycast(rayOrigin, Vector3.down, out hitPos, Mathf.Infinity,_floorMask))
        {
            // if there is an edge, we stop moving
            if (hitPos.distance != _distanceToFloor)
            {
                _stopTime = Time.time;
                _nextChangeTime = Time.time + _attemptDelay / 10;
                return false;
            }
        }
        // if there is a wall in front, we stop moving
        if(Physics.Raycast(rayOrigin,-transform.forward,0.01f))
        {
            _stopTime = Time.time;
            return false;
        }
        if (Physics.Raycast(rayOrigin + transform.right * _headingDirectionWidth / 2f, -transform.forward, 0.01f))
        {
            _stopTime = Time.time;
            return false;
        }
        if (Physics.Raycast(rayOrigin - transform.right * _headingDirectionWidth / 2f, -transform.forward, 0.01f))
        {
            _stopTime = Time.time;
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + -transform.forward * _headingDirectionXOffset + Vector3.up * _headingDirectionYOffset, 1 * transform.localScale.x);
        Gizmos.DrawWireSphere(transform.position + -transform.forward * _headingDirectionXOffset + Vector3.up * _headingDirectionYOffset + transform.right * _headingDirectionWidth / 2f, 1 * transform.localScale.x);
        Gizmos.DrawWireSphere(transform.position + -transform.forward * _headingDirectionXOffset + Vector3.up * _headingDirectionYOffset - transform.right * _headingDirectionWidth / 2f, 1 * transform.localScale.x);

    }
}
