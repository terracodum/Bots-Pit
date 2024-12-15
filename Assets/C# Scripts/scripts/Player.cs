using System;
using System.Collections;
using UnityEngine;


public class Player : Sounds
{
    public Rigidbody rb;
    
    public static float speed = 1.0f;
    public static bool Moving = false;

   [SerializeField] private float _smoothing = 3f;

    
    [SerializeField] float LeftLim;
    [SerializeField] float RightLim;
    [SerializeField] float BottomLim;
    [SerializeField] float UpperLim;
    [Header("Anim SET")]
    public Animator anim;
    public static bool CanMove = true, _CanKick = false;
    
    string name;

    void Start()
    {
        CanMove = true;
        

    }
    void FixedUpdate()
    {
        Move();
        Kick();
    }
    private void Move()
    {

        speed = Input.GetKey(KeyCode.LeftShift) ? 0.4f : 1.5f;
        speed *= CanMove ? 1f : 0f;
        
            if (Input.GetKey(KeyCode.S))
        {

           
            transform.Translate(-Vector3.up * Time.deltaTime * _smoothing * speed);
            
        }
        if (Input.GetKey(KeyCode.W))
        {

           
            transform.Translate(Vector3.up * Time.deltaTime * _smoothing * speed);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Duration", false);
            
            transform.Translate(Vector3.right * Time.deltaTime * _smoothing * speed);


        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Duration", true);
            
            transform.Translate(-Vector3.right * Time.deltaTime * _smoothing * speed);
            

        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            Step_sound_play();
            anim.SetBool("move", true);
        }else anim.SetBool("move", false);
        Vector3 dirrection = new Vector3
            (
            Mathf.Clamp(transform.position.x, LeftLim, RightLim),
            Mathf.Clamp(transform.position.y, BottomLim, UpperLim),
            transform.position.z
            );
        transform.position = dirrection;
        
        
        
    }
        // PlayerMove
        private void Kick()
        {
            if (name != null)
            {
                GameObject box = GameObject.Find(name);
                Rigidbody rdBX = box.GetComponent<Rigidbody>();
                if (Input.GetKey(KeyCode.R) && _CanKick)
                {
                    if (rb.position.x - rdBX.position.x < 0)
                    {
                        rdBX.linearVelocity = Vector3.right * 5;
                        
                    }
                    else
                    {
                        rdBX.linearVelocity = Vector3.left * 5;
                        
                    }
                    StartCoroutine(SlowMove());
                }
                if (Math.Abs(rdBX.position.x )>= RightLim)
                {
                    rdBX.linearVelocity = Vector3.right * 0;
                    rdBX.transform.position = new Vector3(-1f, -1.5f, -1f);
                    name = null;
                }
            }

           
    } // KickObjects
    
   
    IEnumerator SlowMove()
    {

        yield return new WaitForSeconds(0.1f);
        
        yield return new WaitForSeconds(1f);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            name = other.name;

            _CanKick = true;
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
            _CanKick = false;
    }

   
}