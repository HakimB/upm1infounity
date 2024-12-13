using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

// Script associe aux GameObject de type "InputFieldText"

public class FormattedInputFieldScript : MonoBehaviour
{
	// Regex validant uniquement une suite de chiffres.
	// Modifiable dans l'Inspector
	public string m_Regex = "^[0-9]+$";
	
	// Composant InputField lie a ce GameObject
	private InputField m_InputField;
	
	// Si InputField vide
	private Color colorEmpty = Color.white;
	
	// Si texte de l'InputField ne valide pas le regex
	private Color colorError = Color.red;
	
	// Si texte de l'InputField valide le regex
	private Color colorValid = Color.green; 
	
	
	// Start is called before the first frame update
	void Start()
	{
		m_InputField = this.GetComponent<InputField>();
		if (m_InputField == null) {
			Debug.Log("[FormattedInputFieldScript] input field = nul");
		}
		
		m_InputField.onValueChanged.AddListener(delegate {
			ValueChangeCheck();
		});
		
	}

	// Attention : cette fonction est definie independamment de la méthode Start()
	// => initialiser  [m_Text] et [m_Image] dans Start() ne sert a rien
	// ==> ils sont consideres comme "null" dans cette fonction.
	// ===> on en fait des variables locales et le probleme est regle.
	private void ValueChangeCheck() {
		string m_Text = m_InputField.text;
		if (m_Text == null) {
			Debug.Log("[FormattedInputFieldScript] text = nul");
		}
		else {
			// Le type "string" ne supporte pas la concatenation à la Java
			// => Debug.Log("m_Text = " + m_Text); provoque des erreurs
			Debug.Log(string.Concat("m_Text = ", m_Text));
			
			Image m_Image = this.GetComponent<Image>();
			if (m_Image == null) {
				Debug.Log("[FormattedInputFieldScript] image = nul");
			}
			
			if (string.IsNullOrEmpty(m_Text)) {
				m_Image.color = this.colorEmpty;
			}
			else {
				Debug.Log(string.Concat("m_Regex = ", m_Regex));
				
				// Version fonctionnelle numero 1
				/*
				if (Regex.IsMatch(m_Text, m_Regex)) {
					m_Image.color = colorValid;
				}
				else {
					m_Image.color = colorError;
				}
				*/
				
				// Version fonctionnelle numero 2
// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
// https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.match?view=net-8.0
				Regex myRegExp = new Regex(m_Regex);
				Match myMatch = myRegExp.Match(m_Text);
				if (myMatch.Success) {
					m_Image.color = colorValid;
				}
				else {
					m_Image.color = colorError;
				}
			}
		}
	}
}	