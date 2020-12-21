using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCheck : MonoBehaviour
{
    public PlayerController playerController;
    public MapMove mapMove;
    GameObject rhythm;
    GameObject rhythmBtn;
    GameObject jumpBtn;
  

    private void Start()
    {
        rhythm = GameObject.Find("Rhythm").transform.Find("Main").gameObject;
        rhythmBtn = GameObject.Find("Canvas").transform.Find("Buttons").gameObject;
        jumpBtn = GameObject.Find("Canvas").transform.Find("JumpBtn").gameObject;
      
    }

    public RhythmMove rhythmMove;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            //playerController.StopRun();
            mapMove.Stop();
            //rhythmMove.StopRun();
            Instantiate();
            print(rhythm);
            rhythm.SetActive(true);
            rhythmBtn.SetActive(true);
            jumpBtn.SetActive(false);

        }
    }

    public void Restart()
    {
        //playerController.StartRun();
        mapMove.Start();
        //rhythmMove.StartRun();
        rhythm.SetActive(false);
        rhythmBtn.SetActive(false);
        jumpBtn.SetActive(true);
    }


    int count;


    [SerializeField]
    private GameObject[] notes;

    public Transform beat;

    public void Instantiate()
    {
        int ranGameObj = Random.Range(0, 2);
        float ranTime = Random.Range(0.5f, 1.8f);
        count++;
        Instantiate(notes[ranGameObj], beat);

        if (count == 4)
        {
            count = 0;
            Invoke("Restart", 4f);
            return;
        }

        Invoke("Instantiate", ranTime);
    }

}
