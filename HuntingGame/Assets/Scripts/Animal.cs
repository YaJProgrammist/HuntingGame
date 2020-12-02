using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Animal : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    protected List<Vector2> velocities;
    protected float currentSpeed;

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        velocities = new List<Vector2>();
    }

    protected virtual void Update()
    {
        Debug.Log("upd");
        AdjustVelocity();
        velocities.Clear();
    }

    private void AdjustVelocity()
    {
        Vector2 averageVelocity = new Vector2(0, 0);

        if (velocities.Count > 0)
        {
            foreach (Vector2 velocity in velocities)
            {
                averageVelocity += velocity.normalized;
            }

            averageVelocity /= velocities.Count;
        }

        rigidbody.velocity = averageVelocity * currentSpeed;
    }
}
