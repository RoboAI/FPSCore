using UnityEngine;
using UnityEngine.UI;

public class CompassStrip : Compass2D
{
    [Tooltip("Compass strip image")]
    [SerializeField]
    private Image _stripImage;

    private Vector2 _textureOffset;
    private float _stripDegressRatio = 1 / 360.0f;

    private MaterialPropertyBlock mpb;
    private Renderer rend;
    private int _textureNameID;
    private Vector4 _textureValues;


    private void Start()
    {
        _stripImage.material = new Material(_stripImage.material);
        _textureOffset = new Vector2(0, 0);

        //_textureNameID = Shader.PropertyToID("_MainTex_ST");
        //rend = _stripImage.GetComponent<Renderer>();
        //mpb = new MaterialPropertyBlock();
        //_textureValues = new Vector4(1, 1, 0, 0);
    }

    private void Update()
    {
        _textureOffset.x = CalculateAndGetCompassAngle().eulerAngles.z * _stripDegressRatio;
        _stripImage.material.mainTextureOffset = _textureOffset;

        //Debug.Log(CalculateAndGetCompassAngle().eulerAngles);

        //_textureValues.z += 0.1f;
        //rend.GetPropertyBlock(mpb);
        //mpb.SetVector("_MainTex_ST", _textureValues);
        //rend.SetPropertyBlock(mpb);
    }
}
