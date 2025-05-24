using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FPSViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textFPS;

    private float[] _fpsArray;
    private float _previousTime;
    private int _calculatedFPS;
    private float _previousValue;
    private float _totalFPS;
    private float _finalFPS;

    [SerializeField] private int _arraySize;
    private int _currentIndex;
    private void Start()
    {
        _fpsArray = new float[_arraySize];
    }

    private void Update()
    {
        AddFPSValue(1 / Time.deltaTime);
    }

    void AddFPSValue(float value)
    {
        //_totalFPS -= _previousValue;

        //_previousValue = _fpsArray[_currentIndex];

        //_totalFPS += value;
       // _fpsArray[_currentIndex] = value;

        _fpsArray[_currentIndex] = value;
        _totalFPS += value;
        _currentIndex++;

        if (_currentIndex >= _fpsArray.Length)
        {
            _finalFPS = (int)(_totalFPS / _arraySize);
            _textFPS.text = _finalFPS.ToString();
            _currentIndex = 0;
            _totalFPS = 0;
        }
    }
}
