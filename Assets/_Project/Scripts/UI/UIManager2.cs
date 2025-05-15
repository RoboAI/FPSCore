using TMPro;
using UnityEngine;

public class UIManager2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textFPS;

    private float _previousTime;
    private int _calculatedFPS;

    private void Update()
    {
        _calculatedFPS = (int)(1 / Time.deltaTime);
        _textFPS.text = _calculatedFPS.ToString();
    }
}
