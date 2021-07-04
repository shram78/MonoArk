using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections;

namespace MonoArk
{
	public class GuiManager
	{

		private Dictinory<string, Button> _buttons;

		public GuiManager()
		{
			_buttons = new Dictinory<string, Button>;
			_buttons.Add("EXIT", new Button(100, 800, 200, 100, Content.Load<Texture2D>("ExitNoPress")));
			_buttons.Add("START", new Button(100, 400, 200, 100, Content.Load<Texture2D>("StartNoPress")));
			_buttons.Add("OPTIONS", new Button(100, 600, 200, 100, Content.Load<Texture2D>("menuOptionButton")));
			_buttons.Add("BACK", new Button(850, 800, 200, 100, Content.Load<Texture2D>("backButton")));
			_buttons.Add("FULLSCREEN", new Button(750, 400, 200, 100, Content.Load<Texture2D>("FullScreenButtun")));
			_buttons.Add("WINDOW", new Button(950, 400, 200, 100, Content.Load<Texture2D>("WindowsButtun")));
		}

		public Button GetButton(string buttonName)
        {
			return _buttons[buttonName];

		}

		public bool CheckCollision(string buttonName, int mouseX, int mouseY)
        {
			return _buttons[buttonName].ContainsButton(mouseX, mouseY);
		}
	}
}
