using UnityEngine;
using UnityEngine.UI;

public class UICanvasPixelColorPicker : MonoBehaviour
{
    public Color CurrentColor => _currentColor;

    [SerializeField] private Image colorPickerHandle;

    private IColorPicker _colorPicker;
    private RectTransform _handleRectTransform;
    private bool _isInitialized;
    private Color _currentColor;

    public bool Initialize(IColorPicker colorPicker)
    {
        if (colorPickerHandle == null)
        {
            Debug.LogError($"{nameof(UICanvasPixelColorPicker)}: ColorPickerHandle is not set.");
            return false;
        }

        _colorPicker = colorPicker ??
                       throw new System.ArgumentNullException(nameof(colorPicker),
                           "ColorPicker must not be null.");
        _handleRectTransform = colorPickerHandle.GetComponent<RectTransform>();
        _isInitialized = _handleRectTransform != null;

        return _isInitialized;
    }

    private void Update()
    {
        if (!_isInitialized || !Input.GetMouseButton(0)) return;

        var inputPos = Input.mousePosition;

        PickColor(inputPos);
    }

    private void PickColor(Vector2 inputPos)
    {
        _currentColor = _colorPicker.PickColor(inputPos);
        UpdateHandlePosition(inputPos);
    }

    private void UpdateHandlePosition(Vector2 newPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_handleRectTransform, newPosition, null,
            out var localPos);
        _handleRectTransform.localPosition = localPos;
    }

    public void ResetColorPicker()
    {
        Vector2 defaultPosition = Vector2.zero;
        _currentColor = Color.white;
        UpdateHandlePosition(defaultPosition);
    }
}