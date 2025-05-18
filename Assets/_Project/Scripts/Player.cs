using Akila.FPSFramework;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Actor _mainActor;
    public GameObject _orientation;
    Transform _aimDirection;
    Vector3 _velocity;
    Vector3 _predictedLanding;
    PlayerInputBatch _currentAction;

    private void Start()
    {
        if(_orientation == null)
            _orientation = GameObject.FindGameObjectWithTag("PlayerOrientation");
    }
}