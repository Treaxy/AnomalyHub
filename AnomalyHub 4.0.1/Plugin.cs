using System;
using Exiled.API.Features;
using SRV = Exiled.Events.Handlers.Server;

namespace AnomalyHub
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "Treaxy";

        public override string Name => "AnomalyHub";

        public override Version Version => new Version(4, 0, 1);

        public override Version RequiredExiledVersion => new Version(9, 3, 0);

        public override string Prefix => Name;

        public static Plugin Instance;

        private EventHandlers _handlers;

        public override void OnEnabled()
        {
            Instance = this;

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Instance = null;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            try
            {
                _handlers = new EventHandlers();
                SRV.RoundEnded += _handlers.RoundEndEvent;
                Exiled.Events.Handlers.Player.Verified += _handlers.PlayerEnter;
                Exiled.Events.Handlers.Player.Spawning += _handlers.NewHealths;
                Exiled.Events.Handlers.Player.ChangingItem += _handlers.AntiItems;
                SRV.RespawningTeam += _handlers.Spawn;
                SRV.WaitingForPlayers += _handlers.PlayerWait;
                SRV.RoundStarted += _handlers.OnRoundStarted;
                base.OnEnabled();
            }
            catch (Exception ex)
            {
                Log.Error("RegisterEvent: " + ex.Message.ToString());
            }
        }

        private void UnregisterEvents()
        {
            try
            {
                SRV.RoundEnded -= _handlers.RoundEndEvent;
                Exiled.Events.Handlers.Player.Verified -= _handlers.PlayerEnter;
                Exiled.Events.Handlers.Player.Spawning -= _handlers.NewHealths;
                Exiled.Events.Handlers.Player.ChangingItem -= _handlers.AntiItems;
                SRV.RespawningTeam -= _handlers.Spawn;
                SRV.WaitingForPlayers -= _handlers.PlayerWait;
                SRV.RoundStarted -= _handlers.OnRoundStarted;
                _handlers = null;
                base.OnDisabled();
            }
            catch (Exception ex)
            {
                Log.Error("UnRegisterEvent: " + ex.Message.ToString());
            }
        }
    }

}