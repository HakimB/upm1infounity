using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FormattedInputFieldScript : MonoBehaviour
{

    public InputField field;
    public string regex;

    public Color colorEmpty = Color.white;
    public Color colorError = Color.red;
    public Color colorValid = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        Image image = gameObject.GetComponent<Image>();

        Image[] images = gameObject.GetComponents<Image>();
        field = GetComponent<InputField>();
        // field.onValidateInput += delegate (string text, int charIndex, char addedchar) { return MyValidateChar(text, charIndex, addedchar); };
    }



    private char MyValidateChar(string text, int charIndex, char addedchar)
    {
        return addedchar;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextChanged(string text)
    {
        Debug.Log(text);
        if (string.IsNullOrEmpty(text))
            field.GetComponent<Image>().color = colorEmpty;
        else
        {
            if (Regex.IsMatch(text, regex))
                field.GetComponent<Image>().color = colorValid;
            else
                field.GetComponent<Image>().color = colorError;
        }
    }
}
