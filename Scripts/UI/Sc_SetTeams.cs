using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SetTeams : MonoBehaviour
{
    public int currentID;

    [Header("Materials")]
    [SerializeField] public Material curMat;
    [SerializeField] public Material[] playerMats;

    [Header("Components")]
    [SerializeField] public MeshRenderer capsuleMR;

    public static Sc_SetTeams instance;
   

    private void Awake()
    {
        instance = this;
    }

    public void UpdateList()
    {
        Player player = Player.instance;
        GameManager gameManager = GameManager.instance;
        player.teamID = 0;

        foreach(Player players in gameManager.playerList)
        {
            player.teamID += 1;
            //PlayerSetup();
            for(int x = 0; x < gameManager.playerList.Count; x++)
            {
                player.curMat = playerMats[x];
                player.capsuleMR.material = playerMats[x];

            }
        }
    }

    //private void PlayerSetup()
    //{

    //    if (Sc_Player.instance.teamID == 1)
    //    {
    //        Debug.Log("Team1");
    //        Sc_Player.instance.curMat = playerMats[0];
    //        Sc_Player.instance.capsuleMR.material = playerMats[0];
    //    }
    //    else if (Sc_Player.instance.teamID == 2)
    //    {
    //        Debug.Log("Team2");
    //        Sc_Player.instance.curMat = playerMats[1];
    //        Sc_Player.instance.capsuleMR.material = playerMats[1];
    //    }
    //    else if (Sc_Player.instance.teamID == 3)
    //    {
    //        Debug.Log("Team3");
    //        Sc_Player.instance.curMat = playerMats[2];
    //        Sc_Player.instance.capsuleMR.material = playerMats[2];
    //    }
    //    else if (Sc_Player.instance.teamID == 4)
    //    {
    //        Debug.Log("Team4");
    //        Sc_Player.instance.curMat = playerMats[3];
    //        Sc_Player.instance.capsuleMR.material = playerMats[3];
    //    }
    //    else
    //    {
    //        Debug.LogError("Error");
    //    }
    //}

    //public void TileManager(Sc_ColourManager colourManager)
    //{
    //    if (colourManager.TileOwnedBy != Sc_Player.instance.teamID && colourManager.isTileOwned == false)
    //    {
    //        if (Sc_Player.instance.isLocalPlayer) //To stop being called multiple times by the player
    //        {
    //            Sc_ScoreSystem.instance.CmdUpdateScore(Sc_Player.instance.teamID);
    //        }
    //        colourManager.mR.material = Sc_Player.instance.curMat; //Sets the colourManagers material to the current material
    //        colourManager.TileOwnedBy = Sc_Player.instance.teamID; //Lets us know who owns that tile
    //        colourManager.isTileOwned = true;
    //    }
    //    else if (colourManager.TileOwnedBy != Sc_Player.instance.teamID && colourManager.isTileOwned == true)
    //    {
    //        if (Sc_Player.instance.isLocalPlayer)
    //        {
    //            Sc_ScoreSystem.instance.CmdRemoveScore(colourManager.TileOwnedBy);
    //            Sc_ScoreSystem.instance.CmdUpdateScore(Sc_Player.instance.teamID);
    //        }
    //        colourManager.mR.material = Sc_Player.instance.curMat;
    //        colourManager.TileOwnedBy = Sc_Player.instance.teamID;
    //    }
    //}
}
