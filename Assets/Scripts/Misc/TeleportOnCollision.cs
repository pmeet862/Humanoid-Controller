using System.Collections;
using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    [SerializeField] Transform _teleportTo;
    [SerializeField] GameObject _teleportationControl;
    [SerializeField] GameObject _buttonHoldingLoading;
    [SerializeField] HumanoidLandInput _input;
    [SerializeField] ProgressBar _loadingBar;
    [SerializeField] ParticleSystem _particleSystem;

    GameObject _gameobjectToTeleport;

    string _playerTag = "Player";
    bool _isTeleported = false;


    private void FixedUpdate()
    {
        // if (_input.TeleportIsPressed)
        // {
        //     ToggleParticleSystem();
        // }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_playerTag))
        {
            _teleportationControl.SetActive(true);
            _gameobjectToTeleport = other.gameObject;
            _isTeleported = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Q is presed: " + _input.TeleportIsPressed);
        if (other.gameObject.CompareTag(_playerTag))
        {
            if (_input.TeleportIsPressed)
            {
                _buttonHoldingLoading.SetActive(true);
                StartCoroutine(Teleport());
                if (!_particleSystem.isPlaying)
                    _particleSystem.Play();
                // other.gameObject.transform.position = _teleportTo.position + transform.up * 50.0f;
                // other.attachedRigidbody.velocity = Vector3.zero;
            }
            else
            {
                _buttonHoldingLoading.SetActive(false);
                if (_particleSystem.isPlaying)
                    _particleSystem.Stop();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_playerTag))
        {
            _teleportationControl.SetActive(false);
            _buttonHoldingLoading.SetActive(false);
            if (_particleSystem.isPlaying)
                _particleSystem.Stop();
            // _gameobjectToTeleport = null;
        }
    }

    IEnumerator Teleport()
    {
        float timeLeft = 3.0f;


        while (timeLeft > 0.0f && _input.TeleportIsPressed)
        {
            _loadingBar.IncreamentProgress(1.0f);
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
        }


        if (_input.TeleportIsPressed && (_gameobjectToTeleport != null) && !_isTeleported && _loadingBar._progressBarVal == 3.0f)
        {
            _isTeleported = true;
            _gameobjectToTeleport.transform.position = _teleportTo.position + transform.up * 50.0f;
            _gameobjectToTeleport.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _buttonHoldingLoading.SetActive(false);
        }
    }

    // private void ToggleParticleSystem()
    // {

    //     if (_particleSystem.isPlaying)
    //     {
    //         _particleSystem.Stop();
    //     }
    //     else
    //     {
    //         _particleSystem.Play();
    //     }
    // }

}
