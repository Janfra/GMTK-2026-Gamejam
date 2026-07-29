using System;

namespace GMTK.Calculation
{
    [Serializable]
    public class DivideFunction : BaseFunction
    {
        public override int GetResult(int A, int B)
        {
            return A / B;
        }

        public override string GetSymbol()
        {
            return "/";
        }
    }
}
