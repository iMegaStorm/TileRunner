using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [Header("Stats")]
    [SerializeField] [SyncVar] int curHp;
    [SerializeField] public float moveSpeed = 12f;
    [SerializeField] public int teamID;
    [SerializeField] float interactHeight;
    [SerializeField] int interactRange;

    [Header("Camera")]
    [SerializeField] public float cameraHeight;
    [SerializeField] public float mouseSens;
    float updatedMouseSens;
    float cameraPitch = 0f;

    [Header("Materials")]
    [SerializeField] public Material curMat;
    [SerializeField] public Material[] playerMats;

    [Header("Components")]
    [SerializeField] Camera myCamera;
    [SerializeField] GameObject myCameraObj;
    [SerializeField] Transform cameraTransform;
    [SerializeField] GameObject localBody;
    [SerializeField] Transform myTransform;
    [SerializeField] public MeshRenderer capsuleMR;
    [SerializeField] CharacterController controller;

    Sc_TimerSystem timerSystem = Sc_TimerSystem.instance;
    GameManager gameManager = GameManager.instance;

    int layerMask = 1 << 6;

    public static Player instance;

    private void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Sc_SetTeams setTeams = Sc_SetTeams.instance;
        gameManager.playerList.Add(this);
        setTeams.UpdateList();

        if (!isLocalPlayer)
            myCameraObj.SetActive(false);
        else
            localBody.SetActive(false); //Set the localBody to inactive so the camera cant see the localPlayer
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (!timerSystem.roundOver && !gameManager.gamePaused)
            LocalPlayerCamera();

        if (Input.GetKeyDown(KeyCode.Escape) && !timerSystem.roundOver)
            gameManager.TogglePauseGame();
        
        if (Input.GetMouseButtonDown(0))
            Interact();
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (timerSystem.roundStarted && !timerSystem.roundOver && !gameManager.gamePaused)
        {
            LocalPlayerMovement();
        }
    }

    void Interact()
    {
        Vector3 temp = new Vector3(transform.position.x, interactHeight, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(temp, transform.TransformDirection(Vector3.forward), out hit, interactRange, layerMask))
        {
            hit.transform.gameObject.GetComponent<Interactable>().CmdDoorTrigger();
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.Log("Did not Hit");
        }
    }

    void LocalPlayerCamera()
    {
        myCamera.transform.position = myTransform.transform.position - myTransform.transform.forward; //Set the camera to localBodies forward position
        myCamera.transform.LookAt(myTransform.forward); //Look at the transform
        myCamera.transform.position = new Vector3(myTransform.position.x, cameraHeight, myTransform.position.z); //Set the camera to the local players transform (so the camera isnt behind the player)
        //Debug.Log(myTransform.position.x + " | " + myTransform.position.y + " | " + myTransform.position.z);

        //Camera settings for looking around
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraPitch -= mouseDelta.y * mouseSens * Time.deltaTime;
        cameraPitch = Mathf.Clamp(cameraPitch, -45f, 45f);
        cameraTransform.localEulerAngles = Vector3.right * cameraPitch;
        myTransform.Rotate(Vector3.up * mouseDelta.x * mouseSens * Time.deltaTime);
    }

    void LocalPlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Vector3 temp = new Vector3(transform.position.x, interactHeight, transform.position.z);
        Debug.DrawRay(temp, transform.TransformDirection(Vector3.forward) * interactRange, Color.red);
    }

    //public void TileManager()
    //{
    //    Sc_ScoreSystem scoreSystem = Sc_ScoreSystem.instance;
    //    if (colourManager.TileOwnedBy != teamID && colourManager.isTileOwned == false)
    //    {
    //        if (isLocalPlayer) //To stop being called multiple times by the player
    //        {
    //            scoreSystem.CmdUpdateScore(teamID);
    //        }
    //        colourManager.mR.material = curMat; //Sets the colourManagers material to the current material
    //        colourManager.TileOwnedBy = teamID; //Lets us know who owns that tile
    //        colourManager.isTileOwned = true;
    //    }
    //    else if (colourManager.TileOwnedBy != teamID && colourManager.isTileOwned == true)
    //    {
    //        if (isLocalPlayer)
    //        {
    //            scoreSystem.CmdRemoveScore(colourManager.TileOwnedBy);
    //            scoreSystem.CmdUpdateScore(teamID);
    //        }
    //        colourManager.mR.material = curMat;
    //        colourManager.TileOwnedBy = teamID;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("FloorTrigger"))
    //    {
    //        colourManager = other.GetComponent<Sc_ColourManager>();
    //        TileManager();
    //    }
    //}
}

//private void PlayerSetup()
//{

//    if (teamID == 1)
//    {
//        curMat = playerMats[0];
//        capsuleMR.material = curMat;
//    }
//    else if (teamID == 2)
//    {
//        curMat = playerMats[1];
//        capsuleMR.material = curMat;
//    }
//    else if (teamID == 3)
//    {
//        curMat = playerMats[2];
//        capsuleMR.material = curMat;
//    }
//    else
//    {
//        curMat = playerMats[3];
//        capsuleMR.material = curMat;
//    }
//}

