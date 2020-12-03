using System;
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


    private const float MAX_WEAPON_RADIUS = 25;

    private const int FIELD_SIZE = 100;


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
            Vector2 spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (bulletsLeft != 0)
            {
                Vector2 bulletEndPoint = spawnPoint;
                //if (Vector2.Dot(transform.position, spawnPoint) > MAX_WEAPON_RADIUS)
                //{
                //    bulletEndPoint = spawnPoint.normalized * MAX_WEAPON_RADIUS;
                //}

                Instantiate(bullet, bulletEndPoint, Quaternion.identity);
                --bulletsLeft;
            }
        }
    }

    private void Move()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    rigidbody.velocity = Vector2.up * speed;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    rigidbody.velocity = Vector2.down * speed;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    rigidbody.velocity = Vector2.left * speed;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    rigidbody.velocity = Vector2.right * speed;
        //}

        //if (Input.GetKey(KeyCode.A))
        //    rigidbody.AddForce(Vector2.left);

        //if (Input.GetKey(KeyCode.D))
        //    rigidbody.AddForce(Vector2.right);

        //if (Input.GetKey(KeyCode.W))
        //    rigidbody.AddForce(Vector2.up);

        //if (Input.GetKey(KeyCode.S))
        //    rigidbody.AddForce(Vector2.down);

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rigidbody.velocity = movement * speed;

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
        bulletsLeft = 100;
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
