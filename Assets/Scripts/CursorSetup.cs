using UnityEngine;

public class CursorSetup : MonoBehaviour
{
    [Header("Quick Setup")]
    [Tooltip("Creates simple cursor textures if none are provided")]
    [SerializeField] private bool createDefaultCursors = true;
    
    [Header("Cursor Colors")]
    [SerializeField] private Color defaultCursorColor = Color.white;
    [SerializeField] private Color hoverCursorColor = Color.yellow;
    [SerializeField] private Color clickCursorColor = Color.red;
    
    private void Start()
    {
        if (createDefaultCursors)
        {
            CreateDefaultCursors();
        }
    }
    
    private void CreateDefaultCursors()
    {
        CursorManager cursorManager = FindObjectOfType<CursorManager>();
        if (cursorManager == null)
        {
            Debug.LogWarning("CursorManager not found! Please add CursorManager to your scene.");
            return;
        }
        
        // Create simple arrow cursor textures if none exist
        if (cursorManager.GetComponent<CursorManager>())
        {
            Debug.Log("Creating default cursor textures...");
            // You can expand this to actually create simple cursor textures programmatically
            // For now, this serves as a placeholder for cursor setup
        }
    }
    
    // Helper method to create a simple colored cursor texture
    private Texture2D CreateSimpleCursor(Color color, int width = 32, int height = 32)
    {
        Texture2D cursor = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Color[] pixels = new Color[width * height];
        
        // Create a simple arrow shape
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                
                // Simple arrow pattern (you can customize this)
                if ((x == 0 && y < height/2) || // Left edge
                    (y == 0 && x < width/4) ||   // Top edge
                    (x == y && x < width/4))     // Diagonal
                {
                    pixels[index] = color;
                }
                else
                {
                    pixels[index] = Color.clear; // Transparent
                }
            }
        }
        
        cursor.SetPixels(pixels);
        cursor.Apply();
        return cursor;
    }
}
