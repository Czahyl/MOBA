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
        List<Image> imageList = new List<Image>();
        List<Image> classImageList = new List<Image>();
        List<Song> soundList = new List<Song>();
        List<SpriteFont> fontList = new List<SpriteFont>();

        public void storeImage(Texture2D texture, Rectangle source)
        {
            try
            {
                imageList.Add(new Image(texture, source));
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
                        imageList.Add(new Image(texture, new Rectangle(x, y, sprHeight, sprWidth)));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to store from sheet\n"+e);
            }
        }

        public void storeClassSheet(Texture2D texture, int sprWidth, int sprHeight, int picWidth, int picHeight)
        {
            try
            {
                for (int x = 0; x < picWidth; x += sprWidth)
                {
                    for (int y = 0; y < picHeight; y += sprHeight)
                    {
                        classImageList.Add(new Image(texture, new Rectangle(x, y, sprWidth, sprHeight)));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to store from sheet\n" + e);
            }
        }
 
        public Image getTexture(int id)
        {
            return imageList[id];
        }

        public void storeFont(SpriteFont font)
        {
            fontList.Add(font);
        }

        public SpriteFont getFont(int id)
        {
            return fontList[id];
        }

        public void Dispose()
        {
            for (int i = 0; i < imageList.Count; i++)
            {
                imageList[i].Texture.Dispose();
                imageList.Remove(imageList[i]);
            }
        }

    }
}
