using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CameraArrayHandler : MonoBehaviour
{
    public enum ShipCameraType//not used
    {
        Main,
        Front,
        LeftDock1,
        LeftDock2,
        RightDock1,
        RightDock2,
        Rear
    }

    [SerializeField] List<Camera> _cameras;
    //[SerializeField] Camera _defaultActiveCamera;

    IntCircularArrayHandler _intCircularArrayHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _intCircularArrayHandler = new IntCircularArrayHandler();

        if (_cameras == null || _cameras.Count <= 0)
            return;

        _intCircularArrayHandler._arraySize = _cameras.Count;

        for (int i = 0; i < _cameras.Count; i++)
            _cameras[i].gameObject.SetActive(false);
            //_cameras[i].enabled = false;

        //_cameras[0].gameObject.SetActive(true);

        ///////////////////
        //default-camera implementation. WORKING but not used
        //////////////////
        //int foundIndex = _cameras.FindIndex(p => p == _defaultActiveCamera);
        //if (foundIndex >= 0)
        //    _cameras[foundIndex].enabled = true;
        //else
        //{
        //    _cameras[0].enabled = true;
        //    Debug.LogWarning("Ship's default camera is not in the list of cameras");
        //}
        //////////////////
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            EnableNextCamera();

        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleCurrentCamera();
        }
    }

    public void ToggleCurrentCamera()
    {
        bool camState = _cameras[_intCircularArrayHandler._currentIndex].gameObject.activeSelf;
        _cameras[_intCircularArrayHandler._currentIndex].gameObject.SetActive(!camState);
        //bool camState = _cameras[_intCircularArrayHandler._currentIndex].enabled;
        //_cameras[_intCircularArrayHandler._currentIndex].enabled = !camState;
    }

    public void SetCameraActive(ShipCameraType camType)
    {
        if (_cameras == null || _cameras.Count <= 0)
            return;

        Debug.LogWarning("not yet implemented");
    }

    public void EnableNextCamera()
    {
        _cameras[_intCircularArrayHandler._currentIndex].gameObject.SetActive(false);
        _cameras[_intCircularArrayHandler.GetNextNumber()].gameObject.SetActive(true);
        //_cameras[_intCircularArrayHandler._currentIndex].enabled = false;
        //_cameras[_intCircularArrayHandler.GetNextNumber()].enabled = true;
    }
}
