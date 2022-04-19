namespace CSharp
{
    public class Composition
    {
        private int f(decimal value) => (int)value;

        private bool g(int value) => value % 2 == 0;

        public bool g_after_f(decimal value) => throw new System.NotImplementedException();
    }
}