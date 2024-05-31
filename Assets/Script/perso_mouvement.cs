using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class perso_mouvement : MonoBehaviour
{
    int Speed = 5;
    int Boing = 8;
    public bool Jump;
    public float JumpTimeCounter;
    public float JumpTime = 1;
    public float FallMultiplier = 2.5f ;
    public float JumpMultiplier = 2.5f;
   
    public float checkRadius;
    public LayerMask WhatIsGround;
    public bool IsGround = false;
    public Transform FD; 

    /*private bool canDash = true;
    private bool isDashing;
    private float DashingPower DP = 24f;
    private float DashingTime DT = 0.2f;
    private float DashingCooldown DC = 1f;*/

    [SerializeField] SpriteRenderer SR;
    [SerializeField] Rigidbody2D RB;
    //[SerializeField] private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent <Rigidbody2D>(); 
    }

    
    // Update is called once per frame
    void Update()
    {
    PlayerMouvement();
    }
    private void FixedUpdate(){
        IsGround = Physics2D.OverlapCircle(FD.position, checkRadius, WhatIsGround);
    if (RB.velocity.y < 0){
    RB.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier -1) * Time.deltaTime;
    }
    else if (RB.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
    {
    RB.velocity += Vector2.up * Physics2D.gravity.y * (JumpMultiplier -1) * Time.deltaTime;
    }
    }

     // la je veux que mon perso bouge 
    void PlayerMouvement()
    {
    //quand on appuie sur la fleche de droite ect saut et tout ça
    if (Input.GetKey(KeyCode.RightArrow))
    {
          transform.Translate(Vector2.right * Speed * Time.deltaTime);
          SR.flipX = false;
    }
    if (Input.GetKey(KeyCode.LeftArrow))
    {
          transform.Translate(Vector2.left * Speed * Time.deltaTime);
          SR.flipX = true;
    }

    /*if (isDashing)
    {
    return; 
    }*/

    if (Input.GetKeyDown(KeyCode.Space) && IsGround == true)
    {
    if (IsGround == true)   
    {RB.velocity = (Vector2.up * Boing);
    Jump = true; 
    JumpTimeCounter = JumpTime;}
   
    if (Jump == true && Input.GetKey(KeyCode.Space))
    {if (JumpTimeCounter > 0)
     {RB.velocity = (Vector2.up * Boing); 
     JumpTimeCounter-= Time.deltaTime;
     }
     else {Jump = false;}
    }
    }
    
    if (Input.GetKeyUp(KeyCode.Space)) 
    {
        Jump = false;
    }

    /*if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
    {
        StartCoroutine(Dash());
    } */

    //rainbow dash :p
    /*public IEnumator Dash()

   {
    canDash = false;
    isDashing = true;
    float originalGravity = rb.gravityScale;
    rb.gravityScale = 0f;
    rb.velocity = new Vector2(transform.localScale.x * DP, 0f);
    tr.emitting = true;
    yield return new WaitForSeconds(DT);
    tr.emitting = false;
    rb.gravityScale = originalGravity;
    isDashing = false;
    yield return new WaitForSeconds(DC);
    canDash = true; 
   }*/
 
    // platformes :3
    void OnTriggerEnter2D(Collision2D FD)
    {
        IsGround = true;
        Jump = false;
    }
    void OnTriggerExit2D(Collision2D FD)
    {
        IsGround = false;
        Jump = true;
    }

 }}