using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] GameObject _projectile;
    [SerializeField] HumanoidLandInput _input;
    [SerializeField] OnScreenCounter _counter;
    [SerializeField] int _numberOfBoxToCollect = 10; //Number of collected boxes that are required for shooting projectile

    private void FixedUpdate()
    {
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        if (_input.ShootWasPressedThisFrame && _counter.Counter >= _numberOfBoxToCollect)
        {
            Instantiate(_projectile, transform.position, transform.rotation);
            _counter.Counter = _counter.Counter - _numberOfBoxToCollect;
        }
    }
}
