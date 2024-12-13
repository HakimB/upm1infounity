using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Affiche les coordonnees de la souris à chaque clic
public class PanelMouseScript : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData data)
	{
		Debug.Log("OnPointerClick " + data);
	}	
}