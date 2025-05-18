using UnityEngine;

public class TextureOffset : MonoBehaviour
{
    Material _material;
    public Vector2 _offset = new Vector2(0.0001f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_material == null)
            _material = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _material.mainTextureOffset += _offset;
    }
}
