using System.Collections;
using src.Managers;
using UnityEngine;

namespace src.Ammo
{
    public class BombController : MonoBehaviour, IExplosable
    {
        public GameObject explosionPrefab;

        private readonly BombsUtilManager _bombsUtil = BombsUtilManager.Instance;
        private bool _exploded;

        // Start is called before the first frame update
        void Start()
        {
            Invoke(nameof(Explode), _bombsUtil.Timer);
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

            _exploded = true;
            Destroy(gameObject, 0.3f);
            _bombsUtil.RemoveBomb(transform.position);
        }

        private IEnumerator CreateExplosions(Vector3 direction)
        {
            for (int i = 1; i < _bombsUtil.Power; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, i,
                    1 << 8);
                if (!hit.collider)
                {
                    Instantiate(explosionPrefab, transform.position + i * direction, 
                        explosionPrefab.transform.rotation);
                }
                else
                {
                    var key = hit.collider.GetComponent<IExplosable>();
                    if(key != null)
                    {
                        key.onExplosion();
                    }                  
                    break;
                }
            }

            yield return new WaitForSeconds(0.05f);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!_exploded && other.CompareTag("Explosion"))
            {
                onExplosion();
            }
        }

        public void onExplosion()
        {
            CancelInvoke(nameof(Explode));
            Explode();
        }
    }
}