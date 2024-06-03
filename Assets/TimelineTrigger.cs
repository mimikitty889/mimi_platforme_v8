using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector Cinematic;

    IEnumerator TriggerCinematic()
    {
        yield return new WaitForSeconds(3f);
        Cinematic.Play(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            this.GetComponent<SpriteRenderer>().enabled = false;

            StartCoroutine(TriggerCinematic());
        }

    }
  
}
