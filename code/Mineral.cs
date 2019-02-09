using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace OpenBot
{
	public static class Mineral
	{
		public enum Colors
		{
			Red = 70,
			Orange = 76,
			Yellow = 75,
			Green = 74,
			Cyan = 73,
			Blue = 72,
			Purple = 71,
			None = 0
		}

		public const int Red = 70;
		public const int Orange = 76;
		public const int Yellow = 75;
		public const int Green = 74;
		public const int Cyan = 73;
		public const int Blue = 72;
		public const int Purple = 71;
		public const int None = 0;

		public static void PlaceBlock(Connection con, Colors color, int layer, int x, int y)
		{
			int blockid = 0;

			if (color == Colors.Red) blockid = Red;
			if (color == Colors.Orange) blockid = Orange;
			if (color == Colors.Yellow) blockid = Yellow;
			if (color == Colors.Green) blockid = Green;
			if (color == Colors.Cyan) blockid = Cyan;
			if (color == Colors.Blue) blockid = Blue;
			if (color == Colors.Purple) blockid = Purple;
			if (color == Colors.None) = blockid = None;
			con.Send("b", layer, x, y, blockid);
		}
	}
}
