using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class Sc_TimerSystem : NetworkBehaviour
{
    [SyncVar] [SerializeField] float countdownTimer = 5f;
    [SyncVar] [SerializeField] float levelTimer;
    [SyncVar] [SerializeField] int totalTeams;
    [SyncVar] [SerializeField] public bool roundStarted;
    [SyncVar] [SerializeField] public bool roundOver;

    [SerializeField] GameObject countDownUI;
    [SerializeField] GameObject connectingMenu;
    [SerializeField] TextMeshProUGUI countdownTimerText;
    [SerializeField] TextMeshProUGUI levelTimerText;
    [SerializeField] NetworkManagerLobby networkManager;

    public static Sc_TimerSystem instance;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        networkManager = GameObject.FindObjectOfType<NetworkManagerLobby>();
        totalTeams = networkManager.playerCount;
        levelTimerText.text = "Time: " + levelTimer;
    }

    [ServerCallback]
    private void Update()
    {
        if (countdownTimer >= -2 && GameManager.instance.playerList.Count >= totalTeams)
        {
            countdownTimer -= Time.deltaTime;
            RpcCountdownUI();
        }

        if (roundStarted == true && !roundOver)
        {
            levelTimer -= Time.deltaTime;
            RpcClientTimer();
        }
    }

    [ClientRpc]
    void RpcCountdownUI()
    {
        connectingMenu.SetActive(false);
        if (countdownTimer < 3f && countdownTimer > 2f)
        {
            countDownUI.SetActive(true);
        }
        else if (countdownTimer < 2f && countdownTimer > 1f)
        {
            countdownTimerText.text = "2";
        }
        else if (countdownTimer < 1f && countdownTimer > 0f)
        {
            countdownTimerText.text = "1";
        }
        else if (countdownTimer <= 0f && countdownTimer > -1f)
        {
            countdownTimerText.text = "GO";
            roundStarted = true;
        }
        else if (countdownTimer <= -1f)
            countDownUI.SetActive(false);
    }

    [ClientRpc]
    void RpcClientTimer()
    {
        levelTimerText.text = "Time: " + levelTimer.ToString("f1");
        if (levelTimer <= 0.0f)
        {
            Cursor.lockState = CursorLockMode.None;
            levelTimer = 0.0f;
            roundOver = true;
            Sc_ScoreSystem.instance.CmdWinner();
        }
    }
}