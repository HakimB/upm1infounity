// Script attaché aux GameObject "PanelMars" pour gérer l'animation de Mars


// Inspiré de 
// https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.Toggle-onValueChanged.html
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class PanelMarsScript : MonoBehaviour
{
    Toggle m_Toggle;
    Slider m_Slider;
    GameObject mars;
    
    void Start()
    {
        // m_Toggle est le Toggle enfant de PanelMars
       m_Toggle = GetComponentInChildren<Toggle>();
        if (m_Toggle == null)
            Debug.Log("m_Toggle Mars = nul");
        
        // m_Slider est le Slider enfant de PanelMars
       m_Slider = GetComponentInChildren<Slider>();
        if (m_Slider == null)
            Debug.Log("m_Slider Mars = nul");

        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
        
        m_Slider.onValueChanged.AddListener(delegate {
            SliderValueChanged(m_Slider);
        });        

        mars = GameObject.Find("Mars"); 
    }

    void ToggleValueChanged(Toggle change)
    {
      mars.GetComponent<JeTourne>().mustTurn = !mars.GetComponent<JeTourne>().mustTurn;
    }

    void SliderValueChanged(Slider change)
    {
      mars.GetComponent<JeTourne>().rotationSpeed = change.value;
    }
}

