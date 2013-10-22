using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.IO;

namespace MOBA.Assets
{
    public class Image
    {
        public Texture2D Texture;
        public Rectangle sRect;

        public Image(Texture2D boundImage, Rectangle source)
        {
            Texture = boundImage;

            if (source != null)
                sRect = source;
            else
                sRect = new Rectangle(0, 0, boundImage.Width, boundImage.Height);
        }
    }
}
