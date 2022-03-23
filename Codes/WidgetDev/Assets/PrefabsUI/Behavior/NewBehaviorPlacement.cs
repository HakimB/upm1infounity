using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviorPlacement : MonoBehaviour, IPointerClickHandler
{

    public GameObject tocreate;

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Click " + data);
        Vector2 localPoint = new Vector2(data.position.x, data.position.y);
        Vector3 globalPoint = new Vector3();

        // bool isinside = RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), data.position, data.pressEventCamera, out localPoint);

        //isinside = RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), data.position, data.pressEventCamera, out globalPoint);
        // (Instantiate(tocreate, localPoint, Quaternion.identity));

        Camera cam = Camera.main;

        localPoint = cam.ScreenToWorldPoint(localPoint);

        Debug.Log("Local point: " + localPoint);

        GameObject obj = Instantiate(tocreate, localPoint, Quaternion.identity, gameObject.transform);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
