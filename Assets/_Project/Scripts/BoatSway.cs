using UnityEngine;

public class BoatSway : MonoBehaviour
{
    public float _xSpeed = 0.02f;//pitch
    public float _zSpeed = 0.01f;//roll
    public float _pitchFactor = 0.06f;
    public float _rollFactor = 0.04f;

    [Tooltip("Debug")]
    public float _x;
    public float _z;

    public float lookAtSpeed = 2.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SwayBoatUsingTrig();
    }

    void SwayBoatUsingTrig()
    {
        _x += _xSpeed;
        _z += _zSpeed;
        float x = Mathf.Sin(_x);
        float z = Mathf.Cos(_z);
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.x = x * Mathf.Rad2Deg * _pitchFactor;
        currentRotation.z = z * Mathf.Rad2Deg * _rollFactor;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    //not working
    void SwayBoatUsingPerlinNoise()
    {
        _x += _xSpeed;
        _z += _zSpeed;
        float x = Mathf.PerlinNoise1D(_x);
        float z = Mathf.PerlinNoise1D(_z);
        Debug.Log(x);
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.x = x * _pitchFactor;
        currentRotation.z = z * _rollFactor;
        transform.rotation = Quaternion.Euler(currentRotation);

        float f = Mathf.PerlinNoise(15, 30);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), lookAtSpeed * Time.deltaTime);
    }


}
