using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMove : MonoBehaviour
{
	private float speed = 2.5f;
	void Update()
    {
		if (GameManager.instance.gameStart)
			transform.Translate(speed * Time.deltaTime, 0, 0);
    }

	public void StopRun()
	{
		speed = 0f;

	}

	public void StartRun()
	{
		speed = 2.5f;
	} 
}
