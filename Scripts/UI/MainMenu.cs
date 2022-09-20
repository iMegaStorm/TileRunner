using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] NetworkManagerLobby networkManager;
    [SerializeField] InputField portNumberInput;
    [SerializeField] Dropdown playerCountDropdown;
    [SerializeField] Button startButton;
    [SerializeField] int playerCount;
    private int dropdownValue;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void UpdatePlayerCount()
    {
        dropdownValue = playerCountDropdown.value;
        playerCount = int.Parse(playerCountDropdown.options[dropdownValue].text);

        if (portNumberInput.text != null && dropdownValue != 0)
            startButton.interactable = true;
        else
            startButton.interactable = false;
    }

    public void HostLobby()
    {
        networkManager.StartHost(); //Tells the networkManager to start hosting a game
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
