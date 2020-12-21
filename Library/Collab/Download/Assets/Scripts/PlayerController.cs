using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
	public float initHpMax = 20.0f;
	public Rigidbody2D myRigid;
	public int jumpCount = 0;

	
	private float jumpStartTime = 0.0f;
	[SerializeField]
	private float jumpForce;

	protected override void Awake()
	{
		base.Awake();
	}

    private void Start()
    {
		//myRigid = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {	
		HandleInput();
		print(jumpCount);
		//if (myRigid.velocity.y > 0)
		//{
		//	animator.SetBool("Jump", true);
		//}
	}

	public void ActionJump()
	{
		if(jumpCount == 0)
        {
			animator.SetBool("Jump",true);
			myRigid.velocity = Vector2.up * jumpForce;
			jumpCount++;
		}
		else if ( jumpCount == 1)
        {
			animator.Play("Jump");
			myRigid.velocity = Vector2.up * jumpForce;
			jumpCount++;
		}
		

	}

	

	private void HandleInput()
    {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			print("점프");
			ActionJump();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Road"))
		{
			animator.SetBool("Jump", false);
			jumpCount = 0;
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Item"))
		{
			animator.SetLayerWeight(1, 1);
		}
	}

}
