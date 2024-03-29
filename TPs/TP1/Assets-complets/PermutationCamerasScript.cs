using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inspiré de https://docs.unity3d.com/Manual/MultipleCameras.html

public class PermutationCamerasScript : MonoBehaviour
{
    // Toggle du Panel associé à ce script
    Toggle m_Toggle;
    
    // Caméras manipulées
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
        
        mainCamera = getCameraFromParentName("MainCamera");
        mainCamera.enabled = true;
        
        moonCamera = getCameraFromParentName("CameraLune");
        moonCamera.enabled = false;
    }

    private Camera getCameraFromParentName(System.String parentName) {
        GameObject cameraObject = GameObject.Find(parentName);
        if (cameraObject == null) 
            Debug.Log("CameraObject of " + parentName +  "= nul");        
        
        Camera camera = cameraObject.GetComponent<Camera>();
        if (camera == null) 
            Debug.Log("Camera of " + parentName +  "= nul");        
        
        return camera;
    }

    void ToggleValueChanged(Toggle change)
    {
      mainCamera.enabled = !mainCamera.enabled;
      moonCamera.enabled = !moonCamera.enabled;
    }

}
