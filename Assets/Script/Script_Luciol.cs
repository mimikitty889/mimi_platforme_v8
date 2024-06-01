using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Luciol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {

            this.gameObject.SetActive(false);
        }

    }
}
