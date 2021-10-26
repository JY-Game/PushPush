using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Block : MonoBehaviour,IDropHandler
{
    public string item;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;
            Debug.Log(gameObject.name + eventData.pointerDrag.gameObject.name);
            item = eventData.pointerDrag.gameObject.GetComponentInChildren<Text>().text;
            eventData.pointerDrag.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x,GetComponent<RectTransform>().sizeDelta.y);
                //GetComponent<RectTransform>().sizeDelta;
        }
    }
}
