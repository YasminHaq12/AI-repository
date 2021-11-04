using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public enum Item
    {
        ITEM1,
       ITEM2,
       ITEM3,
    }

    bool correctplace1;
    bool correctplace2;
    bool correctplace3;
    public GameObject Item1;
    public Item type;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
           
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item1")
        {
            if(type == Item.ITEM1)
            {
                correctplace1 = true;
                Debug.Log("correct");

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "item1")
        {
            if (type == Item.ITEM1)
            {
                correctplace1 = false;
                Debug.Log("See Ya");

            }
        }
    }
}
/*Enum each is assigned a different state and then if x state and if gameobject dropped, then assign a new state. */