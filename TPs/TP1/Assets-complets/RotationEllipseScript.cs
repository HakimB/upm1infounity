using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEllipseScript : MonoBehaviour
{
	private GameObject soleil;
	public float rotationSpeed = 10;
	
    // Start is called before the first frame update
    void Start()
    {
    	soleil = GameObject.Find("Soleil"); 
    	if (soleil == null) {
    		Debug.Log("RotationAllipseScript: soleil = null");
    	}
    }

    // Update is called once per frame
    void Update()
    {
    }
 
    // FixedUpdate is called at fixed frames. To use with rigid Bodies
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
    void FixedUpdate()
    {
    	// https://docs.unity3d.com/ScriptReference/Transform.RotateAround.html
        transform.RotateAround(soleil.transform.position, Vector3.up, ((float)rotationSpeed) * Time.fixedDeltaTime);
    }
}
