using System;

namespace GMTK.Calculation
{
    [Serializable]
    public abstract class BaseFunction
    {
        public abstract int GetResult(int A, int B);
        public abstract string GetSymbol();
    }

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

    [Serializable]
    public class MultiplyFunction : BaseFunction
    {
        public override int GetResult(int A, int B)
        {
            return A * B;
        }

        public override string GetSymbol()
        {
            return "x";
        }
    }

    [Serializable]
    public class DivideFunction : BaseFunction
    {
        public override int GetResult(int A, int B)
        {
            return A / B;
        }

        public override string GetSymbol()
        {
            return "÷";
        }
    }
}
