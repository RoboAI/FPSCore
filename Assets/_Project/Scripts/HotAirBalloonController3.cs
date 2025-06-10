using UnityEngine;

public class HotAirBalloonController3 : MonoBehaviour
{
    [SerializeField] ParticleSystem _booster;
    [SerializeField] Rigidbody _rb;
    [SerializeField] KinematicBody _kb;
    [SerializeField] bool _isKinematic;

    [SerializeField] float _power;
    [SerializeField] float _speed;

    [SerializeField] Transform _raycastPosition;
    [SerializeField] float _raycastGroundDistance;
    [SerializeField] bool _isAirborne;

    [SerializeField] Vector3 _verticalVelocityLimit;
    [SerializeField] float _maxSpeed;
    Ray _rayGroundDetector;

    [SerializeField] Vector3 _vel;
    [SerializeField] Vector3 _drag;
    [SerializeField] Vector3 _acceleration;

    [Tooltip("Debug")]
    [SerializeField] float _groundDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb.isKinematic = _isKinematic;

        _rayGroundDetector = new Ray(_raycastPosition.position, Vector3.down * 10);
        _booster.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            _booster.Play();
        else if (Input.GetKeyUp(KeyCode.J))
            _booster.Stop();

        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.J))
        {
            _rb.AddForce(new Vector3(0, Time.deltaTime * _power * _speed, 0));
            //_rb.MovePosition(transform.position + new Vector3(0, Time.deltaTime * _power * _speed, 0));
            //_kb.AddForce(new Vector3(0, (Time.deltaTime * _power) * _speed, 0));
        }
DetectGround();
        Vector3 vel = _rb.linearVelocity;
        vel.x = Mathf.Clamp(vel.x, -_maxSpeed, _maxSpeed);
        vel.y = Mathf.Clamp(vel.y, -_maxSpeed, _maxSpeed);
        vel.z = Mathf.Clamp(vel.z, -_maxSpeed, _maxSpeed);
        if (vel.x >= -0.00001f && vel.x <= 0.00001f) vel.x = 0.0f;
        if (vel.y >= -0.00001f && vel.y <= 0.00001f) vel.y = 0.0f;
        if (vel.z >= -0.00001f && vel.z <= 0.00001f) vel.z = 0.0f;

        _vel = vel;
        _rb.linearVelocity = vel;
    }

    void DetectGround()
    {
        Physics.Raycast(_rayGroundDetector, out RaycastHit hitInfo2);
        {
            _groundDistance = hitInfo2.distance;
            if (hitInfo2.distance <= _raycastGroundDistance)
                _isAirborne = false;
        }
    }

}
