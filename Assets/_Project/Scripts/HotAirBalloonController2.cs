using UnityEngine;

public class HotAirBalloonController2 : MonoBehaviour
{
    [SerializeField] ParticleSystem _booster;
    [SerializeField] Rigidbody _rb;

    [SerializeField] float _power;
    [SerializeField] float _speed;

    [SerializeField] Transform _raycastPosition;
    [SerializeField] float _raycastGroundDistance;
    [SerializeField] bool _isAirborne;

    [SerializeField] Vector3 _verticalVelocityLimit;
    [SerializeField] float _maxSpeed;
    Ray _rayGroundDetector;

    [SerializeField] bool _applyPhysics;

    [SerializeField] Vector3 _vel;

    [Tooltip("Debug")]
    [SerializeField] float _groundDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        if (Physics.Raycast(_rayGroundDetector, out RaycastHit hitInfo2))
        {
            _groundDistance = hitInfo2.distance;
            if (hitInfo2.distance <= _raycastGroundDistance)
                _isAirborne = false;
        }

        _vel = _rb.linearVelocity;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.J))
        {
            _applyPhysics = true;
            _rb.AddForce(new Vector3(0, Time.deltaTime * _power * _speed, 0));
        }

        if (_applyPhysics)// || _isAirborne)
        {
            //_rb.AddForce(new Vector3(0, Time.deltaTime * _power * _speed, 0));
        }
        {
            Vector3 vel = _rb.linearVelocity;
            vel.x = Mathf.Clamp(vel.x, -_maxSpeed, _maxSpeed);
            vel.y = Mathf.Clamp(vel.y, -_maxSpeed, _maxSpeed);
            vel.z = Mathf.Clamp(vel.z, -_maxSpeed, _maxSpeed);
            _rb.linearVelocity = vel;
        }
    }

}
    