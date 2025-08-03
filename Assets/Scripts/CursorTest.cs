using UnityEngine;

public class CursorTest : MonoBehaviour
{
    [Header("Test Cursor")]
    [SerializeField] private bool testWithoutTextures = true;
    
    void Start()
    {
        Debug.Log("=== CURSOR SYSTEM TEST ===");
        
        // Check if CursorManager exists
        CursorManager cursorManager = FindObjectOfType<CursorManager>();
        if (cursorManager == null)
        {
            Debug.LogError("❌ CursorManager not found in scene! Please add CursorManager to a GameObject.");
            return;
        }
        else
        {
            Debug.Log("✅ CursorManager found!");
        }
        
        // Test basic cursor change
        if (testWithoutTextures)
        {
            Debug.Log("🧪 Testing cursor change without custom textures...");
            StartCoroutine(TestCursorChanges());
        }
    }
    
    System.Collections.IEnumerator TestCursorChanges()
    {
        Debug.Log("Setting default cursor...");
        CursorManager.instance.SetDefaultCursor();
        yield return new WaitForSeconds(2f);
        
        Debug.Log("Setting hover cursor...");
        CursorManager.instance.SetHoverCursor();
        yield return new WaitForSeconds(2f);
        
        Debug.Log("Back to default cursor...");
        CursorManager.instance.SetDefaultCursor();
        
        Debug.Log("✅ Cursor test complete! If you didn't see cursor changes, check the setup below.");
    }
    
    void Update()
    {
        // Manual test - press space to toggle cursor
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CursorManager.instance != null)
            {
                Debug.Log("🔄 Toggling cursor with SPACE key");
                CursorManager.instance.SetHoverCursor();
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (CursorManager.instance != null)
            {
                CursorManager.instance.SetDefaultCursor();
            }
        }
    }
}
