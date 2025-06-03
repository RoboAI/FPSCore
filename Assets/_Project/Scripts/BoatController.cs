using MalbersAnimations;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] SplineFollower _splineFollower;
    [SerializeField] BoatSway _swayController;
    bool _boatSailing;
    [SerializeField] float _targetSailingSpeed;
    [SerializeField] BoatSwayParams _dockedSwayParams;
    [SerializeField] BoatSwayParams _sailingSwayParams;

    [SerializeField] float _currentSailingSpeed;
    float _timePassed;
    [SerializeField] float _lerpSailSpeed;
    [SerializeField] CameraArrayHandler _boatCameras;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 boatWorldPosition = _splineFollower.GetSplineWorldPositionAtT(0);
        transform.position = new Vector3(boatWorldPosition.x, transform.position.y, boatWorldPosition.z);//exclude y-axis

        Vector3 worldForward = _splineFollower.GetWorldForwardAtT(0.0001f);
        //transform.rotation = Quaternion.Euler(worldForward);
        transform.rotation = Quaternion.LookRotation(worldForward, Vector3.up);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SailBoat(!_boatSailing);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_boatSailing && _currentSailingSpeed < _targetSailingSpeed)
        {
            _timePassed += Time.deltaTime * _lerpSailSpeed;
            float splinePosition = _splineFollower.GetCurrentSplinePosition();

            if (splinePosition < 0.9f)
            {
                _currentSailingSpeed = Mathf.Lerp(_currentSailingSpeed, _targetSailingSpeed, Time.deltaTime * _lerpSailSpeed);
                _splineFollower.SetSpeed(_currentSailingSpeed);
            }

            else if(splinePosition >= 0.9f)
            {
                if(splinePosition < 0.97f)
                    _currentSailingSpeed = Mathf.Lerp(_currentSailingSpeed, 0, (Time.deltaTime * (_lerpSailSpeed * 500)));

                _splineFollower.SetSpeed(_currentSailingSpeed);
            }
        }
    }

    public void SailBoat(bool sail)
    {
        _boatSailing = sail;

        _splineFollower.SetSpeed(_boatSailing ? _currentSailingSpeed : 0);

        _splineFollower.FollowSpline(_boatSailing);
    }
}
