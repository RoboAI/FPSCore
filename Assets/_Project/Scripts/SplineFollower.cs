using MalbersAnimations.Controller;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplineFollower : MonoBehaviour
{
    [SerializeField] SplineContainer _splineContainer;

    [SerializeField] bool _followSpline;
    [SerializeField] public float _speed;
    [SerializeField] float _currentSplinePos;
    //float _timePassed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (_splineContainer == null || _followSpline == false)
            return;

        //_timePassed += Time.deltaTime;
        _currentSplinePos += Time.deltaTime * _speed;

        //set position as spline's current position
        Vector3 worldPosition = GetSplineWorldPositionAtT(_currentSplinePos);
        transform.position = new Vector3(worldPosition.x, transform.position.y, worldPosition.z);//exclude y-axis

        //set rotation as spline's current rotation
        transform.rotation = Quaternion.LookRotation(GetWorldForwardAtT(_currentSplinePos), Vector3.up);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void FollowSpline(bool sail)
    {
        _followSpline = sail;
    }

    public float GetCurrentSplinePosition()
    {
        return _currentSplinePos;
    }

    public Vector3 GetSplineWorldPositionAtT(float t)
    {
        Vector3 localPos = _splineContainer.Spline.EvaluatePosition(t);
        Vector3 worldPos = _splineContainer.transform.TransformPoint(localPos);
        return worldPos;
    }

    public Vector3 GetSplineCurrentWorldPosition()
    {
        return GetSplineWorldPositionAtT(_currentSplinePos);
    }

    public Vector3 GetWorldForwardAtT(float t)
    {
        Vector3 localTangent = _splineContainer.Spline.EvaluateTangent(t);
        Vector3 worldForward = _splineContainer.transform.TransformDirection(localTangent.normalized);
        return worldForward;
    }
}
