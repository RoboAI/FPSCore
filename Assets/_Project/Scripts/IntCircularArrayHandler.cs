using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class IntCircularArrayHandler
{
    public int _arraySize;
    public int _currentIndex;

    public int GetNextNumber()
    {
        _currentIndex++;
        if (_currentIndex >= _arraySize)
            _currentIndex = 0;

        return _currentIndex;
    }
}