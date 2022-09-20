using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] Slider mouseSenSlider;
    [SerializeField] TextMeshProUGUI SensText;


    public static MouseSensitivity instance;

    private void Awake()
    {
        instance = this;
    }

    // Never run on MP, however, there will be multiple players in the scene, so, this grabs all the scripts
    public void UpdateMouseSens()
    {
        Player[] players = FindObjectsOfType<Player>();

        for (int x = 0; x < players.Length; x++)
            players[x].mouseSens = mouseSenSlider.value;

        int mouseSensValue = (int)mouseSenSlider.value;
        mouseSensValue = mouseSensValue / 100; // Makes it easier on the eyes
        SensText.text = mouseSensValue.ToString();

    }
}
