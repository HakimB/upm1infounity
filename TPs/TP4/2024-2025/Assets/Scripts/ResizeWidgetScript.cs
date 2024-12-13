using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Redimensionne le Widget associe a ce script, selon le "drag" de souris
// Les directions "Droite" et "Bas" permettent de grossir le widget
public class ResizeWidgetScript : MonoBehaviour, IPointerDownHandler,
   IPointerUpHandler, IDragHandler
{
	private Vector2 previousPointerPosition;
	private Vector2 currentPointerPosition;
	private RectTransform rectTransform;
	private static string TAG = "RW - ";
	
	// Use Awake to initialize variables or states before the application starts.	
	private void Awake() { rectTransform = GetComponent<RectTransform>(); }
	
	public void OnPointerDown(PointerEventData data) {
		Debug.Log(TAG + "OnPointerDown " + data);
	}
	
	public void OnPointerUp(PointerEventData eventData) {
		Debug.Log(TAG + "OnPointerUp " + eventData);
	}
	
	public void OnDrag(PointerEventData data) {
		Debug.Log(TAG + "OnDrag " + data);
		rectTransform = GetComponent<RectTransform>();
		
		if (rectTransform == null) {
			Debug.Log(TAG + "RectTransform not found !");
			return;
		}
		
// https://docs.unity3d.com/ScriptReference/RectTransform-sizeDelta.html
		Vector2 sizeDelta = rectTransform.sizeDelta;
		Debug.Log(TAG + "Current Size: " + sizeDelta);
		
// https://docs.unity3d.com/ScriptReference/RectTransformUtility.ScreenPointToLocalPointInRectangle.html
		RectTransformUtility.
		ScreenPointToLocalPointInRectangle(rectTransform,
			data.position,
			data.pressEventCamera,
			out currentPointerPosition);
		Vector2 resizeValue = currentPointerPosition - previousPointerPosition;
		
		Debug.Log(TAG + "Resize: " + resizeValue);
		
		sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
		
		rectTransform.sizeDelta = sizeDelta;
		Debug.Log(TAG + "New size: " + rectTransform);
		
		previousPointerPosition = currentPointerPosition;
	}
}	