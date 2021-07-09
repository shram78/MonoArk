using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoArk
{
    class GameOption
    {

        private GraphicsDeviceManager _graphicsManager;
        private int _widthClip;
        private int _heightClip;

        public GameOption(GraphicsDeviceManager graphicsManager, int widthClip, int heightClip)
        {
            _graphicsManager = graphicsManager;
            _widthClip = widthClip;
            _heightClip = heightClip;
            _graphicsManager.PreferredBackBufferWidth = _widthClip;
            _graphicsManager.PreferredBackBufferHeight = _heightClip;
            _graphicsManager.ApplyChanges();
        }

        public void SetResolution(int widthClip, int heightClip)
        {
            _widthClip = widthClip;
            _heightClip = heightClip;
            _graphicsManager.PreferredBackBufferWidth = _widthClip;
            _graphicsManager.PreferredBackBufferHeight = _heightClip;
            _graphicsManager.ApplyChanges();
        }

        //public void ChangeResolution(GraphicsDeviceManager graphicsManager)
        //{
        //    _widthClip = 1024;
        //    _heightClip = 768;

        //    graphicsManager.PreferredBackBufferWidth = _widthClip;
        //    graphicsManager.PreferredBackBufferHeight = _heightClip;

        //    //Сохранил новые настройки
        //    //widthClip = Width;
        //    //heightClip = Height;
        //    //Отправил запрос на изменение разрешения движку
        //    //    graphics.PreferredBackBufferWidth = widthClip;
        //    //  graphics.PreferredBackBufferHeight = heightClip;
        //    //Применил изменение разрешения
        //    // graphics.ApplyChanges();
        //}

        public void SetFullScreenMode(bool isFullscreen)
        {
            _graphicsManager.IsFullScreen = isFullscreen;
            _graphicsManager.ApplyChanges();
        }

        public double GetXscale()
        {
            return _widthClip / 1920.0;
        }

        public double GetYscale()
        {
            return _heightClip / 1080.0;
        }
    }
}