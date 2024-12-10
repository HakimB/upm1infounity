using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Inspire de https://docs.unity3d.com/Manual/MultipleCameras.html
public class PermutationCamerasScript : MonoBehaviour
{
	// Toggle du Panel associe a ce script
	Toggle m_Toggle;
	
	// Cameras manipulees
	Camera mainCamera;
	Camera moonCamera;

	// Start is called before the first frame update
	void Start()
	{
		// m_Toggle est le Toggle enfant de PabelCameras
		m_Toggle = GetComponentInChildren<Toggle>();

		if (m_Toggle == null)
		  Debug.Log("m_Toggle PanelCameras = nul");

		//Add listener for when the state of the Toggle changes, to take action
		m_Toggle.onValueChanged.AddListener(delegate {
			ToggleValueChanged(m_Toggle);
		});
		mainCamera = getCameraFromParentName("Main Camera");
		mainCamera.enabled = true;
		
		moonCamera = getCameraFromParentName("Camera Lune");
		moonCamera.enabled = false;
	}
	
	private Camera getCameraFromParentName(System.String parentName) {
		GameObject cameraObject = GameObject.Find(parentName);
		if (cameraObject == null)
		  Debug.Log("CameraObject of " + parentName + "= nul");

        Camera camera = cameraObject.GetComponent<Camera>();
		if (camera == null)
		  Debug.Log("Camera of " + parentName + "= nul");
		  
		return camera;
	}
	
	void ToggleValueChanged(Toggle change)
	{
		mainCamera.enabled = !mainCamera.enabled;
		Debug.Log("Main Camera is enabled = " + mainCamera.enabled);

		moonCamera.enabled = !moonCamera.enabled;
		Debug.Log("Moon Camera is enabled = " + moonCamera.enabled);
	}
}	
