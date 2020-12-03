using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeBeforeRemoving = 0.5f;

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(timeBeforeRemoving);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wolf" || collision.tag == "doe" || collision.tag == "bunny")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void Start()
    {
        StartCoroutine(Remove());
    }

}
