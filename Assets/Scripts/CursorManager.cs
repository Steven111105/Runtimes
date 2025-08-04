using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Textures")]
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D hoverCursor;
    [SerializeField] private Texture2D clickCursor;
    
    [Header("Cursor Hotspots")]
    [SerializeField] private Vector2 defaultHotspot = Vector2.zero;
    [SerializeField] private Vector2 hoverHotspot = Vector2.zero;
    [SerializeField] private Vector2 clickHotspot = Vector2.zero;
    
    [Header("Debug & Failsafe")]
    [SerializeField] private bool enableDebugLogging = false;
    [SerializeField] private float cursorCheckInterval = 1f;
    
    public static CursorManager instance;
    
    // Use Unity's native hover detection
    private bool isCurrentlyHovering = false;
    private Coroutine cursorWatchdog;
    
    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    private void Start()
    {
        SetDefaultCursor();
        // Start the cursor watchdog system (simplified)
        cursorWatchdog = StartCoroutine(CursorWatchdog());
    }
    
    private void Update()
    {
        // Reset cursor when application loses focus (Alt+Tab fix)
        if (!Application.isFocused)
        {
            ResetCursorOnFocusLoss();
        }
        
        // Use Unity's native hover detection
        CheckUnityHoverState();
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            ResetCursorOnFocusLoss();
        }
    }
    
    private void CheckUnityHoverState()
    {
        // Use Unity's EventSystem to check if mouse is over any UI
        bool shouldBeHovering = IsMouseOverInteractableUI();
        
        // Only change cursor if state actually changed
        if (shouldBeHovering != isCurrentlyHovering)
        {
            isCurrentlyHovering = shouldBeHovering;
            
            if (shouldBeHovering)
            {
                SetHoverCursorInternal();
            }
            else
            {
                SetDefaultCursorInternal();
            }
            
            if (enableDebugLogging)
                Debug.Log($"Cursor state changed to: {(shouldBeHovering ? "Hovering" : "Default")}");
        }
    }
    
    private bool IsMouseOverInteractableUI()
    {
        var eventSystem = EventSystem.current;
        if (eventSystem == null) return false;
        
        var pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        
        var results = new System.Collections.Generic.List<RaycastResult>();
        eventSystem.RaycastAll(pointerEventData, results);
        
        // Check if any result has an InteractableObject component or is a Button
        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<InteractableObject>() != null ||
                result.gameObject.GetComponent<UnityEngine.UI.Button>() != null ||
                result.gameObject.GetComponent<UnityEngine.UI.Selectable>() != null)
            {
                return true;
            }
        }
        
        return false;
    }
    
    // Public methods for manual control (legacy support)
    public void SetDefaultCursor()
    {
        isCurrentlyHovering = false;
        SetDefaultCursorInternal();
    }
    
    public void SetHoverCursor()
    {
        isCurrentlyHovering = true;
        SetHoverCursorInternal();
    }
    
    // Internal methods that actually change the cursor
    private void SetDefaultCursorInternal()
    {
        if (defaultCursor != null)
        {
            try
            {
                Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Failed to set default cursor: {e.Message}. Using system default instead.");
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            // Fallback to system default
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    
    private void SetHoverCursorInternal()
    {
        if (hoverCursor != null)
        {
            try
            {
                Cursor.SetCursor(hoverCursor, hoverHotspot, CursorMode.Auto);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Failed to set hover cursor: {e.Message}. Creating simple cursor instead.");
                CreateSimpleHoverCursor();
            }
        }
        else
        {
            // Create a simple cursor texture if none provided
            if (Application.isPlaying)
            {
                CreateSimpleHoverCursor();
            }
        }
    }
    
    public void SetClickCursor()
    {
        if (clickCursor != null)
        {
            try
            {
                Cursor.SetCursor(clickCursor, clickHotspot, CursorMode.Auto);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Failed to set click cursor: {e.Message}. Using default instead.");
                SetDefaultCursorInternal();
            }
        }
    }
    
    // Method to set custom cursor with parameters
    public void SetCustomCursor(Texture2D cursor, Vector2 hotspot)
    {
        if (cursor != null)
        {
            try
            {
                Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Failed to set custom cursor: {e.Message}. Using default instead.");
                SetDefaultCursorInternal();
            }
        }
    }
    
    private void ResetCursorOnFocusLoss()
    {
        // Reset hover state when losing focus
        isCurrentlyHovering = false;
        
        if (enableDebugLogging)
            Debug.Log("ResetCursorOnFocusLoss called");
        
        SetDefaultCursorInternal();
    }
    
    // Create a simple hover cursor if none is provided
    private void CreateSimpleHoverCursor()
    {
        // Create a simple 32x32 yellow cursor
        Texture2D simpleCursor = new Texture2D(32, 32, TextureFormat.ARGB32, false);
        Color[] pixels = new Color[32 * 32];
        
        // Simple hand-like pattern
        for (int y = 0; y < 32; y++)
        {
            for (int x = 0; x < 32; x++)
            {
                int index = y * 32 + x;
                
                // Create a simple pointer shape
                if ((x >= 8 && x <= 12 && y >= 8 && y <= 24) || // Finger
                    (x >= 6 && x <= 14 && y >= 20 && y <= 24))   // Palm
                {
                    pixels[index] = Color.yellow;
                }
                else
                {
                    pixels[index] = Color.clear;
                }
            }
        }
        
        simpleCursor.SetPixels(pixels);
        simpleCursor.Apply();
        
        hoverCursor = simpleCursor;
        hoverHotspot = new Vector2(10, 8); // Point at finger tip
        
        Cursor.SetCursor(hoverCursor, hoverHotspot, CursorMode.Auto);
        Debug.Log("✅ Created simple hover cursor");
    }
    
    // Simplified watchdog system
    private System.Collections.IEnumerator CursorWatchdog()
    {
        while (true)
        {
            yield return new WaitForSeconds(cursorCheckInterval);
            
            // Skip checks if application doesn't have focus
            if (!Application.isFocused)
                continue;
                
            // Force a cursor state check (in case Update missed something)
            CheckUnityHoverState();
        }
    }
    
    // Public method to force cursor reset
    [ContextMenu("Force Reset Cursor")]
    public void ForceResetCursor()
    {
        if (enableDebugLogging)
            Debug.Log("🔧 Force reset cursor called");
            
        isCurrentlyHovering = false;
        SetDefaultCursorInternal();
    }
    
    // Public method to enable debug logging at runtime
    public void EnableDebugLogging(bool enable)
    {
        enableDebugLogging = enable;
        Debug.Log($"Cursor debug logging {(enable ? "enabled" : "disabled")}");
    }
    
    private void OnDestroy()
    {
        if (instance == this)
        {
            // Stop watchdog
            if (cursorWatchdog != null)
            {
                StopCoroutine(cursorWatchdog);
            }
            
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
