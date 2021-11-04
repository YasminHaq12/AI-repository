using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent < CanvasGroup>();
    }
   public void OnBeginDrag(PointerEventData eventdata)
    {
        Debug.Log("yes");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;


    }

    public void OnDrag(PointerEventData eventdata)
    {
        rectTransform.anchoredPosition += eventdata.delta/ canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventdata)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

    }


    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
  }
