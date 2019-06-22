using UnityEngine;
using src.Helpers;

public class DumbEnemy : EnemyBase
{
    protected new void Start()
    {
        Speed = 4f;
        base.Start();
    }

    protected new void FixedUpdate()
    {     
        if (transform.position.x == Mathf.Floor(transform.position.x) && transform.position.y == Mathf.Floor(transform.position.y))
        {
            if (RandomChange())
            {
                HandleChangeDirection();
            }
            else
            {
                Rigidbody2d.MovePosition(Rigidbody2d.position + Direction * Speed * Time.deltaTime);
            }
        }
        else
        {
            Rigidbody2d.MovePosition(Rigidbody2d.position + Direction * Speed * Time.deltaTime);
        }
    }

    private bool RandomChange()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 100);
        return randomNumber <= 25;
    }

    private void HandleChangeDirection()
    {
        DebugHelper.LogInfo("Direction randomly changed");
        Direction = ChooseRandomExceptCertainDirection(Direction);
        MoveToCenterOfTheCell();
        Rigidbody2d.MovePosition(Rigidbody2d.position + Direction * Speed * Time.deltaTime);
    }
}
