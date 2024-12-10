// Script attaché aux GameObject "Toggle" de PanelLookAt pour centrer la vue de la caméra principale sur une planète donnée, ou revenir à l'orientation originale. Utilisation d'une interpolation sphérique.

// Attention : chaque Toggle possède sa propre instance de ce script.
// Donc toutes les variables du script sont définies indépendamment pour chaque Toggle (elles ne sont pas partagées


// Inspiré de 
// https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.Toggle-onValueChanged.html
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class LookAtPlanetScriptSlerp : MonoBehaviour
{
    Toggle m_Toggle;
		GameObject m_MainCamera;
		float m_TimeCount = 0.0f;
		bool m_EnableInterpolation = false;
		ToggleGroup m_ToggleGroup;
		Quaternion m_MainCameraInitialRotation;

		public ToggleGroupLookAtScript m_ToggleGroupLookAtScript;
		
    void Start()
    {
				m_ToggleGroupLookAtScript = GameObject.FindObjectOfType(typeof(ToggleGroupLookAtScript)) as ToggleGroupLookAtScript;
				
				// ToggleGroup parent
				m_ToggleGroup = GetComponentInParent<ToggleGroup>();
				if (m_ToggleGroup == null) {
						Debug.Log("[LookAtPlanetScriptSlerp] m_ToggleGroup = nul");
				}
				else {
						Debug.Log("[LookAtPlanetScriptSlerp] m_ToggleGroup = " + m_ToggleGroup.name);
				}
				
        // m_Toggle est le Toggle enfant de ce GameObject
       m_Toggle = GetComponentInChildren<Toggle>();
        if (m_Toggle == null)
            Debug.Log("[LookAtPlanetScriptSlerp] m_Toggle = nul");
        
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

				m_MainCamera = GameObject.Find("MainCamera");
				m_MainCameraInitialRotation = m_MainCamera.transform.rotation;
				if (m_MainCamera == null) {
            Debug.Log("[LookAtPlanetScriptSlerp] m_MainCamera = nul");
				}
					
    }

    void ToggleValueChanged(Toggle change)
    {
				if (m_Toggle.isOn) {
						Debug.Log("[LookAtPlanetScriptSlerp] m_Toggle = " + m_Toggle.isOn + " pour " + name);
						m_EnableInterpolation = true;
				}
    }

		// G
		void FixedUpdate() {
				Toggle selectedToggle = m_ToggleGroupLookAtScript.GetCurrentSelection();
				Toggle previousToggle = m_ToggleGroupLookAtScript.GetPreviousSelection();
				
				if (m_Toggle.isOn) {
						GameObject selectedObject = m_ToggleGroupLookAtScript.GetGameObjectFromToggle(selectedToggle);
						if (m_EnableInterpolation) {
								if (selectedToggle != previousToggle) {

										if (selectedObject != m_MainCamera) {
												GameObject previousObject = m_ToggleGroupLookAtScript.GetGameObjectFromToggle(previousToggle);
												if (selectedObject == null || previousObject == null) {
														Debug.Log("[LookAtPlanetScriptSlerp][Update] selectedObject = nul ou previousObject = nul");
												}
												
												m_MainCamera.transform.rotation =
														Quaternion.Slerp(m_MainCamera.transform.rotation,
																						 Quaternion.LookRotation(selectedObject.transform.position -
																																		 m_MainCamera.transform.position),
																						 m_TimeCount);
												//										Debug.Log("[LookAtPlanetScriptSlerp][Update] rotation = " + m_MainCamera.transform.rotation);
												m_TimeCount += Time.deltaTime;
												// Debug.Log("[LookAtPlanetScriptSlerp][Update] m_GameObject = " + m_GameObject);
												if (m_TimeCount > 1.0f) {
														m_TimeCount = 0.0f;
														m_EnableInterpolation = false;
												}
										}
										else { // selectedObject == m_MainCamera
												GameObject previousObject = m_ToggleGroupLookAtScript.GetGameObjectFromToggle(previousToggle);
												if (selectedObject == null || previousObject == null) {
														Debug.Log("[LookAtPlanetScriptSlerp][Update] selectedObject = nul ou previousObject = nul");
												}
												
												m_MainCamera.transform.rotation = Quaternion.Slerp(m_MainCamera.transform.rotation,
																																					 m_MainCameraInitialRotation,
																																					 m_TimeCount);
												//										Debug.Log("[LookAtPlanetScriptSlerp][Update] rotation = " + m_MainCamera.transform.rotation);
												m_TimeCount += Time.deltaTime;
												// Debug.Log("[LookAtPlanetScriptSlerp][Update] m_GameObject = " + m_GameObject);
												if (m_TimeCount > 1.0f) {
														m_TimeCount = 0.0f;
														m_EnableInterpolation = false;
												}
												
										}
								}
						}
						else if (m_MainCamera != selectedObject) {
								m_MainCamera.transform.LookAt(selectedObject.transform);
						}
				}
		}
}

