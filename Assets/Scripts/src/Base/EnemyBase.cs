using UnityEngine;
using src.Helpers;

public abstract class EnemyBase : MonoBehaviour, IExplosable
{

    protected Vector2[] _directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
 
    protected Rigidbody2D Rigidbody2d { get; set; }
    protected float Speed { get; set; }
    protected Vector2 Direction { get; set; }

    // Start is called before the first frame update
    protected void Start()
    {
        Direction = _directions.ChoseRandom();
        Rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        Rigidbody2d.MovePosition(Rigidbody2d.position + Direction * Speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Explosion"))
        {
            onExplosion();
        }
    }

    public void onExplosion()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        MoveToCenterOfTheCell();
        Direction = _directions.ChoseRandomExcept(Direction);
    }

    protected void MoveToCenterOfTheCell()
    {
        var absX = Mathf.RoundToInt(transform.position.x);
        var absY = Mathf.RoundToInt(transform.position.y);
        Vector2 position = new Vector2(absX, absY);
        transform.SetPositionAndRotation(position, Quaternion.identity);
    }

    protected Vector2 ChooseRandomDirection()
    {
        return _directions.ChoseRandom();
    }

    protected Vector2 ChooseRandomExceptCertainDirection(Vector2 direction)
    {
        return _directions.ChoseRandomExcept(direction);
    }
}
