using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PitcherScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Drag Settings")]
    [SerializeField] private Image draggableImage; // The image that will be dragged
    [SerializeField] private Canvas canvas; // Reference to the canvas for proper positioning
    
    [Header("Drop Target")]
    [SerializeField] private Image targetImage; // The specific image to drop on
    
    private bool isMouseOver = false;
    private bool isDragging = false;
    private Vector3 originalLocalPosition;
    
    void Start()
    {
        // Get references
        if (draggableImage == null)
            draggableImage = GetComponent<Image>();
        
        if (canvas == null)
            canvas = GetComponentInParent<Canvas>();
        
        // Store original local position
        if (draggableImage != null)
            originalLocalPosition = draggableImage.transform.localPosition;
    }

    // Called when mouse enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }
    
    // Called when mouse exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragging) // Don't change hover state while dragging
        {
            isMouseOver = false;
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Only allow dragging if mouse is over this object
        if (!isMouseOver)
        {
            return;
        }
        
        isDragging = true;
        
        // Make the image appear above other UI elements while dragging
        if (draggableImage != null)
        {
            draggableImage.raycastTarget = false;
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        
        // Move the image with the mouse
        if (draggableImage != null && canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out position
            );
            
            draggableImage.transform.position = canvas.transform.TransformPoint(position);
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        
        isDragging = false;
        
        // Re-enable raycast target
        if (draggableImage != null)
        {
            draggableImage.raycastTarget = true;
        }
        
        // Check if we dropped over the target image
        bool droppedOnTarget = IsOverTargetImage(eventData);
        
        if (droppedOnTarget)
        {
            OnDroppedOnTarget();
        }
        
        // Always return to original position
        ReturnToOriginalPosition();
        
        // Reset target image color
        
        // Check if mouse is still over the draggable object after dragging
        isMouseOver = RectTransformUtility.RectangleContainsScreenPoint(
            draggableImage.rectTransform, 
            Input.mousePosition, 
            canvas.worldCamera
        );
    }
    
    private bool IsOverTargetImage(PointerEventData eventData)
    {
        if (targetImage == null) return false;
        
        // Use UI raycasting to check what's under the mouse
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == targetImage.gameObject)
            {
                return true;
            }
        }
        
        return false;
    }

    private void OnDroppedOnTarget()
    {
        targetImage.GetComponent<CupScript>().FillCup(); // Assuming CupScript has a property to mark it as filled
    }
    
    private void ReturnToOriginalPosition()
    {
        if (draggableImage != null)
        {
            draggableImage.transform.localPosition = originalLocalPosition;
        }
    }
    
    // Optional: Method to reset the image position
    public void ResetPosition()
    {
        if (draggableImage != null)
        {
            draggableImage.transform.localPosition = originalLocalPosition;
        }
    }
}
