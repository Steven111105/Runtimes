using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class PlayerPrefsEditor : MonoBehaviour
{
    [Header("Clear PlayerPrefs (Editor Only)")]
    [SerializeField] private bool clearAllPlayerPrefs = false;
    
    [Header("Clear Specific Ending PlayerPrefs")]
    [SerializeField] private bool clearTeaTimeEnding = false;
    [SerializeField] private bool clearSelfDestructEnding = false;
    [SerializeField] private bool clearWarStarterEnding = false;
    [SerializeField] private bool clearTooManyAttemptsEnding = false;
    [SerializeField] private bool clearYouDidNothingEnding = false;
    [SerializeField] private bool clearRunningAwayEnding = false;
    [SerializeField] private bool clearPeskyButtonEnding = false;
    [SerializeField] private bool clearTerminusEnding = false;
    
    private void OnValidate()
    {
        if (!Application.isPlaying) return;
        
        if (clearAllPlayerPrefs)
        {
            ClearAllPlayerPrefs();
            clearAllPlayerPrefs = false;
        }
        
        if (clearTeaTimeEnding)
        {
            PlayerPrefs.DeleteKey("TeaTimeEnding");
            Debug.Log("Cleared TeaTimeEnding PlayerPref");
            clearTeaTimeEnding = false;
        }
        
        if (clearSelfDestructEnding)
        {
            PlayerPrefs.DeleteKey("SelfDestructEnding");
            Debug.Log("Cleared SelfDestructEnding PlayerPref");
            clearSelfDestructEnding = false;
        }
        
        if (clearWarStarterEnding)
        {
            PlayerPrefs.DeleteKey("WarStarterEnding");
            Debug.Log("Cleared WarStarterEnding PlayerPref");
            clearWarStarterEnding = false;
        }
        
        if (clearTooManyAttemptsEnding)
        {
            PlayerPrefs.DeleteKey("TooManyAttemptsEnding");
            Debug.Log("Cleared TooManyAttemptsEnding PlayerPref");
            clearTooManyAttemptsEnding = false;
        }
        
        if (clearYouDidNothingEnding)
        {
            PlayerPrefs.DeleteKey("YouDidNothingEnding");
            Debug.Log("Cleared YouDidNothingEnding PlayerPref");
            clearYouDidNothingEnding = false;
        }
        
        if (clearRunningAwayEnding)
        {
            PlayerPrefs.DeleteKey("RunningAwayEnding");
            Debug.Log("Cleared RunningAwayEnding PlayerPref");
            clearRunningAwayEnding = false;
        }
        
        if (clearPeskyButtonEnding)
        {
            PlayerPrefs.DeleteKey("PeskyButtonEnding");
            Debug.Log("Cleared PeskyButtonEnding PlayerPref");
            clearPeskyButtonEnding = false;
        }
        
        if (clearTerminusEnding)
        {
            PlayerPrefs.DeleteKey("TerminusEnding");
            Debug.Log("Cleared TerminusEnding PlayerPref");
            clearTerminusEnding = false;
        }
    }
    
    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("🗑️ Cleared ALL PlayerPrefs!");
    }
    
    [ContextMenu("Clear All PlayerPrefs")]
    public void ClearAllPlayerPrefsContextMenu()
    {
        ClearAllPlayerPrefs();
    }
    
    [ContextMenu("Clear All Ending PlayerPrefs")]
    public void ClearAllEndingPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("TeaTimeEnding");
        PlayerPrefs.DeleteKey("SelfDestructEnding");
        PlayerPrefs.DeleteKey("WarStarterEnding");
        PlayerPrefs.DeleteKey("TooManyAttemptsEnding");
        PlayerPrefs.DeleteKey("YouDidNothingEnding");
        PlayerPrefs.DeleteKey("RunningAwayEnding");
        PlayerPrefs.DeleteKey("PeskyButtonEnding");
        PlayerPrefs.DeleteKey("TerminusEnding");
        PlayerPrefs.Save();
        Debug.Log("🗑️ Cleared all ending PlayerPrefs!");
    }
}

// Add menu items for easy access
public class PlayerPrefsMenu
{
    [MenuItem("Tools/PlayerPrefs/Clear All PlayerPrefs")]
    public static void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("🗑️ Cleared ALL PlayerPrefs from menu!");
    }
    
    [MenuItem("Tools/PlayerPrefs/Clear Ending PlayerPrefs")]
    public static void ClearEndingPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("TeaTimeEnding");
        PlayerPrefs.DeleteKey("SelfDestructEnding");
        PlayerPrefs.DeleteKey("WarStarterEnding");
        PlayerPrefs.DeleteKey("TooManyAttemptsEnding");
        PlayerPrefs.DeleteKey("YouDidNothingEnding");
        PlayerPrefs.DeleteKey("RunningAwayEnding");
        PlayerPrefs.DeleteKey("PeskyButtonEnding");
        PlayerPrefs.DeleteKey("TerminusEnding");
        PlayerPrefs.Save();
        Debug.Log("🗑️ Cleared all ending PlayerPrefs from menu!");
    }
    
    [MenuItem("Tools/PlayerPrefs/Log All PlayerPrefs")]
    public static void LogAllPlayerPrefs()
    {
        Debug.Log("=== CURRENT PLAYERPREFS ===");
        Debug.Log($"TeaTimeEnding: {PlayerPrefs.GetInt("TeaTimeEnding", 0)}");
        Debug.Log($"SelfDestructEnding: {PlayerPrefs.GetInt("SelfDestructEnding", 0)}");
        Debug.Log($"WarStarterEnding: {PlayerPrefs.GetInt("WarStarterEnding", 0)}");
        Debug.Log($"TooManyAttemptsEnding: {PlayerPrefs.GetInt("TooManyAttemptsEnding", 0)}");
        Debug.Log($"YouDidNothingEnding: {PlayerPrefs.GetInt("YouDidNothingEnding", 0)}");
        Debug.Log($"RunningAwayEnding: {PlayerPrefs.GetInt("RunningAwayEnding", 0)}");
        Debug.Log($"PeskyButtonEnding: {PlayerPrefs.GetInt("PeskyButtonEnding", 0)}");
        Debug.Log($"TerminusEnding: {PlayerPrefs.GetInt("TerminusEnding", 0)}");
    }
}
#endif
