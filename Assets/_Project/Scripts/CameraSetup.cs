using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public bool _disableOnStartup;

    void Start()
    {
        gameObject.SetActive(!_disableOnStartup);
    }
}
