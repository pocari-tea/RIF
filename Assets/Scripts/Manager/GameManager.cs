﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField]
    private Text gameStartTimeT;

    [SerializeField]
    private GameObject gmaeStartPanel;

    public bool gameStart;

    private float gamePlayTime;
    [SerializeField]
    private Text gameTimer;

    [SerializeField]
    private Text scoreT;

    [SerializeField]
    private Text ingameCoinT;

    [SerializeField]
    private Text resultScoreT;

    [SerializeField]
    private GameObject gameEndPanel;

    private int score;
    private int ingameCoin;
    private int temp_exp;

    private bool endGame = false;

    void Start()
    {
        instance = this;
        //EndGame();
    }

    void Update()
    {
        gamePlayTime += Time.deltaTime;

        if(!gameStart)
        {
            gameStartTimeT.text = "" + Mathf.Round(3 - gamePlayTime);
        }

        else
        {
            gmaeStartPanel.SetActive(false);
            gameTimer.text = "" + Mathf.Round(gamePlayTime);
            ingameCoinT.text = string.Format("{0}", ingameCoin);
            scoreT.text = string.Format("{0}", score);
            StartCoroutine("SurvivalScore");
        }

        if (gamePlayTime > 3f)
        {
            gamePlayTime = 0f;
            gameStart = true;
        }
    }

    public void BadHit()
    {
        Debug.Log("bad");
    }

    public void NormalHit()
    {
        score += 100;
    }

    public void PerfectHit()
    {
        score += 200;
    }

    public void SumCoin()
    {
        ingameCoin += 10;
        score += 100;
    }

    public void EndGame()
    {

        endGame = true;
        gameEndPanel.gameObject.SetActive(true);
        resultScoreT.text = string.Format("{0}", score);
        StopCoroutine("SurvivalScore");
        BestScoreManager.instance.Score(score);
    
        temp_exp = score / 100;
        LevelManager.instance.Exp(temp_exp);
        TotalCoinManager.instance.Coin(ingameCoin);
        //Time.timeScale = 0f;
    }


    IEnumerator SurvivalScore()
    {
        yield return new WaitForSeconds(0.25f);
        if(!endGame)
        {
            StartCoroutine("SurvivalScore");
        }
          
    }

}
