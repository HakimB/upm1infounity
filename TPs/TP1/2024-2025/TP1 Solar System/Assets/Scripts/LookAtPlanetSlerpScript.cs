// Script attache aux GameObject "Toggle" enfants de ToggleGroupLookAt pour
//  centrer la vue de la camera principale sur une planete donnee, 
// ou revenir a l'orientation originale.

// Attention : chaque Toggle possede sa propre instance de ce script.
// Donc toutes les variables du script sont definies independamment pour chaque Toggle
// (elles ne sont pas partagees)

// Inspire de
// https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.Toggle-onValueChanged.html
// Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
public class LookAtPlanetSlerpScript : MonoBehaviour
{
  Toggle m_Toggle;
	
  GameObject m_MainCamera;
	
  float m_TimeCount = 0.0f;
	
  bool m_EnableInterpolation = false;
	
  ToggleGroup m_ToggleGroup;
	
  Quaternion m_MainCameraInitialRotation;
	
  public ToggleGroupLookAtScript m_ToggleGroupLookAtScript;
	
  void Start() {
    Debug.Log("[LookAtPlanetSlerpScript] [1]");

    // On recupere le script identifiant la planete vers laquelle deplacer la camera
    m_ToggleGroupLookAtScript =
      GameObject.FindObjectOfType(typeof(ToggleGroupLookAtScript))
    	as ToggleGroupLookAtScript;
		
    Debug.Log("[LookAtPlanetSlerpScript] [2]");
				
    // ToggleGroup parent
    m_ToggleGroup = GetComponentInParent<ToggleGroup>();
    if (m_ToggleGroup == null) {
    	Debug.Log("[LookAtPlanetSlerpScript] m_ToggleGroup = nul");
    }
    else {
      Debug.Log("[LookAtPlanetSlerpScript] m_ToggleGroup = " + m_ToggleGroup.name);
    }
		
    // m_Toggle est le Toggle "enfant" de ce GameObject (en fait le Toggle actuel)
    m_Toggle = GetComponentInChildren<Toggle>();

    if (m_Toggle == null)
       Debug.Log("[LookAtPlanetSlerpScript] m_Toggle = nul");
			
// Add listener for when the state of the Toggle changes, to take action
	m_Toggle.onValueChanged.AddListener(delegate {
		ToggleValueChanged(m_Toggle);
	});
		
	// On recherche la [Main Camera] pour la position originale
	m_MainCamera = GameObject.Find("Main Camera");
		
	// Enregistrement de la rotation de la camera originale
	m_MainCameraInitialRotation = m_MainCamera.transform.rotation;
	if (m_MainCamera == null) {
		Debug.Log("[LookAtPlanetSlerpScript] m_MainCamera = nul");
	}
}
	
  void ToggleValueChanged(Toggle change) {
	if (m_Toggle.isOn) {
		Debug.Log("[LookAtPlanetSlerpScript] m_Toggle = " + m_Toggle.isOn + 
		" pour " + name);
		m_EnableInterpolation = true;
	}
  }
	
  void FixedUpdate() {
    Toggle selectedToggle = m_ToggleGroupLookAtScript.GetCurrentSelection();
    Toggle previousToggle = m_ToggleGroupLookAtScript.GetPreviousSelection();
		
    if (m_Toggle.isOn) { // Ce Toggle est active
			
      // Recuperation de la planete associee
      GameObject selectedObject =	  
        m_ToggleGroupLookAtScript.GetGameObjectFromToggle(selectedToggle);
			
      if (m_EnableInterpolation) {
      	if (selectedToggle != previousToggle) {
      	  if (selectedObject != m_MainCamera) {
      	    GameObject previousObject = 		   	  
      	      m_ToggleGroupLookAtScript.GetGameObjectFromToggle(previousToggle);
						
            if (selectedObject == null || previousObject == null) {
              Debug.Log("[LookAtPlanetSlerpScript][Update] selectedObject = nul ou " 
                + "previousObject = nul");
            }
						
// https://docs.unity3d.com/2022.2/Documentation/ScriptReference/Quaternion.LookRotation.html
            Vector3 relativePos = selectedObject.transform.position - 
              m_MainCamera.transform.position;
						
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
						
// https://docs.unity3d.com/2022.2/Documentation/ScriptReference/Quaternion.Slerp.html
// [m_timeCount] varie entre 0 (axe de rotation initial) et 1 (axe de rotation final)
// Sa valeur est mise a jour a chaque frame.
            m_MainCamera.transform.rotation =         
              Quaternion.Slerp(m_MainCamera.transform.rotation,
                               rotation,
                               m_TimeCount);											
          }
					
          else { // selectedObject == m_MainCamera
              GameObject previousObject = 
                m_ToggleGroupLookAtScript.GetGameObjectFromToggle(previousToggle);
	
              if (selectedObject == null || previousObject == null) {
                Debug.Log("[LookAtPlanetSlerpScript][Update] selectedObject = nul ou "
                 + "previousObject = nul");
              }
	
              m_MainCamera.transform.rotation = 
              Quaternion.Slerp(m_MainCamera.transform.rotation,
                               m_MainCameraInitialRotation,
                               m_TimeCount);
          }
            UpdateTimeCount();
        }
      }
      else if (m_MainCamera != selectedObject) {
        m_MainCamera.transform.LookAt(selectedObject.transform);
      }
    }
  }

  void UpdateTimeCount() {
    Debug.Log("[LookAtPlanetSlerpScript][Update] rotation = " + 
      m_MainCamera.transform.rotation);
    m_TimeCount += Time.deltaTime;

    // [m_TimeCount] doit rester dans l'intervalle  [0; 1].
    if (m_TimeCount > 1.0f) {
      m_TimeCount = 0.0f;

      // On est arrive sur l'axe de rotation final : l'interpolation stoppe
      m_EnableInterpolation = false;
    }
  }
}
		
