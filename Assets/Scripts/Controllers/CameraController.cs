using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public bool UsingOrbitalCamera { get; private set; } = false;
    [SerializeField] HumanoidLandInput _input;
    [SerializeField] float _cameraZoomModifier = 28.0f;
    float _minCameraZoomDistance = 0.0f;
    float _maxCameraZoomDistance = 12.0f;
    float _minOrbitCameraZoomDistance = 1.0f;
    float _maxOrbitCameraZoomDistance = 36.0f;



    CinemachineVirtualCamera _activeCamera;
    CinemachineFramingTransposer _cinemachineFramingTransposer3rdPerson;
    CinemachineFramingTransposer _cinemachineFramingTransposerOrbit;

    int _activeCameraPriorityModifier = 31337;
    public Camera MainCamera;
    public CinemachineVirtualCamera cinemachine1stPerson;
    public CinemachineVirtualCamera cinemachine3rdPerson;
    public CinemachineVirtualCamera cinemachineOrbit;

    private void Awake()
    {
        _cinemachineFramingTransposer3rdPerson = cinemachine3rdPerson.GetCinemachineComponent<CinemachineFramingTransposer>();
        _cinemachineFramingTransposerOrbit = cinemachineOrbit.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    private void Start()
    {
        ChangeCamera(); //first time through, lets set the default camera.
    }
    private void Update()
    {
        if (!(_input.ZoomCameraInput == 0.0f))
        {
            ZoomCamera();
        }
        if (_input.ChangeCameraWasPressedThisFrame)
        {
            ChangeCamera();
        }
    }

    private void ChangeCamera()
    {
        if (cinemachine3rdPerson == _activeCamera)
        {
            cinemachine1stPerson.gameObject.SetActive(true);
            SetCameraPriorities(cinemachine3rdPerson, cinemachine1stPerson);
            cinemachine3rdPerson.gameObject.SetActive(false);
            UsingOrbitalCamera = false;
        }
        else if (cinemachine1stPerson == _activeCamera)
        {
            cinemachineOrbit.gameObject.SetActive(true);
            SetCameraPriorities(cinemachine1stPerson, cinemachineOrbit);
            cinemachine1stPerson.gameObject.SetActive(false);
            UsingOrbitalCamera = true;
        }
        else if (cinemachineOrbit == _activeCamera)
        {
            cinemachine3rdPerson.gameObject.SetActive(true);
            SetCameraPriorities(cinemachineOrbit, cinemachine3rdPerson);
            cinemachineOrbit.gameObject.SetActive(false);
            // _activeCamera = cinemachine3rdPerson;
            UsingOrbitalCamera = false;
        }
        else //for first time through or if there's an error.
        {
            cinemachine3rdPerson.Priority += _activeCameraPriorityModifier;
            _activeCamera = cinemachine3rdPerson;
            cinemachine1stPerson.gameObject.SetActive(false);
            cinemachineOrbit.gameObject.SetActive(false);

        }

    }
    private void SetCameraPriorities(CinemachineVirtualCamera CurrentCameraMode, CinemachineVirtualCamera NewCameraMode)
    {
        CurrentCameraMode.Priority -= _activeCameraPriorityModifier;
        NewCameraMode.Priority += _activeCameraPriorityModifier;
        _activeCamera = NewCameraMode;
    }

    private void ZoomCamera()
    {
        if (_activeCamera == cinemachine3rdPerson)
        {
            _cinemachineFramingTransposer3rdPerson.m_CameraDistance = Mathf.Clamp(_cinemachineFramingTransposer3rdPerson.m_CameraDistance + (_input.InvertScroll ? -_input.ZoomCameraInput : _input.ZoomCameraInput) / _cameraZoomModifier, _minCameraZoomDistance, _maxCameraZoomDistance);
        }
        else if (_activeCamera == cinemachineOrbit)
        {
            _cinemachineFramingTransposerOrbit.m_CameraDistance = Mathf.Clamp(_cinemachineFramingTransposerOrbit.m_CameraDistance + (_input.InvertScroll ? -_input.ZoomCameraInput : _input.ZoomCameraInput) / _cameraZoomModifier, _minOrbitCameraZoomDistance, _maxOrbitCameraZoomDistance);
        }
    }

}
