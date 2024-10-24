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
    private readonly Vector3 facingRight = new Vector3(1, 1, 1);
    private readonly Vector3 facingLeft = new Vector3(-1, 1, 1);

    [Header("Graphics references")]
    public GameObject dustPrt;

    public GameObject playCamera;
    
    private Vector2 movement;
    
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        this.enabled = false;
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

    // TODO: Directional input works while airborne...feature?
    void HandleMove()
    {
        // Capture inputs
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        if (horizontal > 0) this.transform.localScale = facingRight;
        else if (horizontal < 0) this.transform.localScale = facingLeft;

        if (horizontal != 0 || vertical != 0)
        {
            animator.Play("Walk");
            dustPrt.SetActive(true);
        }
        else
        {
            animator.Play("Idle");
            dustPrt.SetActive(false);
        }
    }
}
