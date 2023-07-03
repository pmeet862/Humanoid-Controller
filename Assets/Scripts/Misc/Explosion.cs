using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    void Update()
    {
        if (!_particleSystem.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
