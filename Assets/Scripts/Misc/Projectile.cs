using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _projectileSpeedMultiplier = 200.0f;
    [SerializeField] GameObject _explosion;
    [SerializeField] MeshRenderer _meshRenderer;
    // [SerializeField] ParticleSystem _particleSystem;
    bool _forceApplied = true;

    private void FixedUpdate()
    {
        if (_forceApplied)
        {
            Shoot();
        }
        if (transform.position.y < -8.0f || transform.position.y > 100.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void Shoot()
    {
        Vector3 direction = transform.forward;
        Vector3 force = direction * _projectileSpeedMultiplier * _rigidbody.mass;
        _rigidbody.AddForce(force, ForceMode.Impulse);
        _forceApplied = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Ground"))
        // {
        _meshRenderer.enabled = false;
        Instantiate(_explosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject, 0.075f);
        // }
    }
}
