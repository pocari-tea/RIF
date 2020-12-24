using System;
using System.Collections;
using System.Net;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Login
{
    public class Servermanager: MonoBehaviour
    {

        [Header("LoginPanel")] 
        [SerializeField]
        private InputField IDInputField;
        [SerializeField]
        private InputField PWInputField;

        [Header("RegisterPanel")] 
        [SerializeField]
        private InputField NewIDInputField;
        [SerializeField]
        private InputField NewNickInputField;
        [SerializeField] 
        private InputField NewPWInputField;
    
        [Header("SetActive")] 
        [SerializeField] private GameObject OPanel;
        [SerializeField] private GameObject CPanel;
        [SerializeField] private GameObject LogErrPanel;
        [SerializeField] private GameObject RegErrPanel;

        private IPAddress[] addr;

        private void Start()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            addr = ipEntry.AddressList;
        }


        public void RegisterBtn()
        {
            StartCoroutine(RegisterCo());
        }

        private IEnumerator RegisterCo()
        {
            MySqlConnection conn = new MySqlConnection("server=" + addr[1] + "; port=3306; Database=software; uid=root; pwd=123; CharSet=utf8;");
            try
            {
                // 2. DB 열기
                conn.Open();
            
                // 3. Insert 쿼리 실행
                string query = "INSERT INTO game values ('" + NewIDInputField.text + "', '" + NewPWInputField.text + "', '" + NewNickInputField.text + "', 1, 1, 0, 0, 0, 0);";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader Reader = cmd.ExecuteReader();
                
                StartCoroutine(XCo());
            }
            catch (Exception ex)
            {
                StartCoroutine(RegErr());
                Debug.Log(ex.Message);
            }

            yield return null;
        }

        public void LoginBtn()
        {
            StartCoroutine(LoginCo());
        }

        private IEnumerator LoginCo()
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
                string query = "select Nick from game where ID = '" + IDInputField.text + "' and PW = '" + PWInputField.text + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // 4. 쿼리 결과 가져오기
                MySqlDataReader Reader = cmd.ExecuteReader();

                if (Reader.Read())
                {
                    User.GetTest(IDInputField.text);
                    SceneLoader.LoadScene("Lobby");
                }
                else
                {
                    StartCoroutine(LogErr());
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            yield return null;
        }
        
        private IEnumerator RegErr()
        {
            RegErrPanel.SetActive(true);

            yield return null;
        }

        private IEnumerator LogErr()
        {
            LogErrPanel.SetActive(true);

            yield return null;
        }

        private IEnumerator XCo()
        {
            OPanel.SetActive(true);
            CPanel.SetActive(false);
        
            yield return null;
        }
    }
}

