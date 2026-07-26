using DG.Tweening;
using UnityEngine;

namespace GMTK
{
    public class SpriteTweenerComponent : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _renderer;

        public void TweenRendererColour(ColourTweenConfiguration config)
        {
            DOTween.Kill(_renderer);
            _renderer.DOColor(config.TargetColour, config.Duration).SetId(_renderer);
        }

        public void TweenRendererColourIfTrue(bool condition, ColourTweenConfiguration config)
        {

        }
    }
}