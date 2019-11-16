using System;
using System.Threading;
using BurningKnight.assets.items;
using BurningKnight.entity.creature.mob.castle;
using BurningKnight.level;
using BurningKnight.level.basement;
using BurningKnight.level.biome;
using BurningKnight.level.hall;
using BurningKnight.level.hub;
using BurningKnight.level.tile;
using BurningKnight.level.tutorial;
using BurningKnight.physics;
using BurningKnight.state;
using Lens.entity;
using Lens.util;
using Lens.util.file;
using Random = Lens.util.math.Random;

namespace BurningKnight.save {
	public class LevelSave : EntitySaver {
		private static int I;

		public override void Save(Area area, FileWriter writer, bool old) {
			SmartSave(area.Tagged[Tags.LevelSave], writer);
			var d = (old ? Run.LastDepth : Run.Depth);
			
			if (d > 0) {
				Run.LastSavedDepth = d;
				Log.Error($"Set run last saved depth to {Run.LastSavedDepth}");
			}
		}

		public override string GetPath(string path, bool old = false) {
			if (path.EndsWith(".lvl")) {
				return path;
			}
			
			return $"{path}level-{(old ? Run.LastDepth : Run.Depth)}.lvl";
		}

		private RegularLevel CreateLevel() {
			if (Run.Depth == -2) {
				return new TutorialLevel();
			}
			
			if (Run.Depth == -1) {
				return new BasementLevel();
			}
			
			if (Run.Depth == 0) {
				return new HallLevel();
			}
			
			return new RegularLevel(BiomeRegistry.GenerateForDepth(Run.Depth));
		}

		public static Biome BiomeGenerated;
		
		private bool GenerationThread(Area area) {
			var a = new Area();
		
			try {
				Items.GeneratedOnFloor.Clear();
				var level = CreateLevel();
				BiomeGenerated = level.Biome;
				a.Add(level);
				level.Generate();

				foreach (var e in a.Entities.ToAdd) {
					area.Add(e);
				}

				area.EventListener.Copy(a.EventListener);
				area.Entities.AddNew();
				I = 0;
			} catch (Exception e) {
				Log.Error(e);
				I++;

				a.Entities.AddNew();
				a.Destroy();
				a = new Area();
				Run.Level = null;

				if (I > 100) {
					Log.Error("Can't generate a level");

					var level = CreateLevel();
					BiomeGenerated = level.Biome;

					level.Width = 32;
					level.Height = 32;
					level.NoLightNoRender = false;
					level.DrawLight = false;
					
					a.Add(level);

					level.Setup();
					level.Fill(Tile.FloorA);
					level.TileUp();
					
					return false;
				}
				
				return GenerationThread(area);
			}

			BiomeGenerated = null;
			return true;
		}

		public override void Generate(Area area) {
			var done = false;
			var finished = false;
			var aborted = false;
			
			var thread = new Thread(() => {
				done = GenerationThread(area);
				finished = true;
			});
			
			Log.Debug("Level gen thread started");
			thread.Start();
			var i = 0;
			
			while (true) {
				Thread.Sleep(500);

				if (finished) {
					Log.Debug("Thread finished");
					break;
				}

				i++;

				if (i >= 10f) {
					Log.Debug("Thread took too long, aborting :(");
					thread.Abort();
					Physics.Destroy();
					Physics.Init();
					Random.Seed += "_";
					aborted = true;

					break;
				}
			}

			if (aborted) {
				Generate(area);
			} else {
				Log.Debug($"Generation done, took {i} cycles");
			}
		}

		public override FileHandle GetHandle() {
			return new FileHandle(SaveManager.SlotDir);
		}

		public override void Delete() {
			var handle = Run.Depth > 0 ? GetHandle() : new FileHandle(SaveManager.SaveDir);

			if (!handle.Exists()) {
				return;
			}
			
			foreach (var file in handle.ListFileHandles()) {
				if (file.Extension == ".lvl" || file.Extension == "lvl") {
					file.Delete();
				}
			}
		}

		public LevelSave() : base(SaveType.Level) {
			
		}
	}
}