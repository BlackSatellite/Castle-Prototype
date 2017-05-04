using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveVelocity; // fixing problem with gliding (Physics2D material added)
	public float jumpHeigh;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	private bool doubleJumped;
	private bool facingRight;

	private Animator anim;

	public Transform firePoint;
	public GameObject Missle_1;

	public float shotDelay;
	private float shotDelayCounter;
	//private float moveVelocity;

	private Rigidbody2D myRigidBody2D;

	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;

// Use this for initialization
//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------
	void Start () {

		anim = GetComponent<Animator> ();

		myRigidBody2D = GetComponent<Rigidbody2D> ();

		gravityStore = myRigidBody2D.gravityScale;
	}
//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------

	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

	}
//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------

	// Update is called once per frame
	void Update (){



		if (grounded) { 
			doubleJumped = false;
		}

		anim.SetBool ("Grounded", grounded);


		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			Jump ();
			 
		}

		if (Input.GetKeyDown (KeyCode.Space) && !doubleJumped && !grounded) {
			Jump ();
			doubleJumped = true;
			
		}

		moveVelocity = 0f;

		if (Input.GetKey (KeyCode.RightArrow)) {
			//GetComponent<Rigidbody2D>().velocity = new Vector2( moveSpeed, myRigidBody.velocity.y);
			moveVelocity = moveSpeed;
			
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//GetComponent<Rigidbody2D>().velocity = new Vector2( -moveSpeed, myRigidBody.velocity.y);
			moveVelocity = -moveSpeed;
			
		}

		myRigidBody2D.velocity = new Vector2 (moveVelocity, myRigidBody2D.velocity.y); 

		anim.SetFloat ("Speed", Mathf.Abs (myRigidBody2D.velocity.x));
	
		//moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");
		if (myRigidBody2D.velocity.x < 0 && !facingRight) {
			Flip ();
		}
		if (myRigidBody2D.velocity.x > 0 && facingRight) {
			Flip ();
		}
		// putting the missle
		if (Input.GetKeyDown (KeyCode.Z)) 
		{
			Instantiate(Missle_1, firePoint.position, firePoint.rotation);

		}
		//Delay for shooting missles
		if (Input.GetKey (KeyCode.Z)) 
		{
			shotDelayCounter -=Time.deltaTime;

			if(shotDelayCounter <= 0)
			{
				shotDelayCounter = shotDelay;
				Instantiate(Missle_1, firePoint.position, firePoint.rotation);
			}

		}

		//climbing Ladder
		if (onLadder ) 
		{	
			myRigidBody2D.gravityScale = 0f;

			climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");

			myRigidBody2D.velocity = new Vector2 (myRigidBody2D.velocity.x, climbVelocity);

			Physics2D.IgnoreLayerCollision (8, 9, true); // Stupid way to go up through level colliders (turn on)
		}
		if (!onLadder || grounded) 
		{
			myRigidBody2D.gravityScale = gravityStore;

			Physics2D.IgnoreLayerCollision (8, 9, false); // Stupid way to go up through level colliders (turn off)
		}

	}
//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------

	// Flip the direction of the Player
	void Flip()
	{	
		Vector3 startingScale = transform.localScale;

		facingRight = !facingRight;
		transform.localScale = new Vector3 (-startingScale.x, startingScale.y, startingScale.z);
	}
//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------

	public void Jump()
	{
		myRigidBody2D.velocity = new Vector2( myRigidBody2D.velocity.x, jumpHeigh);
	}
}
