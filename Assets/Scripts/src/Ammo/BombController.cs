using System.Collections;
using src.Base;
using src.Managers;
using UnityEngine;

namespace src.Ammo
{
    public class BombController : GameplayComponent, IExplosable
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

            GetComponentInChildren<SpriteRenderer>().enabled = false;

            StartCoroutine(CreateExplosions(Vector3.down));
            StartCoroutine(CreateExplosions(Vector3.left));
            StartCoroutine(CreateExplosions(Vector3.up));
            StartCoroutine(CreateExplosions(Vector3.right));

            transform.Find("2DCollider").gameObject.SetActive(false);

            _exploded = true;
            Destroy(gameObject, 0.55f);
        }

        private IEnumerator CreateExplosions(Vector3 direction)
        {
            var currentPosition = transform.position;
            for (var i = 1; i < _bombsUtil.Power; i++)
            {
                var hit = Physics2D.Raycast(new Vector2(currentPosition.x + 0.5f,
                        currentPosition.y + 0.5f), direction, i, 1 << 8);

                if (!hit.collider)
                {
                    Instantiate(explosionPrefab, transform.position + i * direction,
                        explosionPrefab.transform.rotation);
                }
                else
                {
                    Debug.Log("Hit something");
                    var key = hit.collider.GetComponent<IExplosable>();
                    key?.onExplosion();
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

        public void OnDestroy()
        {
            _bombsUtil.RemoveBomb(transform.position);
        }
    }
}