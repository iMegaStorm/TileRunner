using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_ReturnToMainMenu : MonoBehaviour
{
    private NetworkManagerLobby networkManager;
    private GameObject networkManagerObj;

    private void Start()
    {
        networkManagerObj = GameObject.Find("NetworkManager");
        networkManager = networkManagerObj.GetComponent<NetworkManagerLobby>();
    }

    public void ReturnToMainMenu()
    {
        networkManager.StopHost();
    }
}
