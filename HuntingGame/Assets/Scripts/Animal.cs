using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Animal : MonoBehaviour
{
    private const float DISTORTION_ANGLE_STEP = 10;
    private const float WONDER_CIRCLE_DISTANCE = 1;
    private const float WONDER_CIRCLE_RADIUS = 2;
    private float distortionAngle;
    private Rigidbody2D rigidbody;
    private bool isWondering;
    protected bool isAccelerated;
    private System.Random rand;
    private List<Vector2> forces;
    protected List<Vector2> velocities;
    protected float currentSpeed;
    public event EventHandler<AnimalRemovedEventArgs> OnAnimalRemoved;

    void Awake()
    {
        rand = new System.Random();
        velocities = new List<Vector2>();
        forces = new List<Vector2>();
        isAccelerated = false;
    }

    public void Remove()
    {
        OnAnimalRemoved?.Invoke(this, new AnimalRemovedEventArgs(this));
        Destroy(this.gameObject);
    }

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartWondering();
    }

    protected virtual void Update()
    {
        if (isAccelerated == true)
        {
            isWondering = false;
            AdjustVelocity();
            velocities.Clear();
            isAccelerated = false;
            return;
        }

        if (isWondering)
        {
            ContinueWondering();
            velocities.Clear();
            return;
        }

        StartWondering();
    }

    private void AdjustVelocity()
    {
        Vector2 averageVelocity = GetAverageVelocity();

        AddForce(averageVelocity);
        ApplyForces();
        LookInDirection(rigidbody.velocity);
    }

    private Vector2 GetAverageVelocity()
    {
        Vector2 averageVelocity = new Vector2(0, 0);

        if (velocities.Count > 0)
        {
            foreach (Vector2 velocity in velocities)
            {
                averageVelocity += velocity;
            }

            averageVelocity /= velocities.Count;
        }

        return averageVelocity.normalized;
    }

    private void LookInDirection(Vector2 direction)
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    protected virtual void StartWondering()
    {
        distortionAngle = 0;
        isWondering = true;
        ContinueWondering();
        velocities.Clear();
    }

    protected virtual void ContinueWondering()
    {
        Vector2 newVelocityPart;

        if (velocities.Count > 0)
        {
            newVelocityPart = GetAverageVelocity();
        }
        else
        {
            if (rand.Next(2) == 0)
            {
                distortionAngle += DISTORTION_ANGLE_STEP;
            }
            else
            {
                distortionAngle -= DISTORTION_ANGLE_STEP;
            }

            Vector2 futurePosition = (Vector2)this.transform.position + rigidbody.velocity * WONDER_CIRCLE_DISTANCE;
            Vector2 distortion = new Vector2(Mathf.Cos(distortionAngle * Mathf.Deg2Rad), Mathf.Sin(distortionAngle * Mathf.Deg2Rad)) * WONDER_CIRCLE_RADIUS;

            newVelocityPart = (futurePosition + distortion - (Vector2)this.transform.position).normalized;
        }

        AddForce(newVelocityPart);
        ApplyForces();

        LookInDirection(rigidbody.velocity);
    }

    protected void TryAvoidWall(Collider2D collider)
    {
        if (collider.TryGetComponent<Wall>(out Wall wall))
        {
            velocities.Add(wall.PushBackVector);
        }
    }

    private void AddForce(Vector2 newVelocityPart)
    {
        forces.Add(newVelocityPart);
    }

    private void ApplyForces()
    {
        Vector2 averageForce = Vector2.zero;
        for (int i = 0; i < forces.Count; i++)
        {
            averageForce += forces[i];
        }

        rigidbody.velocity = averageForce.normalized * currentSpeed;

        for (int i = 0; i < forces.Count; i++)
        {
            forces[i] *= 0.8f;
        }

        forces.RemoveAll(force => force.magnitude <= 0.01f);
    }
}
