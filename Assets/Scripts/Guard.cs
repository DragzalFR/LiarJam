using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Personage
{
    private const float RANGE_OF_VIEW = 8;

    private Vector2 SPEED2D_PATROLLING = new Vector2(0.04f, 0.04f);
    private Vector2 SPEED2D_CHASING = new Vector2(0.07f, 0.07f);

    [SerializeField]
    private Vector2[] PatrolPathSteps = new Vector2[2];
    private int PatrolStepIndex = 0;

    public GameObject Prisoner;

    // For openening doors
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            other.gameObject.GetComponent<Door>().Open(int.MaxValue, this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Prisoner)
        {
            Prisoner.GetComponent<Player>().BeingHit();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CanSeePlayer())
        {
            Speed2D = SPEED2D_CHASING;
            MoveTowardPosition((Vector2)Prisoner.transform.position);
        }
        else
        {
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            if ((position2D - PatrolPathSteps[PatrolStepIndex]).magnitude < 0.1)
            {
                PatrolStepIndex++;
                PatrolStepIndex %= PatrolPathSteps.Length;
            }

            Speed2D = SPEED2D_PATROLLING;
            MoveTowardPosition(PatrolPathSteps[PatrolStepIndex]);
        }

    }

    private bool CanSeePlayer()
    {
        Vector2 relativePosition = Prisoner.transform.position - transform.position;
        if (relativePosition.magnitude > RANGE_OF_VIEW)
            return false;

        float angle = Vector2.Angle(LookAt, relativePosition);
        if (angle > 75)
            return false;

        int layerMask = (1 << LayerMask.NameToLayer("Guard"));
        layerMask = ~layerMask;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, relativePosition, RANGE_OF_VIEW, layerMask);

        // If it hits our prisoner.
        if (hit.collider.gameObject == Prisoner)
        {
            return true;
        }

        return false;
    }
}
