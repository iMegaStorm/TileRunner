using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class Sc_ScoreSystem : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI[] playerScores;
    [SerializeField] int[] totalScore;
    [SerializeField] int score = 1;
    [SerializeField] int teamID;
    [SerializeField] GameObject winnerCanvas;
    [SerializeField] GameObject timerText;
    [SerializeField] TextMeshProUGUI winnerText;

    public static Sc_ScoreSystem instance;

    private void Awake()
    {
        instance = this;
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateScore(int _teamID)
    {
        RpcUpdateScoreText(_teamID);
    }

    [ClientRpc]
    public void RpcUpdateScoreText(int _teamID)
    {
        teamID = _teamID;

        if (teamID == 1)
        {
            //Debug.Log("Team1 Score " + _score + "/" + totalScore);
            totalScore[0] += score;
            playerScores[0].text = "" + totalScore[0];
        }
        else if (teamID == 2)
        {
            //bug.Log("Team2 Score " + _score + "/" + totalScore);
            totalScore[1] += score;
            playerScores[1].text = "" + totalScore[1];
        }
        else if (teamID == 3)
        {
            //Debug.Log("Team3 Score " + score + "/" + totalScore[2]);
            totalScore[2] += score;
            playerScores[2].text = "" + totalScore[2];
        }
        else if (teamID == 4)
        {
            //Debug.Log("Team4 Score " + score + "/" + totalScore[3]);
            totalScore[3] += score;
            playerScores[3].text = "" + totalScore[3];
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdRemoveScore(int _teamID)
    {
        RpcScoreDeduction(_teamID);
    }

    [ClientRpc]
    public void RpcScoreDeduction(int _teamID)
    {
        teamID = _teamID;
        if (teamID == 1)
        {
            //Debug.Log("Team1 Score " + _removeScore + "/" + totalScore[0]);
            totalScore[0] -= score;
            playerScores[0].text = "" + totalScore[0];
        }
        else if (teamID == 2)
        {
            //Debug.Log("Team2 Score " + _removeScore + "/" + totalScore[1]);
            totalScore[1] -= score;
            playerScores[1].text = "" + totalScore[1];
        }
        else if (teamID == 3)
        {
            //Debug.Log("Team3 Score " + _removeScore + "/" + totalScore[2]);
            totalScore[2] -= score;
            playerScores[2].text = "" + totalScore[2];
        }
        else if (teamID == 4)
        {
            //Debug.Log("Team4 Score " + _removeScore + "/" + totalScore[3]);
            totalScore[3] -= score;
            playerScores[3].text = "" + totalScore[3];
        }
    }

    [ServerCallback]
    public void CmdWinner()
    {
        RpcWinnerUI();
    }

    [ClientRpc]
    private void RpcWinnerUI()
    {
        winnerCanvas.SetActive(true);
        timerText.SetActive(false);
        Cursor.lockState = CursorLockMode.None;

        if (totalScore[0] > totalScore[1] && totalScore[0] > totalScore[2] && totalScore[0] > totalScore[3])
            winnerText.text = "Player1 has won with a score of " + totalScore[0];
        else if (totalScore[1] > totalScore[0] && totalScore[1] > totalScore[2] && totalScore[1] > totalScore[3])
            winnerText.text = "Player2 has won with a score of " + totalScore[1];
        else if (totalScore[2] > totalScore[0] && totalScore[2] > totalScore[1] && totalScore[2] > totalScore[3])
            winnerText.text = "Player3 has won with a score of " + totalScore[2];
        else if (totalScore[3] > totalScore[0] && totalScore[3] > totalScore[1] && totalScore[3] > totalScore[2])
            winnerText.text = "Player4 has won with a score of " + totalScore[3];
        else
            winnerText.text = "It's a draw!";
    }
}
