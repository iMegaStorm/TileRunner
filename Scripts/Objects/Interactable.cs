using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Interactable : NetworkBehaviour
{
    [Command(requiresAuthority = false)]
    public void CmdDoorTrigger()
    {
        Sc_DoorSystem.instance.RpcDoorTrigger();
    }
}
