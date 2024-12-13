using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpinnerPannelScript : MonoBehaviour
/*, IPointerDownHandler, IPointerUpHandler, IDragHandler*/
{
	private Button m_ButtonUp, m_ButtonDown;
	private InputField m_InputField;
	
	// Increment laisse au choix de l'utilisateur
	public int m_IncrementStep = 1; 
	
	// Start is called before the first frame update
	void Start()
	{
		m_InputField = gameObject.GetComponentInChildren<InputField>();
		if (m_InputField != null) {
			Debug.Log("Input Field found: " + m_InputField + "name: " +
			m_InputField.name);
		}
		
		Button[] buttons = gameObject.GetComponentsInChildren<Button>();
		if (buttons != null) {
			if (buttons[0].name.Equals("ButtonUp") &&
			buttons[1].name.Equals("ButtonDown")) {
				m_ButtonUp = buttons[0]; 
				m_ButtonDown = buttons[1];
			}
			else if (buttons[1].name.Equals("ButtonUp") &&
			buttons[0].name.Equals("ButtonDown")) {
				m_ButtonUp = buttons[1];
				m_ButtonDown = buttons[0];
			}
			else {
				Debug.Log("Buttons Up and Down not found!");
			}
		}
		else {
			Debug.Log("Buttons not found!");
		}
		
		if (m_ButtonUp != null && m_ButtonDown != null) {
			m_ButtonUp.onClick.AddListener(delegate {
				IncrementField(true);
			});
			m_ButtonDown.onClick.AddListener(delegate {
				IncrementField(false);
			});
		}
	}

	
    // Update is called once per frame
	void Update() { }

	private void IncrementField(bool increment) {
		string m_Text = m_InputField.text;
		try {
			int value = Int32.Parse(m_Text);
			Debug.Log("value = " + value);
			if (increment) {
				value += m_IncrementStep;
			}
			else {
				value -= m_IncrementStep;
			}
			Debug.Log("new value = " + value);
			m_InputField.text = value.ToString();
		}
		catch (FormatException e) {
			Debug.Log(e.Message);
		}
	}
}
