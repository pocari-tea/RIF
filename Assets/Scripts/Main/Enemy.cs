using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float distance;
    public float atkDistance;
    public LayerMask isLayer;

    public GameObject bullet;
    public Transform pos;

    public float cooltime;
    private float currenttime;



    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        //if(raycast.collider != null)
        //{
        //    //currenttime = 0;
        //    if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
        //    {
        //        if (currenttime <= 0)
        //        {
        //            GameObject bulletcopy = Instantiate(bullet, pos.position, transform.rotation);

        //            currenttime = cooltime;
        //        }

        //    }

        //    currenttime -= Time.deltaTime;
        //}



    }
    public void Attack()
    {
        GameObject bulletcopy = Instantiate(bullet, pos.position, transform.rotation);
        Destroy(bulletcopy, 3f);
    }


}
