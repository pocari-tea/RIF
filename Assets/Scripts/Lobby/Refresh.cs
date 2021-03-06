﻿using System;
using System.Collections;
using System.Net;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Refresh : MonoBehaviour
{
    [SerializeField] private Text Nick;
    [SerializeField] private Text LvText;
    [SerializeField] private Image EXP;
    [SerializeField] private Text High_Score;
    [SerializeField] private Text Money;
    [SerializeField] private Text Cash;
    
    private IPAddress[] addr;

    private void Start()
    {
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        addr = ipEntry.AddressList;
        
        StartCoroutine(RefreshCo());
    }

    private IEnumerator RefreshCo()
    {
        MySqlConnection conn = new MySqlConnection("server=" + addr[1] + "; port=3306; Database=software; uid=root; pwd=123; CharSet=utf8;");
        try
        {
            // 2. DB 열기
            conn.Open();
            
            string setcharset = "SET NAMES 'utf8'";	// <-- !!
            MySqlCommand charsetcmd = new MySqlCommand(setcharset, conn);
            MySqlDataReader charsetrdr = charsetcmd.ExecuteReader();
            charsetrdr.Close();

            // 3. SELECT 쿼리 실행
            string query = "select Nick, Lv, EXP, High_Score, Money, Cash from game where ID = '" + User.SetTest() + "';";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            // 4. 쿼리 결과 가져오기
            MySqlDataReader Reader = cmd.ExecuteReader();

            if (Reader.Read())
            {
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    if (i == 0)
                    {
                        Nick.text = Reader.GetValue(i).ToString();
                    }
                    else if (i == 1)
                    {
                        LvText.text = Reader.GetValue(i).ToString();
                    }
                    else if (i == 2)
                    {
                        EXP.fillAmount = float.Parse(Reader.GetValue(i).ToString()) / 100f;
                    }
                    else if (i == 3)
                    {
                        High_Score.text = Reader.GetValue(i).ToString();
                    }
                    else if (i == 4)
                    {
                        Money.text = Reader.GetValue(i).ToString();
                    }
                    else
                    {
                        Cash.text = Reader.GetValue(i).ToString();
                    }
                }
            }
            
            Reader.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        yield return null;
    }
}
