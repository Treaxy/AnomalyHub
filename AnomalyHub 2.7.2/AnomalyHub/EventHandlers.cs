using Exiled.Events.EventArgs.Server;
using System;
using MEC;
using Respawning;
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
        bool gr = false;
        int count = 0;
        public void RoundEndEvent(RoundEndedEventArgs ev)
        {
            try
            {
                if (Plugin.Instance.Config.AutoFF)
                {
                    Map.Broadcast(Plugin.Instance.Config.AutoFFbroadcastDur, Plugin.Instance.Config.AutoFFBroadcast);
                    Server.FriendlyFire = true;
                    Log.Info("AutoFF is now OPENED!!");
                }
                Timing.KillCoroutines(autoBroCoroutine);
            }
            catch (Exception e)
            {
                Log.Error("RoundEndedEventArgs: "+e.Message.ToString());
            }
        }

        public void PlayerEnter(VerifiedEventArgs ev)
        {
            if (ev.Player == null)
            {
                Log.Info("Null");
                return;
            }
            if (Plugin.Instance.Config.EnterBroadcastShow)
            {
                Map.Broadcast(Plugin.Instance.Config.EnterBroadcastDur, Plugin.Instance.Config.EnterBroadcast.Replace("%player%", ev.Player.DisplayNickname));
                return;
            }
            ev.Player.Broadcast(Plugin.Instance.Config.EnterBroadcastDur, Plugin.Instance.Config.EnterBroadcast.Replace("%player%", ev.Player.DisplayNickname));
        }

        public void PlayerWait()
        {
            if (Plugin.Instance.Config.AutoFF)
            {
                Server.FriendlyFire = false;
                Log.Info("AutoFF is now CLOSED!!");
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
                        ev.Item.Destroy();
                    }
                }
                if (Plugin.Instance.Config.AntiMicro && !(ev.Item == null))
                {
                    if (ev.Item.Type == ItemType.MicroHID)
                    {
                        ev.Item.Destroy();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ChangingItemEventArgs: "+ex.Message.ToString());
            }
        }

        public void InstaKill(HurtingEventArgs ev)
        {
            try
            {
                if (Plugin.Instance.Config.SCP049insta && ev.Attacker.Role.Type == RoleTypeId.Scp049)
                {
                    ev.Player.Hurt(10000);
                }
                if (Plugin.Instance.Config.SCP106insta && ev.Attacker.Role.Type == RoleTypeId.Scp106)
                {
                    ev.Player.Hurt(10000);
                }
            }
            catch (Exception e)
            {
                Log.Error("HurtingEventArgs: "+e.Message.ToString());
            }
        }

        public IEnumerator<float> light(bool ci)
        {
            while (true)
            {
                count++;

                if (count == 5)
                {
                    Map.ChangeLightsColor(Color.clear);
                    count = 0;
                    break;
                }
                if (Round.IsEnded)
                {
                    break;
                }
                else if (ci)
                {
                    if (gr)
                    {
                        Map.ChangeLightsColor(Color.clear);
                        gr = false;
                    }
                    else
                    {
                        Map.ChangeLightsColor(Color.green);
                        gr = true;
                    }
                }
                else if (blue)
                {
                    Map.ChangeLightsColor(Color.clear);
                    blue = false;
                }
                else
                {
                    Map.ChangeLightsColor(Color.blue);
                    blue = true;
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }

        public void Spawn(RespawningTeamEventArgs ev)
        {
            try
            {
                if (ev.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                {
                    if (Plugin.Instance.Config.NTFspawnLight)
                    {
                        blue = true;
                        Timing.RunCoroutine(light(false));
                    }
                }
                if (ev.NextKnownTeam == SpawnableTeamType.ChaosInsurgency)
                {
                    if (Plugin.Instance.Config.CIspawnLight)
                    {
                        blue = false;
                        Timing.RunCoroutine(light(true));
                    }
                    if (Plugin.Instance.Config.CIspawnAnnounce)
                    {
                        Timing.CallDelayed(Plugin.Instance.Config.CIannoDelay, () =>
                        {
                            Cassie.Message(Plugin.Instance.Config.CIspawnMessage, true, true, true);
                        });
                    }
                }
                if (!(ev.NextKnownTeam == SpawnableTeamType.NineTailedFox || ev.NextKnownTeam == SpawnableTeamType.ChaosInsurgency))
                {
                    if (Plugin.Instance.Config.UIUspawnLight)
                    {
                        blue = true;
                        Timing.RunCoroutine(light(false));
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("RespawningTeamEventArgs: "+e.Message.ToString());
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
                        ev.Player.MaxHealth = RoleHealth[ev.Player.Role];
                    }
                }
            }
            catch (Exception e) 
            {
                Log.Error("SpawningEventArgs: "+e.Message.ToString());
            }
        }

        CoroutineHandle autoBroCoroutine;
        public IEnumerator<float> AutoBro()
        {
            Map.Broadcast(5, Plugin.Instance.Config.AutoBroadcastME);

            yield return Timing.WaitForSeconds(Plugin.Instance.Config.AutoBroadcastDur);
        }

        public void OnRoundStarted()
        {
            try
            {
                Map.ChangeLightsColor(Color.red);
                Cassie.Message(Plugin.Instance.Config.RoundStartMessage, true, true, true);
                Timing.CallDelayed(Plugin.Instance.Config.RoundStartLight, () =>
                {
                    Map.ChangeLightsColor(Color.clear);
                });
                if (Plugin.Instance.Config.AutoBroadcast)
                {
                    Timing.RunCoroutine(AutoBro());
                }
            }

            catch (Exception e)
            {
                Log.Error("OnRoundStarted: "+e.Message.ToString());
            }
        }
    }
}