using UnityEngine;

public class HotAirBalloonController : MonoBehaviour
{
    [SerializeField] ParticleSystem _booster;
    [SerializeField] Rigidbody _rb;

    [SerializeField] float _power;
    [SerializeField] float _speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _booster.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            if (!_booster.isPlaying) _booster.Play();

            _rb.AddForce(new Vector3(0, Time.deltaTime * _power * _speed, 0));

        }
        else if (_booster.isPlaying)
            _booster.Stop();
    }
}
