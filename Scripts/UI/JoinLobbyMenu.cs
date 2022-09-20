using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] NetworkManagerLobby networkManager;

    [Header("UI")]
    [SerializeField] Button joinButton;

    //On join lobby button, this function is called
    public void JoinLobby()
    {
        string ipAddress = "localhost";
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
        joinButton.interactable = false;
    }
}
