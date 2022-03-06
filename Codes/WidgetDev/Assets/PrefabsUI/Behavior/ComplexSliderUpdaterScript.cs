using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexSliderUpdaterScript : MonoBehaviour
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
}
