﻿using Develeon64.SpotifyPlugin.Models;
using Develeon64.SpotifyPlugin.Utils;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;

namespace Develeon64.SpotifyPlugin.ViewModels {
	internal class PauseActionConfigViewModel : ISerializableConfigViewModel {
		public readonly PluginAction _action;
		public PauseActionConfigModel Configuration { get; set; }
		ISerializableConfiguration ISerializableConfigViewModel.SerializableConfiguration => Configuration;

		public PauseActionConfigViewModel (PluginAction action) {
			this._action = action;
			this.Configuration = PauseActionConfigModel.Deserialize(this._action.Configuration);
		}

		public void SetConfig () {
			this._action.ConfigurationSummary = Configuration.Mode switch {
				EMode.Activate => PluginLanguageManager.PluginStrings.PauseActionModeActivate,
				EMode.Deactivate => PluginLanguageManager.PluginStrings.PauseActionModeDeactivate,
				_ => PluginLanguageManager.PluginStrings.PauseActionModeToggle,
			};
			this._action.Configuration = this.Configuration.Serialize();
		}

		public bool SaveConfig () {
			try {
				this.SetConfig();
				MacroDeckLogger.Info(PluginInstance.Main, $"{this.GetType().Name}: config saved");
			}
			catch (Exception e) {
				MacroDeckLogger.Error(PluginInstance.Main, $"{this.GetType().Name}: Error while saving Config: {e.Message}\n{e.StackTrace}");
			}
			return true;
		}
	}
}
