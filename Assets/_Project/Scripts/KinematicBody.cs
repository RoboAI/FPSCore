using UnityEngine;

public class KinematicBody : MonoBehaviour
{
    public Rigidbody _rb;
    public float drag;
    public Vector3 linearVelocity;
    public Vector3 _acceleration;

    Vector3 _drag;
    Vector3 _gravity;

    void Start()
    {
        _gravity = Physics.gravity;
        _drag = new Vector3(drag, drag, drag);
    }


    public void AddForce(Vector3 force)
    {
        _acceleration += force;
    }

    public void UpdateKinematicPhysics()
    {
        //if (_acceleration == Vector3.zero && linearVelocity == Vector3.zero)
        //    return;

        _acceleration += (_gravity * Time.deltaTime) * 0.01f;
        _acceleration -= _drag;
        //_acceleration -=  Time.deltaTime;
        linearVelocity += _acceleration * Time.deltaTime;
        //transform.position += linearVelocity;
        _rb.MovePosition(transform.position + linearVelocity);

        

    }
}
