using GMTK.Calculation;
using UnityEngine;

namespace GMTK
{
    [CreateAssetMenu(fileName = "FunctionColourConfiguration", menuName = "Scriptable Objects/Function Colour Configuration")]
    public class FunctionColourConfiguration : ScriptableObject
    {
        [SerializeField]
        private Color _addColour = Color.white;
        [SerializeField]
        private Color _minusColour = Color.white;
        [SerializeField]
        private Color _multiplicationColour = Color.white;
        [SerializeField]
        private Color _divisionColour = Color.white;

        public Color GetFunctionColor(BaseFunction baseFunction)
        {
            switch (baseFunction)
            {
                case AddFunction addFunction:
                    return _addColour;
                case RemoveFunction removeFunction:
                    return _minusColour;
                case MultiplyFunction multiplyFunction:
                    return _multiplicationColour;
                case DivideFunction divideFunction:
                    return _divisionColour;
                default:
                    return Color.white;
            }
        }
    }
}