using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Animal : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    protected List<Vector2> velocities;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        velocities = new List<Vector2>();
    }

    void Update()
    {
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

        rigidbody.velocity = averageVelocity;
    }
}
