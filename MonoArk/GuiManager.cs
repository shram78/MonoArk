using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace MonoArk
{
	class GuiManager
	{

		private Dictionary<string, Button> _buttons;

		public GuiManager()
		{
			_buttons = new Dictionary<string, Button>();
		}

		public void AddButton(string buttonName, Button button)
        {
			_buttons.Add(buttonName, button);
		}

		public Button GetButton(string buttonName)
		{
			return _buttons[buttonName];
		}

		public void ResizeButtons(double scaleX, double scaleY) {

			foreach (Button button in _buttons.Values)
				button.Resize(scaleX, scaleY);
		}

        public bool CheckCollision(string buttonName, int mouseX, int mouseY)
		{
			return _buttons[buttonName].ContainsButton(mouseX, mouseY);
		}
	}
}
