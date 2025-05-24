using TMPro;
using UnityEngine;

public class UIManager2 : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Before: {Application.targetFrameRate}, {QualitySettings.vSyncCount}");

        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;

        Debug.Log($"After: {Application.targetFrameRate}, {QualitySettings.vSyncCount}");
    }

    private void Update()
    {
    }
}
