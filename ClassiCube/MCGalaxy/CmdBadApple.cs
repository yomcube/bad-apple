//reference System.Drawing.dll
using System;
using System.Drawing;
using System.Threading;
using MCGalaxy;
using MCGalaxy.Commands;
using MCGalaxy.Commands.Maintenance;
using BlockID = System.Byte;

public class CmdBadApple : Command
{
	public override string name { get { return "BadApple"; } }
	public override string shortcut { get { return ""; } }
	public override string type { get { return "other"; } }
	public override bool museumUsable { get { return true; } }
	public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
	
	public int delay = 53; // Sleep delay in milliseconds
	public int limit = 6573; // Frame limit; max is 6573
	public int step = 2; // Frame step
	
	Player player;
	Thread t;
	public override void Use(Player p, string message)
	{
		// Stop thread if it exists
		if (message == "stop")
		{
			p.Message("Stopping Bad Apple...");
			if (t != null && t.IsAlive)
			{
				t.Abort();
			}
			return;
		}
		// Clear the map completely with blue wool (extended block)
		if (message == "clear") {
			p.Message("Clearing...");
			Level level = p.level;
			for (int i = 0; i < level.blocks.Length; i++)
				level.blocks[i] = 58;
			p.SendRawMap(p.level, level);
			return;
		}
		
		// Set player object outside of method to avoid
		// passing an argument to the thread
		player = p;
		
		if (t != null && t.IsAlive)
		{
			t.Abort(); // Abort thread before starting another
		}
		t = new Thread(DoBadApple);
		t.Start();
		p.Message("Starting Bad Apple !!");
	}

	void DoBadApple()
	{
		for (int i = 1; i < limit; i += step)
		{
			player.SendCpeMessage(CpeMessageType.BottomRight1, i.ToString());
			// Skip duplicate frames
			if (i > 1 && i < 44)
			{
				Thread.Sleep(delay);
				continue;
			}

			ushort w = 60, h = 45;

			Level lvl = player.level;
			lvl.Width = w;
			lvl.Height = h;


			Image img = Image.FromFile("BadApple/frames_png/f_" + i.ToString("D4") + ".png");
			Bitmap bmp = new Bitmap(img, w, h);
			
			for (ushort x = 0; x < bmp.Width; x++)
			{
				for (ushort y = 0; y < bmp.Height; y++)
				{
					Color c = bmp.GetPixel(x, y);
					BlockID block;

					if (c.R < 64) block = 49;
					else if (c.R > 63 && c.R < 128) block = 34;
					else if (c.R > 127 && c.R < 192) block = 35;
					else block = 36;

					lvl.blocks[lvl.PosToInt(x, (ushort)(lvl.Height - 1 - y), 0)] = block;
				}
			}
			player.SendRawMap(player.level, lvl);
			Thread.Sleep(delay);
		}

		player.Message("&aDone!");
	}

	public override void Help(Player p)
	{
		p.Message("/BadApple - Does stuff. Example command.");
	}
}