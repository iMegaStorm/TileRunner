using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Sc_DoorSystem : NetworkBehaviour
{
    [SerializeField] GameObject[] doors;
    [SerializeField] Animator doorAnim1;
    [SerializeField] Animator doorAnim2;
    [SerializeField] Animator buttonAnim1;
    [SerializeField] Animator buttonAnim2;
    [SerializeField] float waitTime;
    [SerializeField] bool isDoorOpening;
    [SerializeField] MeshRenderer buttonMR1;
    [SerializeField] MeshRenderer buttonMR2;
    [SerializeField] Material[] buttonStates;

    public static Sc_DoorSystem instance;

    private void Awake()
    {
        instance = this;
    }

    [ClientRpc]
    public void RpcDoorTrigger()
    {
        if(!isDoorOpening)
        {
            isDoorOpening = true;
            buttonMR1.material = buttonStates[1];
            buttonMR2.material = buttonStates[1];
            buttonAnim1.SetTrigger("Button");
            buttonAnim2.SetTrigger("Button");
            doorAnim1.SetTrigger("Door");
            doorAnim2.SetTrigger("Door");
            StartCoroutine(DoorReset());
        }
    }

    IEnumerator DoorReset()
    {
        yield return new WaitForSeconds(waitTime);
        isDoorOpening = false;
        buttonMR1.material = buttonStates[0];
        buttonMR2.material = buttonStates[0];
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
            
    //}
}
