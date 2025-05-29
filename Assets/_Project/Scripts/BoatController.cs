using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] SplineFollower _splineFollower;
    [SerializeField] bool _boatSailing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _boatSailing = !_boatSailing;
            _splineFollower.Sail(_boatSailing);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
