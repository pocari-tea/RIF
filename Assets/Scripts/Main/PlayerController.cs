using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float initHpMax = 20.0f;
	private Rigidbody2D myRigid;
	private Animator myAnim;
	public int jumpCount = 0;

	[SerializeField]
	private float speed = 2f;

	private float defaultSpeed;

	[SerializeField]
	private float jumpForce;

    private void Start()
    {
		defaultSpeed = speed;
		myRigid = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
	    if (transform.position.y < -3)
	    {
			Die();
		    GameManager.instance.EndGame();

	    }

		if (GameManager.instance.gameStart)
		{
			transform.Translate(speed * Time.deltaTime, 0, 0);
		}
	}

	public void Jump()
	{
		if (GameManager.instance.gameStart)
        {
			if (jumpCount == 0)
			{
				myAnim.SetBool("Jump", true);
				myRigid.velocity = Vector2.up * jumpForce;
				jumpCount++;
			}
			else if (jumpCount == 1)
			{
				myAnim.Play("Jump");
				myRigid.velocity = Vector2.up * jumpForce;
				jumpCount++;
			}
		}		

	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Road"))
		{
			myAnim.SetBool("Jump", false);
			jumpCount = 0;
		}

		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Item"))
		{
			myAnim.SetLayerWeight(1, 1);
			
		}
		if (collision.gameObject.CompareTag("Bullet"))
		{
			print("맞음");
		}

		if (collision.gameObject.CompareTag("Coin"))
		{
			collision.gameObject.SetActive(false);
			GameManager.instance.SumCoin();

		}
	}

	public void StopRun()
    {
		myAnim.SetBool("Idle", true);
		speed = 0f;

	}

	public void StartRun()
	{
		myAnim.SetBool("Idle", false);
		speed = defaultSpeed;
	}

	private void Die()
    {
		gameObject.SetActive(false);
    }



}
