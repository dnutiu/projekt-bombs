using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour, IExplosable
{
    public GameObject explosionPrefab;

    BombStatsUtil bombStatsUtil = BombStatsUtil.Instance;
    bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", bombStatsUtil.Timer);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        GetComponent<SpriteRenderer>().enabled = false;
        transform.Find("2DCollider").gameObject.SetActive(false);

        StartCoroutine(CreateExplosions(Vector3.down));
        StartCoroutine(CreateExplosions(Vector3.left));
        StartCoroutine(CreateExplosions(Vector3.up));
        StartCoroutine(CreateExplosions(Vector3.right));
      
        exploded = true;
        Destroy(gameObject, 0.3f);
        //GameObject bombObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //bombObject.GetComponent<Explosion>().Explode(transform.position);

        Destroy(gameObject);
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i < bombStatsUtil.Power; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, i, 1 << 8);
            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            }
            else
            {
                Debug.Log("Collided with something");
                break;
            }

        }

        yield return new WaitForSeconds(0.05f);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!exploded && other.CompareTag("Explosion"))
        {
            onExplosion();
        }
    }

    public void onExplosion()
    {
        //In caz ca o bomba loveste bomba, dam cancel la explozie sa nu explodeze twice si o explodam automagic
        CancelInvoke("Explode");
        Explode();
    }
}
