using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LatSpinnerScript : MonoBehaviour
{
    [SerializeField]
    private int min = -10;

    [SerializeField]
    private int max = 100;

    public int value = 0;

    public Color colorOK = Color.green;
    public Color colorKO = Color.red;

    private InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponentInChildren<InputField>();
        inputField.text = value.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateValue();
    }


    public void updateValue()
    {   
        Image img = GetComponentInChildren<Image>();

        Debug.Log(" >> " + (inputField==null));
        
        if(min <= value && value <= max)
            inputField.image.color = colorOK;
        else
            inputField.image.color = colorKO;
        inputField.text = value.ToString();
        
        Debug.Log(" > " + value +" ====>>> " + inputField);

    }

    public void rightValue()
    {
        value++;
        Debug.Log(" > value plus!!");
    }

    public void leftValue()
    {
        value--;
        Debug.Log(" > value moins!!");
    }
}
