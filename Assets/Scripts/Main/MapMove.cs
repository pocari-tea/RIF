using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    float mapSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameStart)
             transform.Translate(-mapSpeed * Time.deltaTime, 0, 0);
    }

    public void Stop()
    {
        mapSpeed = 0f;
    }


    public void Start()
    {
        mapSpeed = 3f;
    }
}
