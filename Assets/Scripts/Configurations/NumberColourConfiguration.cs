using UnityEngine;

namespace GMTK
{
    [CreateAssetMenu(fileName = "NumberColourConfiguration", menuName = "Scriptable Objects/Number Colour Configuration")]
    public class NumberColourConfiguration : ScriptableObject
    {
        [SerializeField]
        private Color _zeroColour = Color.white;
        [SerializeField]
        private Color _oneColour = Color.white;
        [SerializeField]
        private Color _twoColour = Color.white;
        [SerializeField]
        private Color _threeColour = Color.white;
        [SerializeField]
        private Color _fourColour = Color.white;
        [SerializeField]
        private Color _fiveColour = Color.white;
        [SerializeField]
        private Color _sixColour = Color.white;
        [SerializeField]
        private Color _sevenColour = Color.white;
        [SerializeField]
        private Color _eightColour = Color.white;
        [SerializeField]
        private Color _nineColour = Color.white;

        public Color GetColorForNumber(int number)
        {
            number = Mathf.Clamp(number, 0, 9);
            switch (number)
            {
                case 0:
                    return _zeroColour;
                case 1:
                    return _oneColour;
                case 2:
                    return _twoColour;
                case 3:
                    return _threeColour;
                case 4:
                    return _fourColour;
                case 5:
                    return _fiveColour;
                case 6:
                    return _sixColour;
                case 7:
                    return _sevenColour;
                case 8:
                    return _eightColour;
                case 9: 
                    return _nineColour;
                default:
                    return _oneColour;
            }
        }
    }
}