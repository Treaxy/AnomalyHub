using Exiled.API.Interfaces;

namespace AnomalyHub
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        public string CIspawnMessage { get; set; } = "Chaos Insurgency HasEntered from Gate Alpha";

        public bool CIspawnAnnounce { get; set; } = true;

        public float CIannoDelay { get; set; } = 1;

        public bool CIspawnLight { get; set; } = true;

        public bool NTFspawnLight { get; set; } = true;

        public bool UIUspawnLight { get; set; } = true;

        public bool SHspawnLight { get; set; } = false;

        public string RoundStartMessage { get; set; } = "pitch_0.2 .g4 .g4 pitch_0.9 The Site is Experiencing Many Keter and Euclid Level SCP Containment Breaches. Full Site Lock down Initiated. pitch_1.0";

        public bool AutoFF { get; set; } = true;

        public ushort RoundStartLight { get; set; } = 30;

        public string AutoFFBroadcast { get; set; } = "FriendlyFire Active.";

        public ushort AutoFFbroadcastDur { get; set; } = 5;

        public string EnterBroadcast { get; set; } = "Welcome to our server %player%";

        public bool EnterBroadcastShow { get; set; } = true;

        public ushort EnterBroadcastDur { get; set; } = 10;

        public bool AntiBomb { get; set; } = false;

        public bool AntiMicro { get; set; } = false;

        public string AutoBroadcastME { get; set; } = "join our discord server! discord.gg/discord";

        public bool AutoBroadcast { get; set; } = true;

        public float AutoBroadcastDur { get; set; } = 300;

        public bool DefaultHP {  get; set; } = false;

        public bool DefaultAHP { get; set; } = true;

        public ushort SCP096newAHP { get; set; } = 0;

        public ushort SCP106newAHP { get; set; } = 0;

        public ushort SCP173newAHP { get; set; } = 0;

        public ushort SCP939newAHP { get; set; } = 0;

        public ushort SCP049newAHP { get; set; } = 0;

        public ushort SCP096newHP { get; set; } = 2500;

        public ushort SCP106newHP { get; set; } = 2200;

        public ushort SCP173newHP { get; set; } = 4000;

        public ushort SCP939newHP { get; set; } = 2700;

        public ushort SCP049newHP { get; set; } = 2200;

        public bool SCP106insta { get; set; } = false;

        public bool SCP049insta { get; set; } = false;

    }
}