using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hunter : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    [SerializeField] private int bulletsQuantity = 100;

    [SerializeField] private Bullet bullet;


    private const int FIELD_SIZE = 100;


    private Rigidbody2D rb;

    private int bulletsLeft;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletsLeft = bulletsQuantity;
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (bulletsLeft != 0)
            {
                Bullet newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.Move((spawnPoint - (Vector2)transform.position).normalized);
                --bulletsLeft;
            }
        }
    }

    private void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rb.velocity = movement * speed;

        Camera.main.GetComponent<Rigidbody2D>().velocity = rb.velocity;
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
        bulletsLeft = bulletsQuantity;
        transform.position = new Vector2(UnityEngine.Random.Range(-FIELD_SIZE / 2 + 5, FIELD_SIZE / 2 - 5), UnityEngine.Random.Range(-FIELD_SIZE / 2 + 5, FIELD_SIZE / 2 - 5));
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void Update()
    {
        Move();
        LookInDirection();
        Shoot();

    }
}
