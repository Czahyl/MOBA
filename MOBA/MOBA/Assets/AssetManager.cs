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
    public class AssetManager
    {
        public List<Image> textureList = new List<Image>();
        List<Song> soundList = new List<Song>();
        List<SpriteFont> fontList = new List<SpriteFont>();

        public void storeTexture(Texture2D texture, Rectangle source)
        {
            try
            {
                textureList.Add(new Image(texture, source));
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to store texture\n"+e);
            }
        }

        public void storeFromSheet(Texture2D texture, int sprWidth, int sprHeight, int picWidth, int picHeight)
        {
            try
            {
                for (int x = 0; x < picWidth; x += sprWidth)
                {
                    for (int y = 0; y < picHeight; y += sprHeight)
                    {
                        textureList.Add(new Image(texture, new Rectangle(x, y, sprHeight, sprWidth)));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to store from sheet\n"+e);
            }
        }
 

        public Image getTexture(int id)
        {
            return textureList[id];
        }
    }
}
