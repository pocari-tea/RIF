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

    [SerializeField]
    private Text bestScoreT;

    public int bestScore;
    
    private IPAddress[] addr;
    void Start()
    {
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        addr = ipEntry.AddressList;
        
        instance = this;
    }

    private void FixedUpdate()
    {
        bestScoreT.text = string.Format("{0}", bestScore);
    }

    public void Score(int score)
    {
        if(bestScore < score)
        {
            bestScore = score;
            bestScoreT.text = string.Format("{0}", bestScore);
        }
        
        MySqlConnection conn = new MySqlConnection("server=" + addr[1] + "; port=3306; Database=software; uid=root; pwd=123; CharSet=utf8;");
        try
        {
            // 2. DB 열기
            conn.Open();
            
            // 3. Insert 쿼리 실행
            string query = "UPDATE game SET High_Score = '" + bestScore + "' + where ID = '" + User.SetTest() + "';";
                
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader Reader = cmd.ExecuteReader();

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }
}
