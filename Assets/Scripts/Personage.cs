using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personage : MonoBehaviour
{
    [SerializeField]
    protected Vector2 Speed2D;

    [SerializeField]
    protected Animator animator;

    protected Vector2 LookAt;

    protected void MoveTowardDirection(Vector2 direction)
    {
        direction.Normalize();
        Vector2 step = Vector2.Scale(Speed2D, direction);
        transform.Translate(step);

        SetAnimationParam(direction);
        LookAt = direction;
    }

    protected void MoveTowardPosition(Vector2 position)
    {
        Vector2 direction = new Vector2(
            position.x - transform.position.x,
            position.y - transform.position.y
            );
        MoveTowardDirection(direction);
    }

    private void SetAnimationParam(Vector2 direction)
    {
        animator.SetFloat("Magnitude", direction.sqrMagnitude);
        // If we are on movement, we update the horizontal and vertical.
        if(direction.sqrMagnitude > 0.1)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        // If not, we want to keep the previous value to adjust our idle position.
    }
}
