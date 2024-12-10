// Inspire de
// https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.Toggle-onValueChanged.html
//Attach this script to a Toggle GameObject. To do this, go to Create>UI>Toggle.
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;
public class PanelTerreScript : MonoBehaviour
{
	Toggle m_Toggle;
	Slider m_Slider;
	GameObject terre;
	void Start()
	{
		// m_Toggle est le Toggle enfant de PanelTerre
		m_Toggle = GetComponentInChildren<Toggle>();		
		if (m_Toggle == null)
		Debug.Log("m_Toggle Terre = nul");
		
		// m_Slider est le Slider enfant de PanelTerre
		m_Slider = GetComponentInChildren<Slider>();
		if (m_Slider == null)
		Debug.Log("m_Slider terre = nul");

		//Add listener for when the state of the Toggle changes, to take action
		m_Toggle.onValueChanged.AddListener(delegate {
			ToggleValueChanged(m_Toggle);
		});
		m_Slider.onValueChanged.AddListener(delegate {
			SliderValueChanged(m_Slider);
		});
		terre = GameObject.Find("Terre");
	}

	// "JeTourne" est le nom du composant de type script déjà associé à la Terre
	void ToggleValueChanged(Toggle change)
	{
		terre.GetComponent<JeTourne>().mustTurn =
		 !terre.GetComponent<JeTourne>().mustTurn;
	}
	
	void SliderValueChanged(Slider change)
	{
		terre.GetComponent<JeTourne>().rotationSpeed = change.value;
	}
}	
