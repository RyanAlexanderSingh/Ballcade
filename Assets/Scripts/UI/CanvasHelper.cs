//source: https://forum.unity.com/threads/canvashelper-resizes-a-recttransform-to-iphone-xs-safe-area.521107
 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
[RequireComponent(typeof(Canvas))]
public class CanvasHelper : MonoBehaviour
{
    [SerializeField] private RectTransform safeAreaTransform;

    private static readonly List<CanvasHelper> _helpers = new();
 
    private static UnityEvent _onResolutionOrOrientationChanged = new();
 
    private static bool _screenChangeVarsInitialized = false;
    private static ScreenOrientation _lastOrientation = ScreenOrientation.LandscapeLeft;
    private static Vector2 _lastResolution = Vector2.zero;
    private static Rect _lastSafeArea = Rect.zero;
 
    private Canvas canvas;
 
    private void Awake()
    {
        if(!_helpers.Contains(this))
            _helpers.Add(this);
 
        canvas = GetComponent<Canvas>();
        
        if(!_screenChangeVarsInitialized)
        {
            _lastOrientation = Screen.orientation;
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;
            _lastSafeArea = Screen.safeArea;
 
            _screenChangeVarsInitialized = true;
        }
 
        ApplySafeArea();
    }
 
    private void Update()
    {
        if(_helpers[0] != this)
            return;
 
        if(Application.isMobilePlatform && Screen.orientation != _lastOrientation)
            OrientationChanged();
 
        if(Screen.safeArea != _lastSafeArea)
            SafeAreaChanged();
 
        if(Screen.width != _lastResolution.x || Screen.height != _lastResolution.y)
            ResolutionChanged();
    }
    
    private void OnDestroy()
    {
        if(_helpers != null && _helpers.Contains(this))
            _helpers.Remove(this);
    }

    private void Reset()
    {
        safeAreaTransform = transform.Find("SafeArea") as RectTransform;
    }
 
    private void ApplySafeArea()
    {
        if(safeAreaTransform == null)
            return;
 
        var safeArea = Screen.safeArea;
 
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;
        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;
 
        safeAreaTransform.anchorMin = anchorMin;
        safeAreaTransform.anchorMax = anchorMax;
    }

    private static void OrientationChanged()
    {
        //Debug.Log("Orientation changed from " + lastOrientation + " to " + Screen.orientation + " at " + Time.time);
 
        _lastOrientation = Screen.orientation;
        _lastResolution.x = Screen.width;
        _lastResolution.y = Screen.height;
 
        _onResolutionOrOrientationChanged.Invoke();
    }
 
    private static void ResolutionChanged()
    {
        //Debug.Log("Resolution changed from " + lastResolution + " to (" + Screen.width + ", " + Screen.height + ") at " + Time.time);
 
        _lastResolution.x = Screen.width;
        _lastResolution.y = Screen.height;
 
        _onResolutionOrOrientationChanged.Invoke();
    }
 
    private static void SafeAreaChanged()
    {
        // Debug.Log("Safe Area changed from " + lastSafeArea + " to " + Screen.safeArea.size + " at " + Time.time);
 
        _lastSafeArea = Screen.safeArea;
 
        for (int i = 0; i < _helpers.Count; i++)
        {
            _helpers[i].ApplySafeArea();
        }
    }
}