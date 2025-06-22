using Exiled.Events.EventArgs.Server;
using System;
using MEC;
using UnityEngine;
using Cassie = Exiled.API.Features.Cassie;
using Map = Exiled.API.Features.Map;
using Server = Exiled.API.Features.Server;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using PlayerRoles;

namespace AnomalyHub
{
    public class EventHandlers
    {
        bool blue = false;
        bool red = false;
        bool gr = false;
        int count = 0;
        public void RoundEndEvent(RoundEndedEventArgs ev)
        {
            try
            {
                if (Plugin.Instance.Config.AutoFF)
                {
                    Map.Broadcast(Plugin.Instance.Config.AutoFFbroadcastDur, Plugin.Instance.Config.AutoFFBroadcast);
                    Server.FriendlyFire = true; Log.Debug("Round Ending, AutoFF is now Active.");
                }
                Timing.KillCoroutines(autoBroCoroutine);
            }
            catch (Exception e)
            {
                Log.Error("RoundEndedEventArgs: " + e.Message.ToString());
            }
        }

        public void PlayerEnter(VerifiedEventArgs ev)
        {
            if (ev.Player == null)
            {
                Log.Debug("Null");
                return;
            }
            if (Plugin.Instance.Config.EnterBroadcastShow)
            {
                Map.Broadcast(Plugin.Instance.Config.EnterBroadcastDur, Plugin.Instance.Config.EnterBroadcast.Replace("%player%", ev.Player.DisplayNickname));
                return;
            }
            ev.Player.Broadcast(Plugin.Instance.Config.EnterBroadcastDur, Plugin.Instance.Config.EnterBroadcast.Replace("%player%", ev.Player.DisplayNickname));
            Log.Debug(ev.Player.DisplayNickname + " Joined this Server, broadcast will be show shortly.");
        }

        public void PlayerWait()
        {
            if (Plugin.Instance.Config.AutoFF)
            {
                Server.FriendlyFire = false;
                Log.Debug("Players Waiting, AutoFF is now Disabled.");
            }
        }

        public void AntiItems(ChangingItemEventArgs ev)
        {
            try
            {
                if (Plugin.Instance.Config.AntiBomb && !(ev.Item == null))
                {
                    if (ev.Item.Type == ItemType.GrenadeHE)
                    {
                        ev.Item.Destroy(); Log.Debug("Grenade deleted.");
                    }
                }
                if (Plugin.Instance.Config.AntiMicro && !(ev.Item == null))
                {
                    if (ev.Item.Type == ItemType.MicroHID)
                    {
                        ev.Item.Destroy(); Log.Debug("Micro deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ChangingItemEventArgs: " + ex.Message.ToString());
            }
        }

        public IEnumerator<float> light(bool ci, bool sh, bool ntf)
        {
            while (!Round.IsEnded)
            {
                if (count > 4)
                {
                    count = 0;
                    break;
                }
                count++;
                if (ci)
                {
                    if (gr)
                    {
                        Map.ChangeLightsColor(Color.clear);
                        gr = false;
                    }
                    else
                    {
                        Color color = Plugin.Instance.Config.CIspawnLightColor.TryGetValue(1, out Color col) ? col : Color.clear;
                        Map.ChangeLightsColor(color);
                        gr = true;
                        Log.Debug($"Chaos Insurgency Lights: R={color.r}, G={color.g}, B={color.b}, A={color.a}");
                        if (color.g > 2 || color.r > 2 || color.b > 2)
                        {
                            Log.Warn("Chaos Insurgency Spawn Light Brightness level is too high!");
                        }
                    }
                }
                else if (sh)
                {
                    if (red)
                    {
                        Map.ChangeLightsColor(Color.clear);
                        red = false;
                    }
                    else
                    {
                        Color color = Plugin.Instance.Config.UIUandSHspawnLightColor.TryGetValue(1, out Color col) ? col : Color.clear;
                        Map.ChangeLightsColor(color);
                        if (color.r > 10 || color.g > 10 || color.b > 10)
                        {
                            Log.Warn("UIU or SH Spawn Light Brightness level is too high!");
                        }
                        red = true;
                    }
                }
                if (ntf)
                {
                    if (blue)
                    {
                        Map.ChangeLightsColor(Color.clear);
                        blue = false;
                    }
                    else
                    {
                        Color color = Plugin.Instance.Config.NTFspawnLightColor.TryGetValue(1, out Color col) ? col : Color.clear;
                        Map.ChangeLightsColor(color);
                        blue = true;
                        if (color.g > 2 || color.r > 2 || color.b > 2)
                        {
                            Log.Warn("NTF Spawn Light Brightness level is too high!");
                        }
                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
            Map.ChangeLightsColor(Color.clear);
        }

        public void Spawn(RespawningTeamEventArgs ev)
        {
            try
            {
                var ntf = ev.Wave.SpawnableFaction == Exiled.API.Enums.SpawnableFaction.NtfWave;
                var ntfmini = ev.Wave.SpawnableFaction == Exiled.API.Enums.SpawnableFaction.NtfMiniWave;
                var chaos = ev.Wave.SpawnableFaction == Exiled.API.Enums.SpawnableFaction.ChaosWave;
                var chaosmini = ev.Wave.SpawnableFaction == Exiled.API.Enums.SpawnableFaction.ChaosMiniWave;
                if (ntf || ntfmini)
                {
                    if (Plugin.Instance.Config.NTFspawnLight)
                    {
                        Timing.RunCoroutine(light(false, false, true)); Log.Debug("NTF spawned.");
                    }
                }
                if (chaos || chaosmini)
                {
                    if (Plugin.Instance.Config.CIspawnLight)
                    {
                        Timing.RunCoroutine(light(true, false, false)); Log.Debug("Chaos Insurgency spawned.");
                    }
                    if (Plugin.Instance.Config.CIspawnAnnounce)
                    {
                        Timing.CallDelayed(Plugin.Instance.Config.CIannoDelay, () =>
                        {
                            Cassie.Clear();
                            Cassie.Message(Plugin.Instance.Config.CIspawnMessage,true,true,true);
                        });
                    }
                }
                if (!(ntf || ntfmini || chaos || chaosmini))
                {
                    if (Plugin.Instance.Config.UIUandSHLight)
                    {
                        Timing.RunCoroutine(light(false, true, false)); Log.Debug("UIU or SH spawned.");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("RespawningTeamEventArgs: " + e.Message.ToString());
            }
        }
        public void NewHealths(SpawningEventArgs ev)
        {
            try
            {
                if (!Plugin.Instance.Config.DefaultHP)
                {
                    Dictionary<RoleTypeId, float> RoleHealth = new Dictionary<RoleTypeId, float>()
                    {
                        { RoleTypeId.Scp096, Plugin.Instance.Config.SCP096newHP },
                        { RoleTypeId.Scp106, Plugin.Instance.Config.SCP106newHP },
                        { RoleTypeId.Scp173, Plugin.Instance.Config.SCP173newHP },
                        { RoleTypeId.Scp939, Plugin.Instance.Config.SCP939newHP },
                        { RoleTypeId.Scp049, Plugin.Instance.Config.SCP049newHP }
                    };

                    if (RoleHealth.ContainsKey(ev.Player.Role))
                    {
                        ev.Player.MaxHealth = RoleHealth[ev.Player.Role]; Log.Debug("New SCP healts been set!");
                    }
                    
                }
                if (!Plugin.Instance.Config.DefaultAHP)
                {
                    Dictionary<RoleTypeId, float> RoleAHP = new Dictionary<RoleTypeId, float>()
                    {
                        { RoleTypeId.Scp096, Plugin.Instance.Config.SCP096newAHP },
                        { RoleTypeId.Scp106, Plugin.Instance.Config.SCP106newAHP },
                        { RoleTypeId.Scp173, Plugin.Instance.Config.SCP173newAHP },
                        { RoleTypeId.Scp939, Plugin.Instance.Config.SCP939newAHP },
                        { RoleTypeId.Scp049, Plugin.Instance.Config.SCP049newAHP }
                    };
                    if (RoleAHP.ContainsKey(ev.Player.Role))
                    {
                        ev.Player.ArtificialHealth = RoleAHP[ev.Player.Role]; Log.Debug("New SCP ahp's been set!");
                    }
                    
                }
            }
            catch (Exception e)
            {
                Log.Error("SpawningEventArgs: " + e.Message.ToString());
            }
        }

        CoroutineHandle autoBroCoroutine;
        public IEnumerator<float> AutoBro()
        {
            Map.Broadcast(5, Plugin.Instance.Config.AutoBroadcastME); Log.Debug("AutoBroadcast sent!");

            yield return Timing.WaitForSeconds(Plugin.Instance.Config.AutoBroadcastDur);
        }

        public void OnRoundStarted()
        {
            try
            {
                Map.ChangeLightsColor(Color.red); Log.Debug("Round started. Color set to red!");
                Cassie.Message(Plugin.Instance.Config.RoundStartMessage, true, true, true);
                Timing.CallDelayed(Plugin.Instance.Config.RoundStartLight, () =>
                {
                    Map.ChangeLightsColor(Color.clear); Log.Debug("Red Color Cleared.");
                });
                if (Plugin.Instance.Config.AutoBroadcast)
                {
                    Timing.RunCoroutine(AutoBro());
                }
            }

            catch (Exception e)
            {
                Log.Error("OnRoundStarted: " + e.Message.ToString());
            }
        }
    }
}