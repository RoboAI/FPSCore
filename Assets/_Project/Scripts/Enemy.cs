using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoTo(Vector3 new_position)
    {
        transform.position = new_position;
    }

    void Attack(Transform target)
    {

    }

    public void Hello(Collider col)
    {
        col.enabled = false;
    }
}
