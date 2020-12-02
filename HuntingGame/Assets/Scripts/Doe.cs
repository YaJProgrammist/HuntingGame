using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Doe : Animal
{
    const float NORMAL_SPEED = 5;
    const float HIGH_SPEED = 10;

    protected override void Start()
    {
        base.Start();
        currentSpeed = NORMAL_SPEED;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.isTrigger)
        {
            currentSpeed = HIGH_SPEED;
            velocities.Add(this.transform.position - collider.transform.position);
        }
    }
}
