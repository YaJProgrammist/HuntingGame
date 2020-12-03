﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hunter : MonoBehaviour
{
    [SerializeField] private float speed = 8;

    [SerializeField] private int bulletsLeft = 100;

    [SerializeField] private Bullet bullet;

    const float MAX_WEAPON_RADIUS = 25;

    private Rigidbody2D rigidbody;

    private Vector2 direction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("got target");
                var selectedPoint = hit.transform.position;
                MakeShot(selectedPoint);
            }
                
        }
    }

    private void MakeShot(Vector2 spawnPoint)
    {
        if (bulletsLeft != 0)
        {
            Debug.Log("bullet spawned");
            Vector2 bulletEndPoint = spawnPoint;
            //if (Vector2.Dot(transform.position, spawnPoint) > MAX_WEAPON_RADIUS)
            //{
            //    bulletEndPoint = spawnPoint.normalized * MAX_WEAPON_RADIUS;
            //}

            Instantiate(bullet, bulletEndPoint, Quaternion.identity);
            --bulletsLeft;
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.velocity = Vector2.up * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.velocity = Vector2.down * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = Vector2.left * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = Vector2.right * speed;
        }

        Camera.main.GetComponent<Rigidbody2D>().velocity = rigidbody.velocity;
    }

    private void LookInDirection()
    {
        Vector2 direction = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2).normalized;
        this.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wolf")
        {
            Die();
        }
    }

    private void Die()
    {

    }

    private void Update()
    {
        Move();
        LookInDirection();
        Shoot();

    }
}
