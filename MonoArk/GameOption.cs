using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{
    class GameOption
    {
        private int _widthClip;
        private int _heightClip;
        public GameOption(int widthClip, int heightClip)
        {
            _widthClip = widthClip;
            _heightClip = heightClip;
        }

        public void SetResolution(GraphicsDeviceManager graphicsManager)
        {
            graphicsManager.PreferredBackBufferWidth = _widthClip;
            graphicsManager.PreferredBackBufferHeight = _heightClip;
        }

        public void ChangeResolution(GraphicsDeviceManager graphicsManager)
        {
            _widthClip = 1024;
            _heightClip = 768;

            graphicsManager.PreferredBackBufferWidth = _widthClip;
            graphicsManager.PreferredBackBufferHeight = _heightClip;

            //Сохранил новые настройки
            //widthClip = Width;
            //heightClip = Height;
            //Отправил запрос на изменение разрешения движку
            //    graphics.PreferredBackBufferWidth = widthClip;
            //  graphics.PreferredBackBufferHeight = heightClip;
            //Применил изменение разрешения
            // graphics.ApplyChanges();
        }

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
