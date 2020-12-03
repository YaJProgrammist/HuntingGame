using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Doe : Animal
{
    const float NORMAL_SPEED = 3;
    const float HIGH_SPEED = 5;
    const float DISTANCE_BETWEEN_DOES = 1;
    [SerializeField] private ColliderTrigger flairArea;
    [SerializeField] private ColliderTrigger selfCollider;

    protected override void Start()
    {
        base.Start();
        currentSpeed = NORMAL_SPEED;
        isAccelerated = false;
        flairArea.OnColliderTriggered += (s, ea) => OnFlairTriggered(ea.Collider);
        selfCollider.OnColliderTriggered += (s, ea) => OnSelfTriggered(ea.Collider);
    }

    protected override void StartWondering()
    {
        currentSpeed = NORMAL_SPEED;
        base.StartWondering();
    }

    void OnSelfTriggered(Collider2D collider)
    {
        if (collider.tag == "wolf" || collider.tag == "wall" || collider.tag == "bullet")
        {
            this.Remove();
        }
    }

    void OnFlairTriggered(Collider2D collider)
    {
        if (collider.tag == "wall")
        {
            TryAvoidWall(collider);
            return;
        }

        if (collider.tag == "wolf" || collider.tag == "hunter")
        {
            isAccelerated = true;
            currentSpeed = HIGH_SPEED;
            velocities.Add(this.transform.position - collider.transform.position);
            return;
        }

        if (collider.tag != "doe")
        {
            return;
        }

        Vector2 betweenVector = this.transform.position - collider.transform.position;
        float distance = betweenVector.magnitude;

        if (distance < DISTANCE_BETWEEN_DOES)
        {
            velocities.Add(betweenVector);
        }
        else if (distance > DISTANCE_BETWEEN_DOES)
        {
            velocities.Add(-betweenVector);
        }
    }
}
