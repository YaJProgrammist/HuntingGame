using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bunny : Animal
{
    void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("OnTrigger2DStay Bunny");
        if (!collider.isTrigger)
        {
            Debug.Log(this.transform.position - collider.transform.position);
            velocities.Add(new Vector2(this.transform.position.x - collider.transform.position.x, this.transform.position.y - collider.transform.position.y));
        }
    }
}
