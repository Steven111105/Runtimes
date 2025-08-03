using UnityEngine;

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
    
    public static CursorManager instance;
    
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
    }
    
    public void SetDefaultCursor()
    {
        if (defaultCursor != null)
        {
            Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
        }
        else
        {
            // Fallback to system default
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    
    public void SetHoverCursor()
    {
        if (hoverCursor != null)
        {
            Cursor.SetCursor(hoverCursor, hoverHotspot, CursorMode.Auto);
        }
        else
        {
            // Fallback to system default hover (usually pointing hand)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    
    public void SetClickCursor()
    {
        if (clickCursor != null)
        {
            Cursor.SetCursor(clickCursor, clickHotspot, CursorMode.Auto);
        }
    }
    
    // Method to set custom cursor with parameters
    public void SetCustomCursor(Texture2D cursor, Vector2 hotspot)
    {
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }
    
    // Reset to default when game object is destroyed
    private void OnDestroy()
    {
        if (instance == this)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
