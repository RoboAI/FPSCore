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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
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
            
            _currentSailingSpeed = Mathf.Lerp(_currentSailingSpeed, _targetSailingSpeed, Time.deltaTime * _lerpSailSpeed);
        }
        
    }

    public void SailBoat(bool sail)
    {
        _boatSailing = sail;

        _splineFollower.SetSpeed(_boatSailing ? _currentSailingSpeed : 0);

        _splineFollower.FollowSpline(_boatSailing);
    }
}
