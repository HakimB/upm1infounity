using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeTourne : MonoBehaviour
{

    public bool mustTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        mustTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (mustTurn)
        {
            Debug.Log("Rotation de " + gameObject.name);
            transform.Rotate(0, 5 * Time.fixedDeltaTime, 0);
        }
    }


    public void toggle()
    {
        mustTurn = !mustTurn;
    }

}
