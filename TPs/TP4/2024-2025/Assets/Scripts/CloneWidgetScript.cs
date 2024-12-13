using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Script associe a un Panel
// Clone un widget choisi par l'utilisateur lors d'un clic de souris.
// Ce widget sera integre comme fils du Panel.
// La position du clone est determinee par la position du curseur de la souris 
public class CloneWidgetScript : MonoBehaviour, IPointerClickHandler
{
	private static string TAG = "CW - ";
	private Camera m_Camera;
	public GameObject m_Widget; // Widget selectionne par l'utilisateur
	
	void Start() {
		// https://docs.unity3d.com/ScriptReference/Camera.html
		// The first enabled Camera component that is tagged "MainCamera"
		//  (Read Only).
		m_Camera = Camera.main;
		if (m_Camera == null) {	Debug.Log(TAG + "Camera not found !");	}
		
		Button[] buttons = gameObject.GetComponentsInChildren<Button>();
		if (buttons != null) {
			int i = 0;
			while (i < buttons.Length &&
			(! buttons[i].name.Equals("ButtonToClone"))) { i++;	}
			
			if (i == buttons.Length) {
				Debug.Log(TAG + "Button to clone not found!");
				m_Widget = null;
			}
			else {
				m_Widget = buttons[i].gameObject;
				Debug.Log(TAG + m_Widget.name + " found!");				
			}
		}
	}
	
	public void OnPointerClick(PointerEventData data)
	{
		Debug.Log(TAG + "OnPointerClick " + data);		
		
		if (m_Widget != null) {
			Vector2 localPoint = new Vector2(data.position.x, data.position.y);
			Debug.Log(TAG + "Local point: " + localPoint);
			
			GameObject obj = Instantiate(m_Widget,
			localPoint,
			Quaternion.identity,
			gameObject.transform);
		}
		else {
			Debug.Log(TAG + "Pas de widget selectionne !");
		}
	}
}
