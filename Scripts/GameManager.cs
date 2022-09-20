using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public List<Player> playerList = new List<Player>();
    public bool gamePaused;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] NetworkManager networkManager;
    Player player = Player.instance;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        networkManager = GameObject.FindObjectOfType<NetworkManagerLobby>();
    }

    public void Resume()
    {
        TogglePauseGame();
    }

    public void MainMenu()
    {
        if (isServer)
            networkManager.StopHost();
        else
            networkManager.StopClient();
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false; //Makes it look like the application is quitting
        if (isServer)
            networkManager.StopHost();
        else
            networkManager.StopClient();

        Application.Quit();
    }

    public void TogglePauseGame()
    {
        gamePaused = !gamePaused;
        Cursor.lockState = gamePaused == true ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = gamePaused == true ? Cursor.visible = true : Cursor.visible = false;

        // toggle the pause menu
        TogglePauseMenu(gamePaused);
    }

    public void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }


}
