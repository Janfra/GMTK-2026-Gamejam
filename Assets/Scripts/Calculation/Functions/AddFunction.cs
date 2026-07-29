using System;

namespace GMTK.Calculation
{
    [Serializable]
    public class AddFunction : BaseFunction
    {
        public override int GetResult(int A, int B)
        {
            return A + B;
        }

        public override string GetSymbol()
        {
            return "+";
        }
    }
}
