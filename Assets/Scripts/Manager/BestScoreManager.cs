using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreManager : MonoBehaviour
{
    public static BestScoreManager instance;

    public int bestScore;
    
    private IPAddress[] addr;
    void Start()
    {
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        addr = ipEntry.AddressList;
        
        instance = this;
    }

    public void Score(int score)
    {
        MySqlConnection conn = new MySqlConnection("server=" + addr[1] + "; port=3306; Database=software; uid=root; pwd=123; CharSet=utf8;");
        try
        {
            // DB 열기
            conn.Open();
            
            // select로 하이스코어 찾기
            string query1 = "select High_Score from game where ID = '" + User.SetTest() + "';";
            MySqlCommand cmd = new MySqlCommand(query1, conn);

            // 4. 쿼리 결과 가져오기
            MySqlDataReader Reader1 = cmd.ExecuteReader();
            
            if(Reader1.Read())
            {
                bestScore = int.Parse(Reader1.GetValue(0).ToString());
                if (bestScore < score)
                {
                    bestScore = score;
                }
            }
            
            Reader1.Close();

            // update 쿼리로 하이스코어 갱신
            string query = "UPDATE game SET High_Score = '" + bestScore + "' where ID = '" + User.SetTest() + "';";
                
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
