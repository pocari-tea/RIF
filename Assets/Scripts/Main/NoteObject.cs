using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteObject : MonoBehaviour
{
    //RhythmManager rhythmManager;

    private int checkColor;
    private bool canBePressed;
    int count;

    public PlayerController playerController;
    public Enemy enemy;
    public GameObject goodEffect, perfectEffect, badEffect;


    public float beatTempo;

    private Button btn;

    void Start()
    {
        if(gameObject.tag == "Yellow")
        {
            btn = GameObject.Find("YellowBtn").GetComponent<Button>();
            btn.onClick.AddListener(DestroyYellow);
        }         
        else if (gameObject.tag == "Green")
        {
            btn = GameObject.Find("GreenBtn").GetComponent<Button>();
            btn.onClick.AddListener(DestroyGreen);
        }
            

        beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        print(Mathf.Abs(transform.position.x));
        //Check();
        //print(Mathf.Abs(transform.position.x));
        transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
    }

    void TimingCheck()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
        checkColor = 0;


        if (Mathf.Abs(transform.position.x) > 0.3)
        {
            GameManager.instance.BadHit();
            enemy.Attack();
            print("이1-1");
            GameObject temp = Instantiate(badEffect, transform.position, badEffect.transform.rotation);
            Destroy(temp, 0.5f);
            print("이1-2");
        }
        else if (Mathf.Abs(transform.position.x) > 0.15)
        {
            GameManager.instance.NormalHit();
            print("이2-1");
            //playerController.Attack();
            print("이2-2");
            GameObject temp = Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
            Destroy(temp, 0.5f);
            print("이2-3");
            //Destroy(normalEffect, 1f);
        }
        else
        {
            GameManager.instance.PerfectHit();
            print("이3-1");
           // playerController.Attack();
            print("이3-2");
            GameObject temp = Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            Destroy(temp, 0.5f);
            print("이3-3");

            //Destroy(perfectEffect, 1f);
        }





    }

    public void DestroyYellow()
    {
        checkColor = 1;
        Check();
        Invoke("Resetting", 0.01f);
    }

    public void DestroyGreen()
    {
        checkColor = 2;
        Check();
        Invoke("Resetting", 0.01f);
    }


    void Check()
    {
        print("--------");
        print(gameObject.tag);
        print(canBePressed);
        print(Mathf.Abs(transform.position.x));
        print(checkColor);
        print("--------");
        if (gameObject.tag == "Yellow" && canBePressed)
        {
            print("노란 버튼 클릭됨");

            if (checkColor == 1)
            {
                TimingCheck();
            }

        }
        else if (gameObject.tag == "Green" && canBePressed)
        {
            print("초록 버튼 클릭");
            if (checkColor == 2)
            {
                TimingCheck();

            }
        }
    }

    void Resetting()
    {
        checkColor = 0;
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Activator")
        {
            print("canBePressed2" + canBePressed);
            canBePressed = true;
            print("canBePressed3"+ canBePressed);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator" && (transform.position.x < -0.4))
        {
            print("22");
            canBePressed = false;
            GameManager.instance.BadHit();
            GameObject temp = Instantiate(badEffect, transform.position, badEffect.transform.rotation);
            Destroy(temp, 0.5f);
            enemy.Attack();

            gameObject.SetActive(false);

        }
    }
}

