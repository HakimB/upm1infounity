// Script attaché aux GameObject "PanelSoleil" pour gérer l'animation du soleil


// Inspiré de 
// https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.Toggle-onValueChanged.html
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class PanelSoleilScript : MonoBehaviour
{
    Toggle m_Toggle;
    Slider m_Slider;
    GameObject soleil;
    
    void Start()
    {
        // m_Toggle est le Toggle enfant de PanelSoleil
       m_Toggle = GetComponentInChildren<Toggle>();
        if (m_Toggle == null)
            Debug.Log("m_Toggle Soleil = nul");
        
        // m_Slider est le Slider enfant de PanelSoleil
       m_Slider = GetComponentInChildren<Slider>();
        if (m_Slider == null)
            Debug.Log("m_Slider soleil = nul");

        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
        
        m_Slider.onValueChanged.AddListener(delegate {
            SliderValueChanged(m_Slider);
        });        

        soleil = GameObject.Find("Soleil"); 
    }

    void ToggleValueChanged(Toggle change)
    {
      soleil.GetComponent<JeTourne>().mustTurn = !soleil.GetComponent<JeTourne>().mustTurn;
    }

    void SliderValueChanged(Slider change)
    {
      soleil.GetComponent<JeTourne>().rotationSpeed = change.value;
    }

}

