using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace XLuaFramework.LuaTool
{
    internal static class ResourcesTool
    {
        public static Texture2D LoadTexture2D(string path)
        {
            return Resources.Load<Texture2D>(path);
        }
        public static Sprite LoadSprite(string path, Rect rect, Vector2 pivot, float pixelsPerUnit)
        {
            Texture2D texture = LoadTexture2D(path);
            return Sprite.Create(texture, rect, pivot, pixelsPerUnit);
        }
    }
}
