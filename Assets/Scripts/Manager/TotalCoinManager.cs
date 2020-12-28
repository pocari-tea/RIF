using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;


public class TotalCoinManager : MonoBehaviour
{
    public static TotalCoinManager instance;

    public int totalCoin;
    
    private IPAddress[] addr;
    void Start()
    {
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        addr = ipEntry.AddressList;
        
        instance = this;
    }

    public void Coin(int coin)
    {
        MySqlConnection conn = new MySqlConnection("server=" + addr[1] + "; port=3306; Database=software; uid=root; pwd=123; CharSet=utf8;");
        try
        {
            // 2. DB 열기
            conn.Open();
            
            // select로 보유 코인 찾기
            string query1 = "select Money from game where ID = '" + User.SetTest() + "';";
            MySqlCommand cmd = new MySqlCommand(query1, conn);

            // 4. 쿼리 결과 가져오기
            MySqlDataReader Reader1 = cmd.ExecuteReader();

            if(Reader1.Read())
            {
                totalCoin = int.Parse(Reader1.GetValue(0).ToString());

                totalCoin += coin;
            }
            
            Reader1.Close();
            
            // 3. update 쿼리로 코인 추가
            string query = "UPDATE game SET Money = '" + totalCoin + "' where ID = '" + User.SetTest() + "';";
            
            cmd = new MySqlCommand(query, conn);

            MySqlDataReader Reader = cmd.ExecuteReader();

            Reader.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
