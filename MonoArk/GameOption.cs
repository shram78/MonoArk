﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{
    class GameOption
    {
        private int widthClip;
        private int heightClip;
       // public GraphicsDeviceManager graphicsManager;
        public GameOption(int WidthClip, int HeightClip)
        {
            widthClip = WidthClip;
            heightClip = HeightClip;
        }

        public void ChangeResolution(int Width, int Height)
        {
            //Сохранил новые настройки
            widthClip = Width;
            heightClip = Height;
            //Отправил запрос на изменение разрешения движку
        //    graphics.PreferredBackBufferWidth = widthClip;
          //  graphics.PreferredBackBufferHeight = heightClip;
            //Применил изменение разрешения
           // graphics.ApplyChanges();
        }

        //public bool SetFullScreenOreWindows(GraphicsDeviceManager graphicsManager)
        //{
        //    if (graphicsManager.IsFullScreen == false)
        //    {
        //        graphicsManager.ApplyChanges();
        //        return true;
        //    }
        //    else
        //    {
        //        graphicsManager.ApplyChanges();
        //        return false;
        //    }
        //}

        public void SetFullScreenMode(GraphicsDeviceManager graphicsManager)
        {
                graphicsManager.IsFullScreen = true;
                graphicsManager.ApplyChanges();
        }

        public void SetWindowsMode(GraphicsDeviceManager graphicsManager)
        {
                graphicsManager.IsFullScreen = false;
                graphicsManager.ApplyChanges();
        }

    }
}