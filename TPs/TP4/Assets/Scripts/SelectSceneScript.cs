using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Charge une des scenes du projet.
// Ce script est associe a tous les boutons (un par scene)
// permettant de selectionner une autre scene

// ATTENTION : un GameObject "EventSystem" doit etre place dans CHAQUE scene
// pour que les iteractions soient prises en compte.

// Source : https://www.youtube.com/watch?v=PpIkrff7bKU
public class SelectSceneScript : MonoBehaviour
{
	private Button m_Button;
	
	// Start is called before the first frame update
	void Start()
	{
		m_Button = gameObject.GetComponent<Button>();
		if (m_Button == null) {
			Debug.Log("m_Button = null");
		}
		else {
			Debug.Log("m_Button " + m_Button.name + " found.");
		}
		m_Button.onClick.AddListener(delegate {
			SetOtherScene();
		});
	}
	
	// Update is called once per frame
	void Update() {	}
	
	private void SetOtherScene() {
		Debug.Log("Enter SetOtherScene");
		if (m_Button.name.ToString().Equals("ButtonSwitchToMouseScene")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			Debug.Log("Selected button : ButtonSwitchToMouseScene");
		}
		else if (m_Button.name.ToString().Equals("ButtonSwitchToSampleScene")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
			Debug.Log("Selected button : ButtonSwitchToSmapleScene");
		}
		else {
			Debug.Log("Error SetOtherScene");
		}
	}
}

