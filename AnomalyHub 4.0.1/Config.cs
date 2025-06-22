using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace AnomalyHub
{
    public class Config : IConfig
    {
        [Description("Plugin is enabled?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Debug(console output)?")]
        public bool Debug { get; set; } = false;
        [Description("Chaos Insurgency spawn, CASSIE message is enabled?")]
        public bool CIspawnAnnounce { get; set; } = true;
        [Description("Chaos Insurgency spawn, CASSIE message")]
        public string CIspawnMessage { get; set; } = "Chaos Insurgency HasEntered from Gate Alpha";
        [Description("Chaos Insurgency spawn, CASSIE message sending delay(1 = 1 second)")]
        public float CIannoDelay { get; set; } = 1;
        [Description("Chaos Insurgency spawn lights is enabled?")]
        public bool CIspawnLight { get; set; } = true;
        [Description("Chaos Insurgency spawn light color. I prefer do not type 10 or higher")]
        public Dictionary<int, Color> CIspawnLightColor { get; set; } = new Dictionary<int, Color>()
        {
            { 1, new Color(0f,1f,0f) },
        };
        [Description("Nine Tailed Fox spawn lights is enabled?")]
        public bool NTFspawnLight { get; set; } = true;
        [Description("Nine Tailed Fox spawn light color. I prefer do not type 10 or higher")]
        public Dictionary<int, Color> NTFspawnLightColor { get; set; } = new Dictionary<int, Color>()
        {
            { 1, new Color(0f,0f,1f) },
        };
        [Description("UIU and SH spawn lights is enabled?(UIU and SerpentsHand plugin connection)")]
        public bool UIUandSHLight { get; set; } = true;
        [Description("UIU and SH spawn light color. I prefer do not type 10 or higher")]
        public Dictionary<int, Color> UIUandSHspawnLightColor { get; set; } = new Dictionary<int, Color>()
        {
            { 1, new Color(1f,0f,0f) },
        };
        [Description("Round start CASSIE message.")]
        public string RoundStartMessage { get; set; } = "pitch_0.2 .g4 .g4 pitch_0.9 The Site is Experiencing Many Keter and Euclid Level SCP Containment Breaches. Full Site Lock down Initiated. pitch_1.0";
        [Description("Round start red light time. Default is 30 second.")]
        public ushort RoundStartLight { get; set; } = 30;
        [Description("Round ending AutoFF is enabled?")]
        public bool AutoFF { get; set; } = true;
        [Description("AutoFF broadcast message.")]
        public string AutoFFBroadcast { get; set; } = "FriendlyFire Active.";
        [Description("AutoFF broadcast duration. Default is 5 second.")]
        public ushort AutoFFbroadcastDur { get; set; } = 5;
        [Description("Player join message. Type '%player%' for player name.")]
        public string EnterBroadcast { get; set; } = "Welcome to our server %player%";
        [Description("Can other players see join message?")]
        public bool EnterBroadcastShow { get; set; } = true;
        [Description("Player join message duration.")]
        public ushort EnterBroadcastDur { get; set; } = 10;
        [Description("Granade is allowed?")]
        public bool AntiBomb { get; set; } = false;
        [Description("Micro is allowed?")]
        public bool AntiMicro { get; set; } = false;
        [Description("Delayed broadcast is enabled?")]
        public bool AutoBroadcast { get; set; } = true;
        [Description("Delayed broadcast message")]
        public string AutoBroadcastME { get; set; } = "join our discord server! discord.gg/discord";
        [Description("Delayed broadcast delay time. Default is 300 second(5 minutes)")]
        public float AutoBroadcastDur { get; set; } = 300;
        [Description("You want to use default HP's in the game?")]
        public bool DefaultHP { get; set; } = false;
        [Description("You want to use default AHP's in the game?")]
        public bool DefaultAHP { get; set; } = true;
        [Description("SCP-096 new AHP")]
        public ushort SCP096newAHP { get; set; } = 0;
        [Description("SCP-106 new AHP")]
        public ushort SCP106newAHP { get; set; } = 0;
        [Description("SCP-173 new AHP")]
        public ushort SCP173newAHP { get; set; } = 0;
        [Description("SCP-939 new AHP")]
        public ushort SCP939newAHP { get; set; } = 0;
        [Description("SCP-049 new AHP")]
        public ushort SCP049newAHP { get; set; } = 0;
        [Description("SCP-096 new HP")]
        public ushort SCP096newHP { get; set; } = 2500;
        [Description("SCP-106 new HP")]
        public ushort SCP106newHP { get; set; } = 2200;
        [Description("SCP-173 new HP")]
        public ushort SCP173newHP { get; set; } = 4000;
        [Description("SCP-939 new HP")]
        public ushort SCP939newHP { get; set; } = 2700;
        [Description("SCP-049 new HP")]
        public ushort SCP049newHP { get; set; } = 2200;

    }
}