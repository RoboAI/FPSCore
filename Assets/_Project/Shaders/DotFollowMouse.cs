using UnityEngine;

public class DotFollowMouse : MonoBehaviour
{
    public Material material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse_pos = Input.mousePosition;
        Vector2 mouse_uv = new Vector2(mouse_pos.x / Screen.width, mouse_pos.y / Screen.height);
        material.SetVector("_DotCenter", mouse_uv);
        Debug.Log(mouse_pos.ToString() + " : " + new Vector2(Screen.width, Screen.height));
    }
}
