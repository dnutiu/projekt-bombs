using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
 
    public LayerMask levelMask;
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

        transform.Find("2DCollider").gameObject.SetActive(false);

        //StartCoroutine(CreateExplosions(Vector3.up));
        //StartCoroutine(CreateExplosions(Vector3.down));
        //StartCoroutine(CreateExplosions(Vector3.left));
        //StartCoroutine(CreateExplosions(Vector3.right));

        CreateExplosions(Vector3.up);
        CreateExplosions(Vector3.down);
        CreateExplosions(Vector3.left);
        CreateExplosions(Vector3.right);

        GetComponent<SpriteRenderer>().enabled = false;
        exploded = true;

        //Destroy(gameObject, 0.3f);
        Destroy(gameObject);
    }

    //private IEnumerator CreateExplosions(Vector3 direction)
    //{
    //    for (int i = 1; i < bombStatsUtil.Power; i++)
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, .5f, 0), direction, i, levelMask);

    //        if (!hit.collider)
    //        {
    //            Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
    //        }
    //        else
    //        {
    //            break;
    //        }

    //    }

    //    yield return new WaitForSeconds(0.05f);

    //}

    private void CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i < bombStatsUtil.Power; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, .5f, 0), direction, i, levelMask);

            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            }
            else
            {
                break;
            }

        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!exploded && other.CompareTag("Explosion"))
        {
            //In caz ca o bomba loveste bomba, dam cancel la explozie sa nu explodeze twice si o explodam automagic
            CancelInvoke("Explode");
            Explode();
        }
    }
}
