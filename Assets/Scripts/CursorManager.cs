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
    
    public void SetHoverCursor()
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
                SetDefaultCursor();
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
                SetDefaultCursor();
            }
        }
    }
    
    // Reset to default when game object is destroyed
    private void OnDestroy()
    {
        if (instance == this)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
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
}
