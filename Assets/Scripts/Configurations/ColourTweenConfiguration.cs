using DG.Tweening;
using UnityEngine;

namespace GMTK
{
    [CreateAssetMenu(fileName = "ColourTween Configuration", menuName = "Scriptable Objects/Colour Tween Configuration")]
    public class ColourTweenConfiguration : ScriptableObject
    {
        [SerializeField]
        private Color _targetColour = Color.white;
        public Color TargetColour => _targetColour;

        [SerializeField]
        private float _duration = 0.2f;
        public float Duration => _duration;

        [SerializeField]
        private Ease _easeType = Ease.Linear;
        public Ease EaseType => _easeType;

        [SerializeField]
        private bool _returnsToDefault = true;
        public bool ReturnsToDefault => _returnsToDefault;

        [SerializeField]
        private float _returnDuration = 0.1f;
        public float ReturnDuration => _returnDuration;

        [SerializeField]
        private Ease _returnEaseType = Ease.Linear;
        public Ease ReturnEaseType => _returnEaseType;
    }
}