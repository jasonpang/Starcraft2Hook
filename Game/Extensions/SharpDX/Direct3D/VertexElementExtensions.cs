using SharpDX.Direct3D9;

namespace Game.Extensions.SharpDX.Direct3D
{
    public static class VertexElementExtensions
    {
        public static bool IsEqual(this VertexElement a, VertexElement b)
        {
            return (a.Method == b.Method &&
                    a.Offset == b.Offset &&
                    a.Stream == b.Stream &&
                    a.Type == b.Type &&
                    a.Usage == b.Usage &&
                    a.UsageIndex == b.UsageIndex);
        }

        public static bool IsEndOfDeclaration(this VertexElement a)
        {
            return IsEqual(a, VertexElement.VertexDeclarationEnd);
        }
    }
}
