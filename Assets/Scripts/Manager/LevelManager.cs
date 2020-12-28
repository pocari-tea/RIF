using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private int maxExp = 100;
    public int exp;
    private int level;
    
    private IPAddress[] addr;

    void Start()
    {
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        addr = ipEntry.AddressList;
        
        instance = this;
    }


    public void Exp(int temp_exp)
    {
        MySqlConnection conn = new MySqlConnection("server=" + addr[1] + "; port=3306; Database=software; uid=root; pwd=123; CharSet=utf8;");
        try
        {
            // 2. DB 열기
            conn.Open();
            
            // select로 경험치 찾기
            string query1 = "select Lv, EXP from game where ID = '" + User.SetTest() + "';";
            MySqlCommand cmd = new MySqlCommand(query1, conn);

            // 4. 쿼리 결과 가져오기
            MySqlDataReader Reader1 = cmd.ExecuteReader();

            if(Reader1.Read())
            {
                level = int.Parse(Reader1.GetValue(0).ToString());
                exp = int.Parse(Reader1.GetValue(1).ToString());
                
                exp += temp_exp;
                
                if (exp >= maxExp)
                {
                    for (int i = 0; i < exp / maxExp; i++)
                    {
                        level += 1;
                    }
                    exp = exp % maxExp;
                }
            }
            
            Reader1.Close();
            
            // update 쿼리 실행
            string query = "UPDATE game SET Lv = '" + level + "', EXP = '" + exp + "' where ID = '" + User.SetTest() + "';";
            
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

