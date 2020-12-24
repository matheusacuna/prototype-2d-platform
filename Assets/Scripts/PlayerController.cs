using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public float jumpForce;

    public bool isJump;
    public bool doubleJump;

    public AudioSource soundJump;

    private Rigidbody2D rig;
    private Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        soundJump = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       Move(); 
       Jump();
    }

    void Move()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //move o personagem em uma posicao
        //transform.position += movement * Time.deltaTime * Speed;

        float movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

        if(movement > 0)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(movement < 0f)
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(movement == 0)
        {
            anim.SetBool("Walk", false);
        }
        
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            soundJump.Play();

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJump = false;
            anim.SetBool("Jump", false);
        }

        if(collision.gameObject.tag == "Spikes")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJump = true;
            anim.SetBool("Jump", true);
        }
    }

}
