using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBoxBorder : MonoBehaviour
{
    Canvas _canvas;

    public void CreateBorder(String name, Canvas canvas)
    {
        _canvas = canvas;

        var rt = gameObject.AddComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 1.0f);
        rt.offsetMin = new Vector2(0.0f, 0.0f);
        rt.offsetMax = new Vector2(0.5f, 1.0f);
        rt.sizeDelta = new Vector2(canvas.renderingDisplaySize.x / canvas.scaleFactor, 1);
        rt.localScale = new Vector3(1, 0, 1);
        rt.rotation = Quaternion.Euler(0, 0, 0);
        rt.position = new Vector3(0, (canvas.renderingDisplaySize.y / canvas.scaleFactor) / 2, 0);

        var image = gameObject.AddComponent<Image>();
        image.enabled = true;
        image.color = Color.black;
    }

    public void SetAsTopBorder()
    {
        var rt = gameObject.GetComponent<RectTransform>();
        rt.rotation = Quaternion.Euler(0, 0, 0);
        rt.position = new Vector3(0, (_canvas.renderingDisplaySize.y / _canvas.scaleFactor) / 2, 0);
    }

    public void SetAsBottomBorder()
    {
        var rt = gameObject.GetComponent<RectTransform>();
        rt.rotation = Quaternion.Euler(0, 0, 180);
        rt.position = new Vector3(0, 0 - ((_canvas.renderingDisplaySize.y / _canvas.scaleFactor) / 2), 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
