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

        Vector3 localPos = _splineContainer.Spline.EvaluatePosition(_currentSplinePos);
        Vector3 localTangent = _splineContainer.Spline.EvaluateTangent(_currentSplinePos);

        Vector3 worldPosition = _splineContainer.transform.TransformPoint(localPos);
        Vector3 worldForward = _splineContainer.transform.TransformDirection(localTangent.normalized);

        Vector3 excludeYAxis = new Vector3(worldPosition.x, transform.position.y, worldPosition.z);
        transform.position = excludeYAxis;
        transform.rotation = Quaternion.LookRotation(worldForward, Vector3.up);
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
        return _splineContainer.transform.TransformPoint(localPos);
    }

    public Vector3 GetSplineCurrentWorldPosition()
    {
        return GetSplineWorldPositionAtT(_currentSplinePos);
    }

    public Vector3 GetWorldForwardAtT(float t)
    {
        Vector3 localTangent = _splineContainer.Spline.EvaluateTangent(t);
        return _splineContainer.transform.TransformDirection(localTangent.normalized);
    }
}
