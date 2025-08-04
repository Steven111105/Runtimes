using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Cursor Settings")]
    [SerializeField] private bool useCustomHoverCursor = false;
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
        
        // Use custom cursor if specified, otherwise let CursorManager handle it automatically
        if (CursorManager.instance != null && useCustomHoverCursor && customHoverCursor != null)
        {
            CursorManager.instance.SetCustomCursor(customHoverCursor, customHoverHotspot);
        }
        // Note: If not using custom cursor, CursorManager's Update() will handle it automatically
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        // Note: CursorManager's Update() will automatically detect we're no longer over this object
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
        
        // Let CursorManager's automatic system handle the cursor state
        // It will detect if we're still hovering and set appropriate cursor
    }
    
    private void OnDisable()
    {
        // Reset state when object is disabled
        isHovering = false;
        isClicking = false;
        // CursorManager's automatic system will detect the change
    }
}
