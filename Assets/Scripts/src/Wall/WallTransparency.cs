using System;
using System.Collections;
using System.Collections.Generic;
using src.Base;
using src.Helpers;
using UnityEngine;

namespace src.Wall
{
    public class WallTransparency : GameplayComponent
    {
        public float secondsToWait = 0.1f; 
        private SpriteRenderer _spriteRenderer;
        private Color _originalSpriteColor;
        private bool _isTransparent;

        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _originalSpriteColor = _spriteRenderer.color;
            StartCoroutine(nameof(CheckIfSomethingNear));
        }

        private IEnumerator CheckIfSomethingNear()
        {
            while (true)
            {
                    var position = transform.position;
                    /* RayCast from the center of the tile up one distance and set layerMask to Player only! */
                    var hit = Physics2D.Raycast(new Vector2(position.x + .5f, position.y + 0.5f),
                        Vector2.up, 1f, 1 << 15);
                    if (hit.collider)
                    {
                        BecomeTransparent();
                    }
                    else
                    {
                        BecomeOpaque();
                    }

                    yield return new WaitForSeconds(secondsToWait);
            }
        }

        private void ChangeSpriteAlpha(float alpha)
        {
            _spriteRenderer.color =
                new Color(_originalSpriteColor.r, _originalSpriteColor.g, _originalSpriteColor.b, alpha);
        }

        private void BecomeTransparent()
        {
            if (_isTransparent)
            {
                return;
            }

            _isTransparent = true;
            ChangeSpriteAlpha(0.5f);
        }

        private void BecomeOpaque()
        {
            if (!_isTransparent)
            {
                return;
            }

            _isTransparent = false;
            ChangeSpriteAlpha(1f);
        }
    }
}