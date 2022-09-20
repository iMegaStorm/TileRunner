using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Pickup : NetworkBehaviour
{
    [Header("Powerup")]
    [SerializeField] int minValue;
    [SerializeField] int maxValue;
    [SerializeField] GameObject pickupObj;
    [SerializeField] SphereCollider powerupCollider;

    [Header("Powerup Effects")]
    [SerializeField] GameObject playerBlackScreen;
    [SerializeField] GameObject blackScreen;
    [SerializeField] GameObject playerSpeedupScreen;
    [SerializeField] GameObject speedupScreen;
    [SerializeField] GameObject playerSlowedScreen;
    [SerializeField] GameObject slowedScreen;

    [Header("Audio")]
    [SerializeField] AudioClip pickupSfx;

    [Header("Bobbing")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float bobSpeed;
    [SerializeField] float bobHeight;
    private Vector3 startPos;
    private bool bobbingUp;

    void Start()
    {
        // set the start position
        startPos = transform.position;
    }

    void Update()
    {
        // rotating
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        // bob up and down
        Vector3 offset = (bobbingUp == true ? new Vector3(0, bobHeight / 2, 0) : new Vector3(0, -bobHeight / 2, 0)); // ternary statement thats true or false and then moves upwards or downwards
        transform.position = Vector3.MoveTowards(transform.position, startPos + offset, bobSpeed * Time.deltaTime);

        if (transform.position == startPos + offset)
            bobbingUp = !bobbingUp;
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            other.GetComponent<AudioSource>().PlayOneShot(pickupSfx);
            
            var powerupValue = Random.Range(minValue, maxValue);
            RpcPowerup(player, powerupValue);
        }
    }

    [ClientRpc]
    private void RpcPowerup(Player player, int _powerupValue)
    {
        powerupCollider.enabled = false;
        pickupObj.SetActive(false);

        StartCoroutine(PowerupEffect(player, _powerupValue));
    }

    IEnumerator PowerupEffect(Player player, int _powerupValue)
    {
        if (_powerupValue == 1)
        {
            if (player.isLocalPlayer)
            {
                player.moveSpeed = 15f;
                playerSpeedupScreen.SetActive(true);
                yield return new WaitForSeconds(10f);
                player.moveSpeed = 12f;
                playerSpeedupScreen.SetActive(false);
            }
            else
            {
                speedupScreen.SetActive(true);
                yield return new WaitForSeconds(10f);
                speedupScreen.SetActive(false);
            }
        }
        else if (_powerupValue == 2)
        {
            if (!player.isLocalPlayer)
            {
                blackScreen.SetActive(true);
                yield return new WaitForSeconds(10f);
                blackScreen.SetActive(false);
            }
            else
            {
                playerBlackScreen.SetActive(true);
                yield return new WaitForSeconds(10f);
                playerBlackScreen.SetActive(false);
            }
        }
        else if (_powerupValue == 3)
        {
            if (!player.isLocalPlayer)
            {
                Player[] otherPlayers = FindObjectsOfType<Player>();

                slowedScreen.SetActive(true);
                for(int x = 0; x < otherPlayers.Length; x++)
                    otherPlayers[x].moveSpeed = 6f;

                yield return new WaitForSeconds(10f);

                slowedScreen.SetActive(false);
                for (int x = 0; x < otherPlayers.Length; x++)
                    otherPlayers[x].moveSpeed = 12f;
            }
            else
            {
                playerSlowedScreen.SetActive(true);
                yield return new WaitForSeconds(10f);
                playerSlowedScreen.SetActive(false);
            }
        }
         
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        pickupObj.SetActive(true);
        powerupCollider.enabled = true;
    }
}
