using Microsoft.Xna.Framework.Graphics;

namespace ImGuiNET.SampleProgram.XNA
{
    public static class DrawVertDeclaration
    {
        public static readonly VertexDeclaration DECLARATION;

        public static readonly int SIZE;

        static DrawVertDeclaration()
        {
            unsafe { SIZE = sizeof(ImDrawVert); }

            DECLARATION = new VertexDeclaration(
                SIZE,

                // Position
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),

                // UV
                new VertexElement(8, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),

                // Color
                new VertexElement(16, VertexElementFormat.Color, VertexElementUsage.Color, 0)
            );
        }
    }
}
