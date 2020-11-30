using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Animal : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private List<Vector2> velocities;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        velocities = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustVelocity();
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
