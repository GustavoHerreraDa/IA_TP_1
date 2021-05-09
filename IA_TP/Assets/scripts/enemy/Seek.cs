using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : ISteering
{
    Transform _target;
    Transform _transform;
    float _distance;

    public Seek(Transform to, Transform from, float dist)
    {
        _target = to;
        _transform = from;
        _distance = dist;
    }

    public Vector3 GetDirection()
    {
        if (Vector3.Distance(_target.position, _transform.position) > _distance)
        {
            Vector3 direction = (_target.position - _transform.position).normalized;
            return direction;
        }
        else return Vector3.zero;
    }
}
