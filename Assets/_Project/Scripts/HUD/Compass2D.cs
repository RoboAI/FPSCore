using UnityEngine;
using UnityEngine.UI;

public class Compass2D : MonoBehaviour
{
    [Tooltip("Player")]
    [SerializeField] private GameObject _playerOrientation;

    [Tooltip("North")]
    [SerializeField] private Vector3 _worldNorth = Vector3.forward;

    Vector3 _orientationToAngle = new Vector3(0, 0, 0);

    private void Start()
    {
        if(_playerOrientation == null)
            _playerOrientation = GameObject.FindGameObjectWithTag("PlayerOrientation");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 p = Vector3.ProjectOnPlane(_player.transform.forward, Vector3.up);
        //Quaternion northDifference = Quaternion.FromToRotation(Vector3.forward, _player.transform.rotation.eulerAngles);
        //var angle = northDifference.eulerAngles;
        //Debug.Log(_player.transform.rotation.eulerAngles);
        //Vector3 projectedToPlane = Vector3.ProjectOnPlane(northDifference.eulerAngles, Vector3.up);
        ////_compassImage.transform.rotation = Quaternion.LookRotation(_worldNorth);
        ////abc = new Vector3(0, 0, angle);
        ///
        //Vector3 orientationToAngle = _player.transform.rotation.eulerAngles;
        //_orientationToAngle.z = orientationToAngle.y;


        transform.rotation = CalculateAndGetCompassAngle();
    }

    protected Quaternion CalculateAndGetCompassAngle()
    {
        _orientationToAngle.z = _playerOrientation.transform.rotation.eulerAngles.y;
        return Quaternion.Euler(_orientationToAngle);
    }
}
