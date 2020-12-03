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

    void Start()
    {
        StartCoroutine(Remove());
    }

}
