﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using PlayerIOClient; // magic being implemented
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace OpenBot
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		Connection con;
		bool snake = false;
		Client client;
		bool isConnected = false;
		int[] ignoresnakers = new int[] { };

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("You can access the source by visiting https://github.com/skull111-co/openbot.");
		}

		private void Connect(object sender, EventArgs e) // going to be included as "Click" event for btnConnect, see in Form1's designer class.
		{
			if (!isConnected)
			{
				try
				{
					client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", txtEmail.Text, txtPassword.Text, null);
					con = client.Multiplayer.CreateJoinRoom(txtWorldID.Text, "public", true, new Dictionary<string, string>(), new Dictionary<string, string>());

					con.Send("init");
					con.OnMessage += new MessageReceivedEventHandler(handlemsg);
					isConnected = true;
					btnConnect.Text = "Disconnect";
				}
				catch
				{
					MessageBox.Show("Error!");
				}
			}
			else
			{
				con.Send("say", "[OpenBot] Disconnected!");
				con.Disconnect();
				isConnected = false;
				btnConnect.Text = "Connect!";
			}
		}

		// the message handler will receive messages here!
		// if any events possible from the protocol, then it will be quickly signed here.
		//
		// EXAMPLE:  if (m.Type == "say")
		//           {
		//               if (m.GetString(1) == "!ping") con.Send("say", "[OpenBot] Pong!");
		//           }
		//
		private void handlemsg(object sender, PlayerIOClient.Message m)
		{
			if (m.Type == "say")
			{
				// add the rest commands with
				//
				// else if (m.GetString(1) == "!mycommand")
				// {
				//     con.Send("say", "[OpenBot] My command! OH NO!");
				// }
				//
				// You can also make a seperate class file to shorten your "else if" condition AKA command script.
				//
				if (m.GetString(1) == "!ping")
				{
					con.Send("say", "[OpenBot] Pong!");
					Thread.Sleep(100);
					con.Send("say", "[OpenBot] ...did I just said that?");
				}
				else if (m.GetString(1) == "!github")
				{
					con.Send("say", "[OpenBot] Get this bot at https://github.com/skull111-co/openbot");
				}
				else if (m.GetString(1) == "!help")
				{
					con.Send("say", "[OpenBot] !github, !ping, !help");
					// Make sure to include your command after implementing to the bot.
					// If you don't want to, then this is counted as an "easter egg".
				}
				else if (m.GetString(1) == "!ignoresnake")
				{
					bool enabled = false;
					foreach (int id in ignoresnakers)
					{
						if (id == m.GetInt(0))
						{
							enabled = true;
							List<int> list = new List<int>();
							list = ignoresnakers.ToList();
							list.Remove(m.GetInt(0));
							ignoresnakers = list.ToArray();
						}
					}

					if (!enabled)
					{
						List<int> list = new List<int>(); 
						list = ignoresnakers.ToList();
						list.Add(m.GetInt(0));
						ignoresnakers = list.ToArray();
						con.Send("pm", m.GetInt(0), "[OpenBot] The snake will ignore you.");
					}
					else
					{
						con.Send("pm", m.GetInt(0), "[OpenBot] The snake will no longer ignore you.");
					}
				}
			}
			else if (m.Type == "init")
			{
				con.Send("say", "[OpenBot] Joined!");
				con.Send("init2"); // joined successfully
			}
			else if (m.Type == "b")
			{
				if (!snake) return;

				foreach (int id in ignoresnakers)
				{
					if (id == m.GetInt(4))
					{
						return;
					}
				}

				if (m.GetInt(3) == Mineral.Green) Mineral.PlaceBlock(con, Mineral.Colors.Red, m.GetInt(0), m.GetInt(1), m.GetInt(2));
				else if (m.GetInt(3) == Mineral.Red) Mineral.PlaceBlock(con, Mineral.Colors.None, m.GetInt(0), m.GetInt(1), m.GetInt(2));
			}
		}

		private void btnToggle_Click(object sender, EventArgs e)
		{
			if (!snake)
			{
				snake = true;
				btnToggle.BackColor = Color.Green;
			}
			else
			{
				snake = false;
				btnToggle.BackColor = Color.Red;
			}
		}
	}
}
