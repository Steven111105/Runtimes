using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Cursor Settings")]
    [SerializeField] private bool useCustomHoverCursor = true;
    [SerializeField] private bool useCustomClickCursor = false;
    [SerializeField] private Texture2D customHoverCursor;
    [SerializeField] private Texture2D customClickCursor;
    [SerializeField] private Vector2 customHoverHotspot = Vector2.zero;
    [SerializeField] private Vector2 customClickHotspot = Vector2.zero;
    
    private bool isHovering = false;
    private bool isClicking = false;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        
        if (CursorManager.instance != null)
        {
            if (useCustomHoverCursor && customHoverCursor != null)
            {
                CursorManager.instance.SetCustomCursor(customHoverCursor, customHoverHotspot);
            }
            else
            {
                CursorManager.instance.SetHoverCursor();
            }
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        
        if (!isClicking && CursorManager.instance != null)
        {
            CursorManager.instance.OnHoverExit();
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isClicking = true;
        
        if (CursorManager.instance != null)
        {
            if (useCustomClickCursor && customClickCursor != null)
            {
                CursorManager.instance.SetCustomCursor(customClickCursor, customClickHotspot);
            }
            else
            {
                CursorManager.instance.SetClickCursor();
            }
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isClicking = false;
        
        if (CursorManager.instance != null)
        {
            if (isHovering)
            {
                // Still hovering, go back to hover cursor
                if (useCustomHoverCursor && customHoverCursor != null)
                {
                    CursorManager.instance.SetCustomCursor(customHoverCursor, customHoverHotspot);
                }
                else
                {
                    CursorManager.instance.SetHoverCursor();
                }
            }
            else
            {
                // Not hovering anymore, go back to default
                CursorManager.instance.OnHoverExit();
            }
        }
    }
    
    private void OnDisable()
    {
        // Reset cursor when object is disabled
        if (isHovering && CursorManager.instance != null)
        {
            CursorManager.instance.OnHoverExit();
        }
        isHovering = false;
        isClicking = false;
    }
}
