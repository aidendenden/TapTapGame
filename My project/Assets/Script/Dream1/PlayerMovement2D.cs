using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{ 
    [Header("Movement")] [Range(1, 20)] public float moveSpeedMultiplier = 8f;
    [Range(-1f, 1f)] public float horizontal;
    [Range(-1f, 1f)] public float vertical;
    public Animator animator = null;
    public new Rigidbody2D rigidbody;
    public AudioSource audioSource;
    public AudioClip[] clip;
    
    private Vector2 movement;
    
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        
    }

    

    void Update()
    {
        
        if (!Application.isFocused) return;
      
        HandleMove();
   
    }


    private void FixedUpdate()
    {
     
        if (!Application.isFocused) return;
   
        
        movement.x = horizontal;
        movement.y = vertical;
        rigidbody.MovePosition(rigidbody.position + movement.normalized * moveSpeedMultiplier * Time.fixedDeltaTime);
    }

    void HandleMove()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (horizontal > 0)
        {
            animator.SetBool("WalkRight", true);
            animator.SetBool("WalkLeft", false);
        }
        else if (horizontal < 0)
        {
            animator.SetBool("WalkRight", false);
            animator.SetBool("WalkLeft", true);
        }
        else
        {
            animator.SetBool("WalkRight", false);
            animator.SetBool("WalkLeft", false);
        }

        if (vertical > 0)
        {
            animator.SetBool("WalkUp", true);
            animator.SetBool("WalkDown", false);
        }
        else if (vertical < 0)
        {
            animator.SetBool("WalkUp", false);
            animator.SetBool("WalkDown", true);
        }
        else
        {
            animator.SetBool("WalkUp", false);
            animator.SetBool("WalkDown", false);
        }

        if (horizontal != 0 || vertical != 0)
        {
            PlaySound();
        }
        else
        {
            StopPlaySound();
        }
    }

    private void PlaySound()
    {
        if (audioSource.isPlaying) return;
        int index = Random.Range(0, clip.Length - 1);
        audioSource.clip = clip[index];
        audioSource.Play();
    }

    private void StopPlaySound()
    {
        audioSource.Stop();
    }
}
