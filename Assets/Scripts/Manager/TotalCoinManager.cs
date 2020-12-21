using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;


public class TotalCoinManager : MonoBehaviour
{
    public static TotalCoinManager instance;

    [SerializeField]
    private Text totalCoinT;

    public int totalCoin;

    void Start()
    {
        instance = this;
        totalCoinT.text = string.Format("{0}", totalCoin);
    }

    public void Coin(int coin)
    {
        totalCoin += coin;
        totalCoinT.text = string.Format("{0}", totalCoin);       
        
        MySqlConnection conn = new MySqlConnection("server=192.168.0.37; port=3306; Database=software; uid=root; pwd=123; CharSet=utf8;");
        try
        {
            // 2. DB 열기
            conn.Open();
            
            // 3. Insert 쿼리 실행
            string query = "UPDATE game SET Money = '" + coin + "' + where ID = '" + User.SetTest() + "';";
                
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader Reader = cmd.ExecuteReader();

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
