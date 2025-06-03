using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneAnimator : MonoBehaviour
{
    [SerializeField]
    public Canvas _canvas;

    private RectTransform _topBorder, _bottomBorder;

    [SerializeField]
    private float _borderOpenedHeightPercentage = 9.0f;//letterbox height

    [SerializeField]
    private float _borderClosedInPercentage = 0.0f;//letterbox cleared height

    private float _calculatedClosedScaleAmount = 0.0f;
    private float _calculatedOpenedScaleAmount = 0.0f;

    [SerializeField]
    public float _durationSeconds = 2.0f;

    private bool _isAnimating = false;
    private float _animTimeElapsed = 0.0f;
    private float _animAlpha;
    private CanvasRenderer _topBorderRenderer;
    private CanvasRenderer _bottomBorderRenderer;
    private float _fullScreenPercentage = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        var g = new GameObject("TopBorder");
        var top = g.AddComponent<LetterBoxBorder>();
        top.CreateBorder("TopBorder", _canvas);
        top.SetAsTopBorder();
        top.transform.SetParent(gameObject.transform, false);

        var b = new GameObject("BottomBorder");
        var bottom = b.AddComponent<LetterBoxBorder>();
        bottom.CreateBorder("BottomBorder", _canvas);
        bottom.SetAsBottomBorder();
        bottom.transform.SetParent(gameObject.transform, false);

        _topBorder = top.GetComponent<LetterBoxBorder>().GetComponent<RectTransform>();
        _bottomBorder = bottom.GetComponent<LetterBoxBorder>().GetComponent<RectTransform>();

        _topBorderRenderer = _topBorder.GetComponent<CanvasRenderer>();
        _bottomBorderRenderer = _bottomBorder.GetComponent<CanvasRenderer>();

        _animAlpha = _topBorderRenderer.GetAlpha();

        SetOpenBorderHeightPercent(_borderOpenedHeightPercentage);
        SetClosedBorderHeightPercent(_borderClosedInPercentage);

        Debug.Log(_canvas.normalizedSortingGridSize.ToString());
        Debug.Log(_canvas.renderingDisplaySize.ToString());

        Debug.Log("Child count: " + gameObject.transform.childCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AnimateToCutSceneStarted();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AnimateToCutSceneEnded();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CoverEntireScreen(_durationSeconds);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ClearEntireScreen(_durationSeconds);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartFadeIn();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StartFadeOut();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
        }
    }

    public float GetScaleFromPercentage(float percent)
    {
        return ((_canvas.pixelRect.height / 100) * percent) / _canvas.scaleFactor;
    }

    public void SetOpenBorderHeightPercent(float borderPercentage)
    {
        _borderOpenedHeightPercentage = borderPercentage;
        _calculatedOpenedScaleAmount = GetScaleFromPercentage(_borderOpenedHeightPercentage);
    }

    public void SetClosedBorderHeightPercent(float borderPercentage)
    {
        _borderClosedInPercentage = borderPercentage;
        _calculatedClosedScaleAmount = GetScaleFromPercentage(_borderClosedInPercentage);
    }

    public void AnimateCutScene(float scaleToValue, float durationSeconds)
    {
        _topBorder.DOScaleY(scaleToValue, durationSeconds);
        _bottomBorder.DOScaleY(scaleToValue, durationSeconds);
    }

    public void AnimateToCutSceneStarted()
    {
        SetOpenBorderHeightPercent(_borderOpenedHeightPercentage);
        SetClosedBorderHeightPercent(_borderClosedInPercentage);
        AnimateCutScene(_calculatedOpenedScaleAmount, _durationSeconds);
    }

    public void AnimateToCutSceneEnded()
    {
        SetOpenBorderHeightPercent(_borderOpenedHeightPercentage);
        SetClosedBorderHeightPercent(_borderClosedInPercentage);
        AnimateCutScene(_calculatedClosedScaleAmount, _durationSeconds);
    }

    public void CoverEntireScreen()
    {
        CoverEntireScreen(_durationSeconds);
    }

    public void CoverEntireScreen(float durationSeconds)
    {
        AnimateCutScene(GetScaleFromPercentage(_fullScreenPercentage), durationSeconds);
    }

    public void ClearEntireScreen()
    {
        ClearEntireScreen(_durationSeconds);
    }

    public void ClearEntireScreen(float durationSeconds)
    {
        AnimateCutScene(GetScaleFromPercentage(0.0f), durationSeconds);
    }

    public void StartFadeOut()
    {
        if (!_isAnimating)
        {
            _isAnimating = true;
            _animTimeElapsed = 0;
            StartCoroutine(AnimateFadeOut());
        }

    }

    public void StartFadeIn()
    {
        if (!_isAnimating)
        {
            _isAnimating = true;
            _animTimeElapsed = 0;
            StartCoroutine(AnimateFadeIn());
        }
    }

    IEnumerator AnimateFadeOut()
    {
        Debug.Log("fade out");
        while (_animAlpha > 0.0f)
        {
            _animTimeElapsed += Time.deltaTime;//not used

            _animAlpha -= 0.01f;
            if(_animAlpha < 0.0f)
                _animAlpha = 0.0f;

            _topBorderRenderer.SetAlpha(_animAlpha);
            _bottomBorderRenderer.SetAlpha(_animAlpha);
            yield return new WaitForSeconds(0.01f);
        }
        _isAnimating = false;
        Debug.Log("fade out ended");
    }

    IEnumerator AnimateFadeIn()
    {
        Debug.Log("fade in");
        while (_animAlpha < 1.0f)
        {
            _animTimeElapsed += Time.deltaTime;//not used

            _animAlpha += 0.01f;
            if (_animAlpha > 1.0f)
                _animAlpha = 1.0f;

            _topBorderRenderer.SetAlpha(_animAlpha);
            _bottomBorderRenderer.SetAlpha(_animAlpha);
            yield return new WaitForSeconds(0.01f);
        }
        _isAnimating = false;
        Debug.Log("fade in ended");
    }
}