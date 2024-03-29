using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeTourne : MonoBehaviour
{
    // Par introspection, l'attribut mustTurn est ajouté sous forme de Toggle dans les propriétés du script de l'Inspector, sous le nom "Must Turn"
    // Modifier cette valeur dans l'Inspector modifie cet attribut dans ce code
    public bool mustTurn = true;
    
    // L'attribut rotationSpeed est également ajouté dans les propriétés du script, sous le nom "Rotation Speed"
    // Modifier cette valeur dans l'Inspector modifie cet attribut dans ce code
    public double rotationSpeed = 5.0;

	

    // Start is called before the first frame update
    void Start()
    {
////        Debug.Log("Start() de " + gameObject.name);
        mustTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
////        Debug.Log("Update() de " + gameObject.name);
    }
 
    // FixedUpdate is called at fixed frames. To use with rigid Bodies
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
    void FixedUpdate()
    {
////       Debug.Log("FixedUpdate() de " + gameObject.name);
        if (mustTurn)
        {
////            Debug.Log("Rotation de " + gameObject.name);
            // Rotation autour de l'axe Y
 //           transform.Rotate(0, 5 * Time.fixedDeltaTime, 0);
            transform.Rotate(0, ((float)rotationSpeed) * Time.fixedDeltaTime, 0);
        }
    }

    public void toggle()
    {
        Debug.Log("toggle() de " + gameObject.name);
        mustTurn = !mustTurn;
    }

}
