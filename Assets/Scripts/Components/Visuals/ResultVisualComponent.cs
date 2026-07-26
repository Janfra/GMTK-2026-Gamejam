using UnityEngine;

namespace GMTK.Visuals
{
    public class ResultVisualComponent : MonoBehaviour
    {
        [SerializeField]
        private SpriteTweenerComponent _tweener;
        [SerializeReference]
        private ColourTweenConfiguration _positiveColour;
        [SerializeReference]
        private ColourTweenConfiguration _negativeColour;

        public void OnResultDetermined(bool isPositive)
        {
            if (isPositive)
            {
                _tweener.TweenRendererColour(_positiveColour);
            }
            else
            {
                _tweener.TweenRendererColour(_negativeColour);
            }
        }
    }
}