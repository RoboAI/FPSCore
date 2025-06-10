using UnityEngine;

public class GraphicsSetup : MonoBehaviour
{
    [Tooltip("Screen Resolutiom")]
    [SerializeField] bool _useDefault;
    [SerializeField] int _screenWidth;
    [SerializeField] int _screenHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!_useDefault)
            SetScreenResolution(_screenWidth, _screenHeight);
    }

    public void SetScreenResolution(int widht, int height)
    {
        Screen.SetResolution(widht, height, true);
    }
}
