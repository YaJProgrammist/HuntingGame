using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Wolf : Animal
{
    const float NORMAL_SPEED = 5;
    const float HIGH_SPEED = 15;
    const float TIME_WITHOUT_FOOD = 10;
    private float timerWithoutFood = 0;

    protected override void Start()
    {
        base.Start();
        currentSpeed = NORMAL_SPEED;
    }

    protected override void Update()
    {
        timerWithoutFood += Time.deltaTime;

        if (timerWithoutFood >= TIME_WITHOUT_FOOD)
        {
            Destroy(this.gameObject);
        }

        base.Update();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.TryGetComponent<Bunny>(out _) || collider.TryGetComponent<Doe>(out _) || collider.TryGetComponent<Hunter>(out _))
        {
            Destroy(collider.gameObject);
            timerWithoutFood = 0;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.isTrigger && !collider.TryGetComponent<Wolf>(out _))
        {
            currentSpeed = HIGH_SPEED;
            velocities.Add(collider.transform.position - this.transform.position);
        }
    }
}
