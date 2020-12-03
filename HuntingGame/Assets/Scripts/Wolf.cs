using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Wolf : Animal
{
    const float NORMAL_SPEED = 3;
    const float HIGH_SPEED = 8;
    const float TIME_WITHOUT_FOOD = 60;
    private float timerWithoutFood = 0;
    [SerializeField] private ColliderTrigger flairArea;
    [SerializeField] private ColliderTrigger selfCollider;

    protected override void Start()
    {
        base.Start();
        currentSpeed = NORMAL_SPEED;
        flairArea.OnColliderTriggered += (s, ea) => OnFlairTriggered(ea.Collider);
        selfCollider.OnColliderTriggered += (s, ea) => OnSelfTriggered(ea.Collider);
    }

    protected override void Update()
    {
        timerWithoutFood += Time.deltaTime;

        if (timerWithoutFood >= TIME_WITHOUT_FOOD)
        {
            this.Remove();
        }

        base.Update();
    }

    protected override void StartWondering()
    {
        currentSpeed = NORMAL_SPEED;
        base.StartWondering();
    }

    void OnSelfTriggered(Collider2D collider)
    {
        if (collider.tag == "wall")
        {
            this.Remove();
            return;
        }

        if (collider.tag == "bunny" || collider.tag == "doe" || collider.tag == "hunter")
        {
            timerWithoutFood = 0;
        }
        else if (collider.tag == "bullet")
        {
            Remove();
        }
    }

    void OnFlairTriggered(Collider2D collider)
    {
        if (collider.tag == "wall")
        {
            TryAvoidWall(collider);
            return;
        }

        if (collider.tag == "bunny" || collider.tag == "doe" || collider.tag == "hunter")
        {
            isAccelerated = true;
            currentSpeed = HIGH_SPEED;
            velocities.Add(collider.transform.position - this.transform.position);
        }
    }
}
