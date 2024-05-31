using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Detector : MonoBehaviour
{
    public bool IsGround = false;
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform"){
        IsGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform"){
        IsGround = false;
        }
    }
}
