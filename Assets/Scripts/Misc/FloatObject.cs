using System;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    Rigidbody _rigidbody = null;
    BoxCollider _boxCollider = null;

    [Header("Config")]
    [SerializeField][Range(0.0f, 10.0f)] float _desiredColliderFloatHeight = 2.0f;
    [SerializeField][Range(0.0f, 100.0f)] float _floatDistanceModifier = 10.0f;
    [SerializeField][Range(0.0f, 100.0f)] float _floatVelModifier = 20.0f;

    [Header("Collider Float Values")]
    [SerializeField] Vector3 _calculatedForce = Vector3.zero;
    [SerializeField] float _centerToGroundDistance = 0.0f;
    [SerializeField] float _floatForce = 0.0f;

    RaycastHit _groudCheckHit = new RaycastHit();



    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float boxHalfHeight = _boxCollider.bounds.extents.y;
        Physics.Raycast(new Vector3(_rigidbody.position.x, _rigidbody.position.y, _rigidbody.position.z), -transform.up, out _groudCheckHit);
        _centerToGroundDistance = _groudCheckHit.distance - boxHalfHeight - _desiredColliderFloatHeight;
        _calculatedForce.y = Float();
        _rigidbody.AddRelativeForce(_calculatedForce, ForceMode.Force);
    }

    private float Float()
    {
        float calculatedFloatForce = _calculatedForce.y;
        float dotDownVel = Vector3.Dot(-_groudCheckHit.normal, _rigidbody.velocity);
        _floatForce = (_centerToGroundDistance * _floatDistanceModifier) - (dotDownVel * _floatVelModifier);

        calculatedFloatForce -= _floatForce;
        return calculatedFloatForce;
    }
}
