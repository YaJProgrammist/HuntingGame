using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeBeforeRemoving = 0.25f;

    [SerializeField] private float bulletSpeed = 10f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed;
    }

    private IEnumerator RemoveBulletAfterSomeTime()
    {
        yield return new WaitForSeconds(timeBeforeRemoving);
        Remove();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wolf" || collision.tag == "doe" || collision.tag == "bunny")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            Remove();
        }
    }

    private void Start()
    {
        StartCoroutine(RemoveBulletAfterSomeTime());
    }

}
