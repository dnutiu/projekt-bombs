using src.Helpers;
using src.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IExplosable
{

    protected Vector2[] _directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
    protected readonly GameStateManager gameStateManager = GameStateManager.Instance;

    protected Rigidbody2D Rigidbody2d { get; set; }
    protected float Speed { get; set; }
    protected Vector2 Direction { get; set; }

    private List<Vector3> _allowedDirections = new List<Vector3>();
    private bool _isStucked = false;
    private System.Random _random = new System.Random();

    // Start is called before the first frame update
    protected void Start()
    {
        Direction = _directions.ChoseRandom();
        Rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (gameStateManager.IsGamePaused || gameStateManager.IsPlayerMovementForbidden) {return;}
        if(_isStucked)
        {
            Unstuck();
        }
        Rigidbody2d.MovePosition(Rigidbody2d.position + Speed * Time.deltaTime * Direction);
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
        Unstuck();
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        MoveToCenterOfTheCell();
        Unstuck();
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

    private void Unstuck()
    {
        _allowedDirections.Clear();
        StartCoroutine(CheckForObstacle(Vector3.down));
        StartCoroutine(CheckForObstacle(Vector3.left));
        StartCoroutine(CheckForObstacle(Vector3.up));
        StartCoroutine(CheckForObstacle(Vector3.right));
        if(_allowedDirections.Count == 0)
        {
            _isStucked = true;
        }
        else
        {
            var index = _random.Next(_allowedDirections.Count);
            Direction = _allowedDirections[index];
            _isStucked = false;
        }
    }

    private IEnumerator CheckForObstacle(Vector3 direction)
    {
        var layerMask = (1 << 8) | (1 << 9);
        var currentPosition = transform.position;

        var hit = Physics2D.Raycast(new Vector2(currentPosition.x + 0.5f, currentPosition.y + 0.5f), direction, 1, layerMask);

       if (!hit.collider)
       {
            _allowedDirections.Add(direction);
       }

        yield return new WaitForSeconds(0.05f);
    }
}
