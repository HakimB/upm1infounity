using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComplexSliderUpdaterScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public Slider _slider;
    public InputField _inputField;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start script looking for sub-component of UI");
        _slider = gameObject.GetComponentInChildren<Slider>();
        _inputField = gameObject.GetComponentInChildren<InputField>();

        Debug.Log("Slider found: " + _slider  + " name: " + _slider.name);
        Debug.Log("Field found: " + _inputField + "name: " + _inputField.name);

        _slider.onValueChanged.AddListener(UpdateValueFromFloat);
        _inputField.onEndEdit.AddListener(UpdateValueFromString);

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateValueFromFloat(float value)
    {
        Debug.Log("float value changed: " + value);
        if (_inputField) { _inputField.text = value.ToString(); }
    }

    public void UpdateValueFromString(string value)
    {
        Debug.Log("string value changed: " + value);
        try
        {
            float ff = float.Parse(value);
            if (_slider && _slider.value != ff) { _slider.value = ff; }
        }
        catch(System.Exception e) {
            Debug.Log("error: " + e);
        }
    }

    public void toto()
    {
        Debug.Log("TOTO TOTO");
    }

    public void OnMouseOver()
    {
        Debug.Log("MOUSE OVER");
    }
    //---------------------------------------------------------------------------------------------------------

    private Vector2 previousPointerPosition;
    private Vector2 currentPointerPosition;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown " + data);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out previousPointerPosition);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp " + eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log("OnDrag " + data);
        
        if (rectTransform == null)
            return;

        Vector2 sizeDelta = rectTransform.sizeDelta;
        Debug.Log("Current Size: " + sizeDelta);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
        Vector2 resizeValue = currentPointerPosition - previousPointerPosition;

        Debug.Log("resize: " + resizeValue);

        sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
        /*sizeDelta = new Vector2(
            Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
            );*/

        rectTransform.sizeDelta = sizeDelta;

        Debug.Log("resize: " + rectTransform);

        previousPointerPosition = currentPointerPosition;
    }
}
