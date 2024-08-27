using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private GameObject adminToolsPanel;
    [SerializeField] private GameObject adminAccessButton;
    public TMP_InputField usernameInput; // Assign in the Unity inspector
    public TMP_InputField passwordInput;
    public TextMeshProUGUI feedbackText; // TextMeshProUGUI for UI text display
    public TMP_InputField adminUsernameInput; // Assign in the Unity inspector
    public TMP_InputField adminPasswordInput;
    public TextMeshProUGUI adminFeedbackText; // TextMeshProUGUI for UI text display
    private GameObject content;
    
    // UI references
    // public GameObject typerPrefab;
    // public GameObject spawnerPrefab;
    public GameObject gamePrefab;
    // public GameObject wordBankPrefab;
    // public GameObject typer;
    // public GameObject spawner;
    public GameObject game;
    // public GameObject wordBank;

    // Start is called before the first frame update
    void Start()
    {
        AccountManagerBehaviour.Instance.AccountManager.CreateAdminAccount();
    }

    /*public void playGame()
    {
        SceneManager.LoadScene("TypingPrototype");
    }*/
    
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

     // Show the Option menu and disable the Main menu
    public void ShowOptions()
    {
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    // Show the Main menu and disable any previous menu
    public void ShowMainMenu()
    {
        menuPanel.SetActive(true);

        optionPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        loginPanel.SetActive(false);
        adminToolsPanel.SetActive(false);

    }

    // Show Login panel from Play button
    public void ShowLogin()
    {
        menuPanel.SetActive(false);
        
        loginPanel.SetActive(true);
        adminToolsPanel.SetActive(false);

    }

    public void ShowLeaderboard()
    {
        menuPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        adminToolsPanel.SetActive(false);
        adminAccessButton.SetActive(true);

        // Erase input text if any
        adminFeedbackText.text = "";
        adminUsernameInput.text = "";
        adminPasswordInput.text = "";
    }

    public void ShowAdminToolsPanel()
    {
        adminAccessButton.SetActive(false);
        adminToolsPanel.SetActive(true);
    }

    public void CreateAccount()
    {
        string username = usernameInput.text;  // Directly access the text property of TMP_InputField
        string password = passwordInput.text;  // Same here for password

        // Check minimum length requirements
        if (username.Length <= 3)
        {
            feedbackText.text = "Username must be longer than 3 characters.";
            return;
        }
        if (password.Length <= 6)
        {
            feedbackText.text = "Password must be longer than 6 characters.";
            return;
        }

        bool success = AccountManagerBehaviour.Instance.AccountManager.CreateAccount(username, password);

        if (success)
        {
            feedbackText.text = "Account created successfully!";
            // Debug.Log("Account created successfully!");
        }
        else
        {
            feedbackText.text = "Account creation failed. User '" + username + "' already exists.";
            Debug.Log("Account creation failed. Username already exists.");
        }
    }

    public void Login()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Check minimum length requirements
        if (username.Length <= 0)
        {
            feedbackText.text = "Please enter username"; // Correctly accessing the text property
            return;
        }
        if (password.Length <= 0)
        {
            feedbackText.text = "Please enter password";
            return;
        }

        bool success = AccountManagerBehaviour.Instance.AccountManager.Login(username, password);

        if (success)
        {
            feedbackText.text = "Login Successful";
            // Debug.Log("Login successful!");
            AccountManagerBehaviour.Instance.currentAccount = username;
            StartGame();
        }
        else
        {
            feedbackText.text = "Invalid username or password. Please try again.";
            // Debug.Log("Login failed. Check username and password.");
        }
    }

    public void AdminLogin()
    {
        content = GameObject.Find("Content");
        string username = adminUsernameInput.text;
        string password = adminPasswordInput.text;

        if (username.Length <= 0)
        {
            feedbackText.text = "Please enter username";
            return;
        }
        if (password.Length <= 0)
        {
            feedbackText.text = "Please enter password";
            return;
        }

        bool success = AccountManagerBehaviour.Instance.AccountManager.Login(username, password);

        if (success && AccountManagerBehaviour.Instance.AccountManager.IsAdmin(username)) // Check if user is also an admin
        {
            adminFeedbackText.text = "Admin Privilages Granted";
            Debug.Log("Admin login successful!");
            // AccountManagerBehaviour.Instance.currentAccount = username;
            content.GetComponent<Leaderboard>().ToggleAdminButtons(true);  // Enable admin buttons

        }
        else
        {
            adminFeedbackText.text = "Invalid Credentials";
            Debug.Log("Login failed. Check username and password, or not an admin.");
            content.GetComponent<Leaderboard>().ToggleAdminButtons(false);  // Disable admin buttons
        }
    }

    private void StartGame()
    {
        // Disable menu
        menuPanel.SetActive(false);
        optionPanel.SetActive(false);
        loginPanel.SetActive(false);

        // Start game
        // wordBank = Instantiate(WordBankPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        game = Instantiate(gamePrefab);
        // typer = Instantiate(TyperPrefab, new Vector3(0, -5.25f, 0), Quaternion.identity);
        // spawner = Instantiate(SpawnerPrefab, new Vector3(400, 1000f, 0), Quaternion.identity);


    }

    public void EndGame()
    {
        // Return to Main menu
        ShowMainMenu();
        Destroy(game);
    }
}

