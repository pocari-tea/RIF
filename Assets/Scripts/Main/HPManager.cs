using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPManager : MonoBehaviour
{
    public static HPManager instance;

    private Image healthI;

    [SerializeField]
    private float lerpSpeed;

    private float currentFill = 100f;
    public float health = 100f;
    public float maxHelath = 100f;          // 최대 hp
   

    void Start()
    {
        instance = this;
        healthI = GetComponent<Image>();
        StartCoroutine("HP");
    }

    IEnumerator HP()
    {
        health -= 0.5f;
        yield return new WaitForSeconds(1f);
        StartCoroutine("HP");
    }
  

    // Update is called once per frame
    void Update()
    {
        healthI.fillAmount = health * 0.01f;
    }
}

//hPBar.value -= minusHp * Time.deltaTime;