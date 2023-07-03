using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulator : MonoBehaviour
{
    const float GravitationalConstant = 0.667408f;

    [SerializeField] HumanoidLandInput _input;
    [SerializeField] OnScreenCounter _counter;

    MeshRenderer _meshRenderer;

    private List<Rigidbody> Attractees = new List<Rigidbody>();

    [SerializeField] bool _manipulatorIsEnabled = false;
    [SerializeField] bool _manipulatorToggledOn = false;
    [SerializeField] bool _manipulatorModeToggled = false;

    private string _playerTag = "Player";
    private string _projectileTag = "Projectile";


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (_input.OnOffWasPressedThisFrame) { OnOff(); }
        if (_input.ModeWasPressedThisFrame) { ChangeMode(); }
        _manipulatorToggledOn = _input.ActivateInput > 0.0f;
        SetColor();
    }

    private void FixedUpdate()
    {
        if (_manipulatorIsEnabled || _manipulatorToggledOn)
        {

            foreach (Rigidbody attractee in Attractees)
            {
                if (attractee != this)
                {
                    Attract(attractee);
                }
            }
        }
    }

    private void OnOff()
    {
        _manipulatorIsEnabled = !_manipulatorIsEnabled;
    }

    private void ChangeMode()
    {
        _manipulatorModeToggled = !_manipulatorModeToggled;
    }

    private void SetColor() // Attract = Blue  & Repel = Red
    {
        if (_manipulatorIsEnabled || _manipulatorToggledOn)
        {
            if (_manipulatorModeToggled)
            {
                _meshRenderer.material.color = Color.red;
            }
            else
            {
                _meshRenderer.material.color = Color.blue;
            }
        }
        else
        {
            if (_manipulatorModeToggled)
            {
                _meshRenderer.material.color = Color.white + Color.red;
            }
            else
            {
                _meshRenderer.material.color = Color.white + Color.blue;
            }
        }
    }

    private void Attract(Rigidbody rbToAttract)
    {
        // if (_manipulatorIsEnabled || _manipulatorToggledOn)
        // {
        Vector3 direction = transform.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0.0f) { return; } // Note: if on top of each other then don't apply any force (exit). Or a minimum distance could be set.

        float forceMagnitude = 0.0f;
        if (_input.ActivateInput > 0.0f)
        {
            forceMagnitude = GravitationalConstant * (750.0f * rbToAttract.mass) / distance * _input.ActivateInput;
        }
        else
        {
            forceMagnitude = GravitationalConstant * (750.0f * rbToAttract.mass) / distance;
        }
        Vector3 force = direction.normalized * forceMagnitude;

        if (_manipulatorModeToggled)
        {
            rbToAttract.AddForce(-force);
        }
        else
        {
            rbToAttract.AddForce(force);
        }
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.attachedRigidbody == null) && !(other.attachedRigidbody.isKinematic) && !(other.gameObject.CompareTag(_projectileTag)))
        {
            if (!(Attractees.Contains(other.attachedRigidbody)))
            {
                Attractees.Add(other.attachedRigidbody);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Attractees.Contains(other.attachedRigidbody))
        {
            if (_manipulatorIsEnabled || _manipulatorToggledOn)
            {
                other.attachedRigidbody.useGravity = false;
                if (!(other.gameObject.CompareTag(_playerTag)))
                {
                    if (Vector3.Distance(transform.position, other.transform.position) < 0.5f + other.bounds.extents.y)
                    {
                        Attractees.Remove(other.attachedRigidbody);
                        Destroy(other.gameObject);
                        _counter.Counter++;
                    }
                }
            }
            else
            {
                if (!(other.gameObject.CompareTag(_playerTag)))
                {
                    other.attachedRigidbody.useGravity = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(other.attachedRigidbody == null))
        {
            if (Attractees.Contains(other.attachedRigidbody))
            {
                Attractees.Remove(other.attachedRigidbody);
                if (!(other.gameObject.CompareTag(_playerTag)))
                {
                    other.attachedRigidbody.useGravity = true;
                }
            }
        }
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (_manipulatorIsEnabled || _manipulatorToggledOn)
    //     {
    //         if (Attractees.Contains(collision.rigidbody))
    //         {
    //             if (!(collision.gameObject.CompareTag(_playerTag)) || !(collision.gameObject.CompareTag(_projectileTag)))
    //             {
    //                 Attractees.Remove(collision.rigidbody);
    //                 Destroy(collision.gameObject);
    //                 _counter.Counter++;
    //             }
    //         }
    //     }

    // }
}
