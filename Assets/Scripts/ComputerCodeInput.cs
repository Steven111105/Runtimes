using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComputerCodeInput : MonoBehaviour
{
    public static ComputerCodeInput instance; // Singleton instance for easy access
    public GameObject screen1GameObject;
    public GameObject screen2GameObject;

    [Header("Passwords")]
    public string[] passwords = new string[3]; // Array of 3 possible passwords

    [Header("UI Elements")]
    public Transform inputBoxParent; // Parent object containing the 8 input boxes
    public TextMeshProUGUI feedbackText; // TextMeshPro to show success/error messages

    [Header("Settings")]
    public int maxPasswordLength = 8;
    public int maxAttempts = 5;

    private string currentInput = "";
    private TextMeshProUGUI[] inputBoxTexts; // Will be populated automatically
    private int attemptsLeft;
    private bool hasAccess = false; // Flag to indicate if access is granted

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        hasAccess = false; // Initialize access as false
        // Automatically populate the inputBoxTexts array from the parent
        if (inputBoxParent == null)
        {
            Debug.LogError("Please assign the Input Box Parent object!");
            return;
        }

        // Get all TextMeshProUGUI components from children
        inputBoxTexts = inputBoxParent.GetComponentsInChildren<TextMeshProUGUI>();

        // Validate that we have exactly 8 input boxes
        if (inputBoxTexts == null || inputBoxTexts.Length != 8)
        {
            Debug.LogError($"Expected 8 TextMeshPro components in children, but found {(inputBoxTexts?.Length ?? 0)}!");
            return;
        }

        UpdateDisplay();
        if (feedbackText != null)
            feedbackText.text = "";
            
        // Initialize attempts
        attemptsLeft = maxAttempts;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Backspace();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Enter();
        }
        else if (Input.anyKeyDown)
        {
            Keyboard(Input.inputString.ToUpper());
        }
    }

    public void SwitchScreen()
    {
        screen1GameObject.SetActive(false);
        screen2GameObject.SetActive(true);
    }

    // Called when any keyboard button is clicked
    public void Keyboard(string key)
    {
        if(hasAccess) return; // Prevent input if access is granted
        // Check if it's a special key
        if (key == "BACKSPACE")
        {
            Backspace();
        }
        else if (key == "ENTER")
        {
            Enter();
        }
        else if (key.Length == 1) // Regular character
        {
            AddCharacter(key[0]);
        }
    }

    private void AddCharacter(char character)
    {
        // Prevent input if system is locked
        if (attemptsLeft <= 0) return;
        
        // Only add if we haven't reached max length
        if (currentInput.Length < maxPasswordLength)
        {
            currentInput += character;
            UpdateDisplay();
        }
    }

    private void Backspace()
    {
        // Prevent input if system is locked
        if (attemptsLeft <= 0) return;
        
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            UpdateDisplay();
        }
    }

    private void Enter()
    {
        if (attemptsLeft <= 0)
        {
            Debug.Log("You shouldnt be here");
            return;
        }
        
        if (currentInput.Length == maxPasswordLength)
        {
            CheckPassword();
        }
        else
        {
            ShowFeedback($"Password must be {maxPasswordLength} characters long!", false);
        }
    }

    private void CheckPassword()
    {
        // Check against all 3 passwords
        bool passwordCorrect = false;

        foreach (string correctPassword in passwords)
        {
            if (!string.IsNullOrEmpty(correctPassword) &&
                currentInput.ToUpper() == correctPassword.ToUpper()) // Case insensitive comparison
            {
                passwordCorrect = true;
                break;
            }
        }

        if (passwordCorrect)
        {
            ShowFeedback("ACCESS GRANTED!", true);
            hasAccess = true; // Flag to indicate access granted
            // Add your success logic here (e.g., unlock something, change scene, etc.)
            Debug.Log("Password correct - access granted!");
        }
        else
        {
            attemptsLeft--;
            
            if (attemptsLeft > 0)
            {
                ShowFeedback($"Wrong Answer, {attemptsLeft} attempts left", false);
                ClearInputOnly(); // Clear input immediately
                StartCoroutine(DelayedClearFeedback(1.5f)); // Clear feedback after delay
            }
            else
            {
                Debug.Log("Too Many Attempts Ending");
                EndScreenScript.instance.ShowEndScreen(3); // Show locked screen
            }
        }
    }

    private void UpdateDisplay()
    {
        if (inputBoxTexts == null || inputBoxTexts.Length != 8) return;

        // Update each input box
        for (int i = 0; i < 8; i++)
        {
            if (inputBoxTexts[i] != null)
            {
                if (i < currentInput.Length)
                {
                    // Show the actual character
                    inputBoxTexts[i].text = currentInput[i].ToString();
                }
                else
                {
                    // Empty boxes are just empty
                    inputBoxTexts[i].text = "";
                }
            }
        }
    }

    private void ShowFeedback(string message, bool isSuccess)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.color = isSuccess ? Color.green : Color.red;
        }
    }

    private void ClearFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    private void ClearInput()
    {
        currentInput = "";
        UpdateDisplay();
        ClearFeedback();
    }
    
    private void ClearInputOnly()
    {
        currentInput = "";
        UpdateDisplay();
    }
    
    private IEnumerator DelayedClearFeedback(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearFeedback();
    }

    // Public method to reset the input (useful for external scripts)
    public void ResetInput()
    {
        ClearInput();
    }
    
    // Public method to reset attempts (useful for external scripts)
    public void ResetAttempts()
    {
        attemptsLeft = maxAttempts;
        ClearInput();
        ClearFeedback();
    }
    
    // Public method to check if system is locked
    public bool IsSystemLocked()
    {
        return attemptsLeft <= 0;
    }

    void OnDestroy()
    {
        if (instance == this) instance = null;
    }
}
