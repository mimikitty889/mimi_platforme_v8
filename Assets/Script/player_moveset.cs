using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;
using Cinemachine;


public class player_moveset : MonoBehaviour
{
    /* --- Camera --- */
    [SerializeField] private CinemachineVirtualCamera Camera;

    /* --- Stat --- */
    [Header("Stat")]
    [SerializeField] private float Movement_Speed;
    [SerializeField] private float Jump_Height;
    [SerializeField] private float Fall_MaxSpeed;


    /* --- GroundCheck --- */
    [Header("GroundCheck")]
    [SerializeField] private Transform Ground_Check_Position;
    [SerializeField] private float Ground_Check_Radius;
    [SerializeField] private LayerMask Ground_Check_Layer;

    /* --- Control --- */
    private Controls_Player _player_manager;
    private InputAction _Run, _Jump, _Pause;
    private Vector2 Input_Direction;

    /* --- Shortcut --- */
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Animator _Animator;

    /* --- State --- */
    private bool _IsGrounded;
    private bool Freeze;

    /* --- Enemy --- */
    [Header("Enemy")]
    [SerializeField] private Transform Enemy_Detector_position;
    [SerializeField] private float Enemy_Detector_Radius;
    [SerializeField] private LayerMask Enemy_Detector_LayerMask;


    /* --- Collectible --- */
    [Header("Collectible")]
    [SerializeField] private int Coin_Amount;

    /* --- Pause --- */
    [Header("Pause Setup")]
    [SerializeField] private Transform Pause_Menue;

    /* --- Waypoint --- */
    [Header("Waypoint")]
    [SerializeField] private Transform Waypoint_Detector_position;
    [SerializeField] private float Waypoint_Detector_Radius;
    [SerializeField] private LayerMask Waypoint_Detector_LayerMask;
    [SerializeField] private Transform Waypoint_Bag;

    /* --- Debug --- */
    [Header("Debug")]
    [SerializeField] private bool Debug_jump = false;
    [SerializeField] private bool Debug_Waypoint_Detector = false;
    [SerializeField] private bool Debug_Enemy_Detector = false;

    /* --- Time --- */
    [Header("Time")]
    [SerializeField] private float _Timer = 0.0f;
    [SerializeField] private float Time_Collectible_Add = 0.0f;





    /************************************  Setup  ************************************/


    void Awake() {

        _player_manager = new Controls_Player();
        _Run = _player_manager.Player.Run;
        _Jump = _player_manager.Player.Jump;
        _Pause = _player_manager.Player.Pause;

        Coin_Amount = 0;

       
    }

    private void OnEnable() {

        _rigidbody = this.GetComponent<Rigidbody2D>();
        _Animator = this.GetComponent<Animator>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();

        _Run.Enable();
        _Jump.Enable();
        _Pause.Enable();

        _Jump.performed += On_Jump;
        _Jump.canceled += Cancel_Jump;
        _Run.performed += On_Run;
        _Pause.performed += OnPause;

        Ground_Check_Position = transform.Find("Overlap_Jump").GetComponent<Transform>();
    }


    private void OnDisable() {

        _Run.Disable();
        _Jump.Disable();

        _Jump.performed -= On_Jump;
        _Run.performed -= On_Run;

    }


    /************************************  Time  ************************************/

    private void Time_manager() {

        if (!Freeze) {
            _Timer -= Time.deltaTime;
        }
        

        if (_Timer <= 0 ) {
            Pause_Menue.GetComponent<Script_Menu>().Start_Menue();
        }
    }

    private void Time_Add() {
        
        _Timer += Time_Collectible_Add;
    }

    public float Get_Time() {

        return _Timer;
    }

    /************************************  Update  ************************************/


    private void FixedUpdate() {

        if (!Freeze)
        {
            Movement_Update();
            Fall_Manager();
            Caracter_Orientation();

            Ground_detection();
            Enemy_Detection();
            Waypoint_Detection();
            Time_manager();
            Pause_Menue.GetComponent<Script_Menu>().UptadeTime();
        }
       
    }


    /************************************  Debug  ************************************/


    private void OnDrawGizmos() {
        
        if (Debug_jump) {

            if (_IsGrounded) {

                Gizmos.color = Color.green;

            } else {

                Gizmos.color = Color.red;
            }

            Gizmos.DrawWireSphere(Ground_Check_Position.position, Ground_Check_Radius);
        }


        if (Debug_Waypoint_Detector) {

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(Waypoint_Detector_position.position, Waypoint_Detector_Radius);
        }


        if (Debug_Enemy_Detector) {


            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(Enemy_Detector_position.position, Enemy_Detector_Radius);
        }

    }

    /************************************  Camera  ************************************/

    public void SwitchCamera()
    {
        Camera.m_Lens.OrthographicSize = 105.3f;
    }

    /************************************  Mouvement  ************************************/


    private void Movement_Update() {

        Input_Direction = _Run.ReadValue<Vector2>();

        Vector2 Player_Velocity = _rigidbody.velocity;
        Player_Velocity.x = ((Movement_Speed * Time.deltaTime) * Input_Direction.x);

        _rigidbody.velocity = Player_Velocity;

    }

    private void On_Run(InputAction.CallbackContext context) {

        _Animator.SetBool("B_Run", true);
    }

    private void Caracter_Orientation()
    {
        if (_rigidbody.velocity.x > 0) {

            _spriteRenderer.flipX = false;

        }
        else if (_rigidbody.velocity.x < 0) {

            _spriteRenderer.flipX = true;
        }

        if (_rigidbody.velocity.x == 0) {

            _Animator.SetBool("B_Run", false);
        }

    }

    public void OnFreeze() {
        Freeze = true;
        _rigidbody.velocity = new Vector2(0, 0);
    }

    public void OffFreeze() {
        Freeze = false;
    }

    /************************************  Jump  ************************************/


    private void On_Jump(InputAction.CallbackContext context) {

        Debug.Log("il saute :)");

        if (_IsGrounded && _rigidbody.velocity.y < 0.05) {

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (Jump_Height * Time.deltaTime));
            // lancer annimation saut
        } 
            
    }

    private void Cancel_Jump(InputAction.CallbackContext context) {

        if (_rigidbody.velocity.y > 0.01) {

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (_rigidbody.velocity.y*0.5f));
        }
    }


    /************************************  Collectible  ************************************/


    private void Add_Collectible() {

        Time_Add();

        //jouer le son des piece
    }


    /************************************  Respawn  ************************************/


    public void Respawn() {

        // trigger animation death

        _rigidbody.position = Waypoint_Bag.position;
        _Animator.ResetTrigger("T_Death");
        // trigger animation Respawn
    }

    private void DeathAnimation()
    {
        _Animator.SetBool("B_Run", false);
        _Animator.SetBool("B_Jump", false);
        _Animator.SetTrigger("T_Death");

    }



    private void Change_Waypoint(Transform Target) {

        Waypoint_Bag = Target;

    }


    /************************************  detection  ************************************/


    /* --- Ground --- */
    private void Ground_detection() {

        Collider2D[] Ground_detection = Physics2D.OverlapCircleAll(Ground_Check_Position.position, Ground_Check_Radius, Ground_Check_Layer);

        _IsGrounded = false;
        
        foreach (var Object in Ground_detection) {         

            if ( Object.tag == "Platform") {

                _IsGrounded = true;
            }
        }
    }


    /* --- Enemy --- */
    private void Enemy_Detection() {

        Collider2D[] Enemy_Detector = Physics2D.OverlapCircleAll(Enemy_Detector_position.position, Enemy_Detector_Radius, Enemy_Detector_LayerMask);

        foreach (var Object in Enemy_Detector) {

            if (Object.tag == "deathbox") {

                DeathAnimation();
            }
        }
    }

    /* --- Coin --- */
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Coin")) {

            Add_Collectible();
        }

    }

    /* --- Waypoint --- */
    private void Waypoint_Detection() {

        Collider2D[] Waypoint_Catcher = Physics2D.OverlapCircleAll(Waypoint_Detector_position.position, Waypoint_Detector_Radius, Waypoint_Detector_LayerMask);

        foreach (var Object in Waypoint_Catcher) {

            if (Object.tag == "Waypoint") {

                Change_Waypoint(Object.transform);
            }
        }

    }


    /************************************  Fall  ************************************/


    private void Fall_Manager() {

        if (_rigidbody.velocity.y < Fall_MaxSpeed) {

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Fall_MaxSpeed);
        }

        if (!_IsGrounded)
        {

            _Animator.SetBool("B_Jump", true);

        }
        else
        {

            _Animator.SetBool("B_Jump", false);
        }

    }



    /************************************  Pause  ************************************/
    private void OnPause(InputAction.CallbackContext context)
    {
        Pause_Menue.GetComponent<Script_Menu>().Pause_Game();
    }

}


