using System;

namespace GMTK.Calculation
{
    [Serializable]
    public class DivideFunction : BaseFunction
    {
        public override int GetResult(int A, int B)
        {
            if (A == 0 || B == 0)
            {
                return 0;
            }

            return A / B;
        }

        public override string GetSymbol()
        {
            return "/";
        }
    }
}
