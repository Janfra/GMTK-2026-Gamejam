using DG.Tweening;
using UnityEngine;

namespace GMTK.Visuals
{
    public class SpriteTweenerComponent : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _renderer;

        public Color DefaultColour = Color.white;

        private void Start()
        {
            DefaultColour = _renderer.color;
        }

        public void TweenRendererColour(ColourTweenConfiguration config)
        {
            DOTween.Kill(_renderer);

            if (config.ReturnsToDefault)
            {
                var sequence = DOTween.Sequence().SetId(_renderer);
                sequence.Insert(0, _renderer.DOColor(config.TargetColour, config.Duration).SetEase(config.EaseType));
                sequence.Insert(config.Duration, _renderer.DOColor(DefaultColour, config.ReturnDuration).SetEase(config.ReturnEaseType));
            }
            else
            {
                _renderer.DOColor(config.TargetColour, config.Duration).SetId(_renderer);
            }
        }
    }
}