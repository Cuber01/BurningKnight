using System;
using Lens.util.math;

namespace BurningKnight.util {
	public static class LoadScreenTitles {
		public static string Generate() {
			return titles[new Random().Next(titles.Length)];
		}
		
		private static string[] titles = {
			"Please stand by",
			"Press X",
			"Generating trouble",
			"Looking cool",
			"Loading...",
			"Get em!",
			"If you want to buy something, you need to have money",
			"Press / to open chat",
			"Generating generators",
			"Submitting an issue",
			"Terraforming mars",
			"To defeat a boss, shoot at it until it dies",
			"I think that knight wanted to say something",
			"Are we there yet?",
			"Generating secrets",
			"Hiding secrets",
			"I'm hungry",
			"/!\\ /!\\ /!\\",
			"Saving is important",
			"Cooling down",
			"Heating up",
			"Adding some drama",
			"You better get digging",
			"F2",
			"Generating generators",
			"Waking up",
			"Deleting the saves",
			"Preparing to start",
			"Looking for an excuse",
			"Automatically synchronizing cardinal grammeters",
			"Reducing sinusoidal repleneration",
			"Fromaging the bituminous spandrels",
			"Join our discord!",
			"Reticulating splines",
			"Calculating Math.PI",
			"Inventing the wheel",
			"Adding some oil",
			"Recruiting robot hamsters",
			"Generating buttons",
			"Installing deinstallers",
			"Thinking",
			"I like pizza",
			"That fight tho",
			"Be careful",
			"Almost alive",
			"Dog food",
			"Always wear dry socks",
			"Its your lucky day",
			"Press F to pay respect",
			"Let's do this",
			"Let's go",
			"YOOOOOO",
			"This is a joke mimic",
			"Go go go",
			"Делаем вид что это что-то значит",
			"Loading terrain",
			"Building terrain",
			"Googling",
			"Generating more enemies",
			"Sending help",
			"SOS",
			"Are we lost?",
			"Spooooky",
			"On fire",
			"It's magic time",
			"Settings things on fire",
			"Preparing to explode",
			"Installing linux",
			"Erasing data",
			"Generating a joke",
			"Attacking enemies does more damage, than not attacking",
			"Getting hit hurts",
			"Yeeeeet",
			"You get a higher chance to hit the enemy, if you aim",
			"Money is useful",
			"Dying is bad",
			"You can change the cursor in settings",
			"Fullscreen is dope"
		};
	}
}
