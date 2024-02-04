using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    COMP398LAB _inputs;

    [SerializeField] private int _index = 0;
    [SerializeField] private CinemachineVirtualCamera _currentCamera;
    [SerializeField]
    private List<CinemachineVirtualCamera> _virtualCameras =
        new List<CinemachineVirtualCamera>();

    private void Awake()
    {
        InitCameraPriorities();
        _inputs = new COMP398LAB();
        _inputs.Player.Camera.performed += context => MoveCamera(context.ReadValue<float>());
    }

    private void InitCameraPriorities()
    {
        foreach(var camera in _virtualCameras)
        {
            camera.Priority = 0;
        }
        _currentCamera = _virtualCameras[0];
        _currentCamera.Priority = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveCamera(float value)
    {
        _index += (int)value;
        if (_index < 0) _index = _virtualCameras.Count - 1;
        if (_index > _virtualCameras.Count - 1) _index = 0;
        ChangeCamera();
    }

    void ChangeCamera()
    {
        _currentCamera.Priority = 0;
        _currentCamera = _virtualCameras[_index];
        _currentCamera.Priority = 10;
    }


    void OnEnable() => _inputs.Enable();
    void OnDisable() => _inputs.Disable();
}
