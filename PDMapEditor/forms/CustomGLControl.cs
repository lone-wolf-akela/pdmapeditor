using OpenTK;
using OpenTK.Graphics;
using System.Windows.Forms;

namespace PDMapEditor
{
    class CustomGLControl : GLControl
    {
        // 32bpp color, 24bpp z-depth, 8bpp stencil and 4x antialiasing
        // OpenGL version is major=3, minor=0
        public CustomGLControl(int fsaaSamples)
            : base(new GraphicsMode(32, 24, 8, fsaaSamples), 3, 0, GraphicsContextFlags.Default)
        { }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            OnKeyDown(new KeyEventArgs(keyData));
            return false;
        }
    }
}