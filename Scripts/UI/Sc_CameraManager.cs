using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOT BEING USED

public class Sc_CameraManager : MonoBehaviour
{
    [SerializeField] Camera[] cameras;
    [SerializeField] GameObject[] cameraObjs;
    public static Sc_CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    //public void SetCamera()
    //{
    //    GameManager gameManager = GameManager.instance;

    //    foreach (Sc_Player players in gameManager.playerList)
    //    {
    //        for (int x = 0; x < gameManager.playerList.Count; x++)
    //        {
    //            Sc_Player.instance.myCamera = cameras[x];
    //            cameraObjs[x].SetActive(true);
    //        }
    //    }
    //}
}
