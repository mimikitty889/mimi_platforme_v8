using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musiqueMenu : MonoBehaviour
{
    private void Update()
    {
        Destroy(GameObject.Find("Audio Source Jeu"));
        // détruire le audiomanager du jeu
    }
}
