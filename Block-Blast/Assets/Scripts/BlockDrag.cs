using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool allSnapped = true;

        // Check if all block pieces are snapped
        foreach (Transform piece in transform)
        {
            BlockSnap snapScript = piece.GetComponent<BlockSnap>();
            if (snapScript != null && !snapScript.IsSnapped())
            {
                allSnapped = false;
                break;
            }
        }

        if (allSnapped)
        {
            foreach (Transform piece in transform)
            {
                BlockSnap snapScript = piece.GetComponent<BlockSnap>();
                if (snapScript != null)
                {
                    snapScript.SnapToCell();
                }
            }
            Debug.Log("Block successfully snapped!");
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
            Debug.Log("Block failed to snap, returning to original position.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
