using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField]
    private Text expT;

    [SerializeField]
    private Image expI;

    private int maxExp = 100;
    public int exp;
    private int level = 1;

    void Start()
    {
        instance = this;
    }


    public void Exp(int temp)
    {
        exp += temp;
        if (exp >= maxExp)
        {
            level +=1;
            expT.text = string.Format("{0}", level);
            exp = maxExp - 100;
        }
        
        MySqlConnection conn = new MySqlConnection("server=10.120.73.28; port=3306; Database=software; uid=root; pwd=123; CharSet=utf8;");
        try
        {
            // 2. DB 열기
            conn.Open();
            
            // 3. Insert 쿼리 실행
            string query = "UPDATE game SET Lv = '" + level + "', EXP = '" + exp + "' + where ID = '" + User.SetTest() + "';";
                
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader Reader = cmd.ExecuteReader();

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }


}

