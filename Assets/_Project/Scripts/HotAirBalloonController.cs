using UnityEngine;

public class HotAirBalloonController : MonoBehaviour
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         _rayGroundDetector = new Ray(_raycastPosition.position, Vector3.down);
        _booster.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!_booster.isPlaying) _booster.Play();
            _isAirborne = true;
        }
        if (Input.GetKey(KeyCode.J))
        {
            _applyPhysics = true;
            _rb.AddForce(new Vector3(0, Time.deltaTime * _power * _speed, 0));
        }
        else if (_booster.isPlaying || !Input.GetKey(KeyCode.J))
        {
            _booster.Stop();
            _applyPhysics = false;
        }
        else if (_isAirborne)
        {
            if(Physics.Raycast(_rayGroundDetector, out RaycastHit hitInfo))
            {
                if (hitInfo.distance <= _raycastGroundDistance)
                    _isAirborne = false;
            }
        }

        _vel = _rb.linearVelocity;
    }

    private void FixedUpdate()
    {
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
