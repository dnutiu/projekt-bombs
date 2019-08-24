using System.Collections;
using src.Base;
using src.Helpers;
using src.Managers;
using UnityEngine;

namespace src.Bomb
{
    public class BombController : GameplayComponent, IExplosable
    {
        private readonly BombsUtilManager _bombsUtil = BombsUtilManager.instance;
        private BombCameraShake _cameraShake;
        private SpriteRenderer _spriteRenderer;
        private bool _exploded;

        // Start is called before the first frame update
        private void Start()
        {
            _cameraShake = GameObject.Find("VCAM1").GetComponent<BombCameraShake>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Invoke(nameof(Explode), _bombsUtil.timer);
        }

        private void Explode()
        {
            Instantiate(PrefabAtlas.BombExplosion, transform.position, Quaternion.identity);

            _spriteRenderer.enabled = false;
            _cameraShake.StartCameraShakeCoro();
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
            for (var i = 1; i < _bombsUtil.power; i++)
            {
                var hit = Physics2D.Raycast(new Vector2(currentPosition.x + 0.5f,
                        currentPosition.y + 0.5f), direction, i, 1 << 8);

                if (!hit.collider)
                {
                    Instantiate(PrefabAtlas.BombExplosion, transform.position + i * direction,
                        PrefabAtlas.BombExplosion.transform.rotation);
                }
                else
                {
                    var key = hit.collider.GetComponent<IExplosable>();
                    key?.OnExplosion();
                    break;
                }
            }

            yield return new WaitForSeconds(0.05f);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!_exploded && other.CompareTag("Explosion"))
            {
                OnExplosion();
            }
        }

        public void OnExplosion()
        {
            CancelInvoke(nameof(Explode));
            Explode();
        }

        public void OnDestroy()
        { 
            _bombsUtil.UnregisterBomb(transform.position);
        }
    }
}