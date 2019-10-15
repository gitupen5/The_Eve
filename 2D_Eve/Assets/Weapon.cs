﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0f;
    public float Damage = 10f;
    public LayerMask whatToHit;

    float timeToFire = 0f;
    Transform firePoint;


    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("No Fire Point");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1")){
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        
    }

    void Shoot()
    {

        //Assigning the ScreenToWorldPoint to x and ScreenToWorldPoint to y
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y );
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + "and did " + Damage + " damage.");

        }

    }
}
