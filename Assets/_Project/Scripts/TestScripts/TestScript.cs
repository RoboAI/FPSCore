using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject radar;
    bool _active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
