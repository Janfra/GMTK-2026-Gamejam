using System;

namespace GMTK.Calculation
{
    [Serializable]
    public class RemoveFunction : BaseFunction
    {
        public override int GetResult(int A, int B)
        {
            return A - B;
        }

        public override string GetSymbol()
        {
            return "-";
        }
    }
}
