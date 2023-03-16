using System;
using System.Collections.Generic;
using System.Drawing;

namespace Nucleus.Gaming.Cache
{
    public class ImageCache
    {
        private static Dictionary<string, Bitmap> cachedImages = new Dictionary<string, Bitmap>();

        public static Bitmap GetImage(string path)
        {
            lock (cachedImages)
            {
                if (AddToCache(path))
                {
                    return cachedImages[path];
                }
                else
                {
                    AddToCache(path);
                    return new Bitmap(path);
                }
            }
        }

        public static bool AddToCache(string path)
        {
            if(!cachedImages.ContainsKey(path))
            {
                cachedImages.Add(path,new Bitmap(path));
                return false;
            }

            return true;
        }

    }
}
