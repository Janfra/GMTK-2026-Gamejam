using UnityEngine;

namespace GMTK
{
    [CreateAssetMenu(fileName = "ColourTween Configuration", menuName = "Scriptable Objects/Colour Tween Configuration")]
    public class ColourTweenConfiguration : ScriptableObject
    {
        [SerializeField]
        private Color _targetColour;
        public Color TargetColour => _targetColour;

        [SerializeField]
        private float _duration;
        public float Duration => _duration;
    }
}