using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager2 : MonoBehaviour
{
    [SerializeField] bool _VSync = true;
    [HideInInspector] public int _targetFrameRate;
    [HideInInspector] public int _vsyncCount;

    [SerializeField] bool _captureMouse;

    private void Start()
    {
        _targetFrameRate = Application.targetFrameRate;
        _vsyncCount = QualitySettings.vSyncCount;
        SetVSync(_VSync);

        if(_captureMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;  // Locks to center
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_captureMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;  // Locks to center
                Cursor.visible = false;
                _captureMouse = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _captureMouse = true;
            }
        }
    }

    void SetVSync(bool bOn)
    {
        Debug.Log($"Before: {Application.targetFrameRate}, {QualitySettings.vSyncCount}");

        if (bOn)
        {
            Application.targetFrameRate = _targetFrameRate;
            QualitySettings.vSyncCount = _vsyncCount;
        }
        else
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;
        }

        Debug.Log($"After: {Application.targetFrameRate}, {QualitySettings.vSyncCount}");
        Debug.Log("VSync has been changed");
    }
}
