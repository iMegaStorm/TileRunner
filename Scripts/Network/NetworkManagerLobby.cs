using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class NetworkManagerLobby : NetworkManager
{
    [SerializeField] Dropdown playerCountDropdown;
    [SerializeField] public int playerCount;
    private int dropdownValue;

    public void UpdatePlayerCount()
    {
        dropdownValue = playerCountDropdown.value;
        playerCount = int.Parse(playerCountDropdown.options[dropdownValue].text);
    }
}

