using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musique : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);     // ne d�truis pas l'objet en changeant de sc�ne.
    }
}
