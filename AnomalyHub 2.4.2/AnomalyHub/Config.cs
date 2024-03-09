﻿using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace AnomalyHub
{
    public class Config : IConfig
    {
        [Description("Plugin is Enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Plugin Debug is Enabled?")]
        public bool Debug { get; set; } = false;

        [Description("Chaos spawn message (cassie style)")]
        public string CIspawnMessage { get; set; } = "Chaos Insurgency HasEntered from Gate Alpha";

        [Description("Chaos Insurgency spawn Announcement(cassie) is Enabled?")]
        public bool CIspawnAnnounce { get; set; } = true;

        [Description("Chaos Insurgency spawn Announcement(cassie) delay second")]
        public float CIannoDelay { get; set; } = 1;

        [Description("Chaos Insurgency spawn Lights(green lights) is Enabled?")]
        public bool CIspawnLight { get; set; } = true;

        [Description("Nine Tailed Fox spawn Lights(blue lights) is Enabled?")]
        public bool NTFspawnLight { get; set; } = true;

        [Description("UIU plugin spawn Lights(blue lights) is Enabled?")]
        public bool UIUspawnLight { get; set; } = true;

        [Description("Round start message (cassie style)")]
        public string RoundStartMessage { get; set; } = "pitch_0.2 .g4 .g4 pitch_0.9 The Site is Experiencing Many Keter and Euclid Level SCP Containment Breaches. Full Site Lock down Initiated. pitch_1.0";

        [Description("Round is ended Friendly Fire will be open (true or false).")]
        public bool AutoFF { get; set; } = true;

        [Description("Round start red lights close time(secondes)")]
        public ushort RoundStartLight { get; set; } = 30;

        [Description("Round ended Friendly Fire broadcast message")]
        public string AutoFFBroadcast { get; set; } = "FriendlyFire Active.";

        [Description("Round ended Friendly Fire broadcast duration")]
        public ushort AutoFFbroadcastDur { get; set; } = 5;

        [Description("Player enter broadcast (%player% is Player name).")]
        public string EnterBroadcast { get; set; } = "Welcome to our server %player%";

        [Description("Enter broadcast duration.")]
        public ushort EnterBroadcastDur { get; set; } = 10;
        [Description("SCP-096 new round start hp")]
        public float SCP096newHP { get; set; } = 2500;
        [Description("SCP-106 new round start hp")]
        public float SCP106newHP { get; set; } = 2200;
        [Description("SCP-173 new round start hp")]
        public float SCP173newHP { get; set; } = 4000;
        [Description("SCP-939 new round start hp")]
        public float SCP939newHP { get; set; } = 2700;
        [Description("SCP-049 new round start hp")]
        public float SCP049newHP { get; set; } = 2200;

    }
}
