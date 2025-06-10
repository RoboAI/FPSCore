using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject radar;
    bool _active;

    [SerializeField] Transform _raycastPosition;
    [SerializeField] float _groundDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DetectGround();
    }

    void DetectGround()
    {
        Ray _rayGroundDetector = new Ray(_raycastPosition.position, Vector3.left * 10);
        Physics.Raycast(_rayGroundDetector, out RaycastHit hitInfo2);
        {
            _groundDistance = hitInfo2.distance;
        }
    }
}
