using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiqueMenu : MonoBehaviour
{
    private void Update()
    {
        Destroy(GameObject.Find("Audio Source Jeu"));
        // d�truire le audiomanager du jeu
    }
}
