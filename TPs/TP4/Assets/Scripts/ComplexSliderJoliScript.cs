using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComplexSliderJoliScript : MonoBehaviour
{	
	public Slider m_Slider;
	public InputField m_InputField;
	
	// Start is called before the first frame update
	void Start()
	{
		m_Slider = gameObject.GetComponentInChildren<Slider>();
		m_InputField = gameObject.GetComponentInChildren<InputField>();
		
		Debug.Log("Slider found: " + m_Slider  + " name: " + m_Slider.name);
		Debug.Log("Field found: " + m_InputField + "name: " + m_InputField.name);
		
		m_Slider.onValueChanged.AddListener(UpdateValueFromFloat);
		m_InputField.onEndEdit.AddListener(UpdateValueFromString);
		
	}
	
	// Update is called once per frame
	void Update() { }
	
	public void UpdateValueFromFloat(float value)
	{
		Debug.Log("float value changed: " + value);
		if (m_InputField) { m_InputField.text = value.ToString(); }
	}
	
	public void UpdateValueFromString(string value)
	{
		Debug.Log("string value changed: " + value);
		try
		{
			float ff = float.Parse(value);
			if (m_Slider && m_Slider.value != ff) { m_Slider.value = ff; }
		}
		catch(System.Exception e) {
			Debug.Log("error: " + e);
		}
	}	
}
