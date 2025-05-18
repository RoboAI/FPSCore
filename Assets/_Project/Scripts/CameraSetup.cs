using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public bool _bDisableOnStartup;

    void Start()
    {
        gameObject.SetActive(!_bDisableOnStartup);
    }
}
