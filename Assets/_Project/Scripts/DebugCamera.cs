using UnityEngine;

public class DebugCamera : MonoBehaviour
{
    [SerializeField] Camera _camera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            _camera.gameObject.SetActive(!_camera.gameObject.activeSelf);
    }
}
