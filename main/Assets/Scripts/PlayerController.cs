using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	private float moveInput;

	private Rigidbody2D rb;

	private bool facingRight = true;

	private bool isGrounded;
	public Transform feetPos;
	public float checkRadius;
	public LayerMask whatIsGround;

	private int extraJumps;
	public int extraJumpsValue;
	
	private Animator anim;

	private void Start()
	{
		extraJumps = extraJumpsValue;
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}


 
	private void FixedUpdate()
	{
		moveInput = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
		if(facingRight == false && moveInput > 0)
		{
			Flip();
		}
		else if(facingRight == true && moveInput < 0)
		{
			Flip();
		}

		if (moveInput == 0){
			anim.SetBool("isRunning", false);
		} else {
			anim.SetBool("isRunning", true);
		}

	}
	private void Update()
	{
		isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

		if (isGrounded == true) {
			extraJumps = extraJumpsValue;
		}

		if (extraJumps > 0 && Input.GetKeyDown(KeyCode.Space)) {
				rb.velocity = Vector2.up * jumpForce;
				extraJumps--;
				anim.SetTrigger("takeOff");
			} else {if(isGrounded == true && extraJumps == 0 && Input.GetKeyDown(KeyCode.Space))
		{
			rb.velocity = Vector2.up * jumpForce;
			anim.SetTrigger("takeOff");
		}}
		if (isGrounded == true) {
			anim.SetBool("isJumping", false);
		} else {
			anim.SetBool("isJumping", true);
		}
	}

	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}
}
