using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ColourManager : MonoBehaviour
{
    public MeshRenderer mR;
    public bool isTileOwned;
    public int TileOwnedBy;
    Player player;


    private void Start()
    {
        mR = GetComponent<MeshRenderer>();
    }

    public void TileManager()
    {
        Sc_ScoreSystem scoreSystem = Sc_ScoreSystem.instance;

        if (TileOwnedBy != player.teamID && isTileOwned == false)
        {
            if (player.isLocalPlayer) //To stop being called multiple times by the player
            {
                scoreSystem.CmdUpdateScore(player.teamID);
            }
            mR.material = player.curMat; //Sets the s material to the current material
            TileOwnedBy = player.teamID; //Lets us know who owns that tile
            isTileOwned = true;
        }
        else if (TileOwnedBy != player.teamID && isTileOwned == true)
        {
            if (player.isLocalPlayer)
            {
                scoreSystem.CmdRemoveScore(TileOwnedBy);
                scoreSystem.CmdUpdateScore(player.teamID);
            }
            mR.material = player.curMat;
            TileOwnedBy = player.teamID;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Sc_TimerSystem.instance.roundOver)
        {
            player = other.GetComponent<Player>();
            TileManager();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Yup");
    //        UpdateTeams();
    //    }
    //}


    //public void UpdateTeams()
    //{
    //    Debug.Log("Happening");
    //    if (Sc_Player.instance.teamBlue == 1)
    //        mR.material = matPlayer1;
    //    else if (Sc_Player.instance.teamRed == 1)
    //        mR.material = matPlayer2;

    //}
}
