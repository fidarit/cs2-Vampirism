using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using System;
using System.Linq;
using VampirismCS2.Models;

namespace VampirismCS2.Controllers
{
    internal class DamageController : BaseController
    {
        public DamageController(Plugin plugin) : base(plugin)
        {
            plugin.RegisterEventHandler<EventPlayerHurt>(EventPlayerHurtHandler, HookMode.Pre);
            plugin.RegisterEventHandler<EventPlayerDeath>(EventPlayerDeathHandler, HookMode.Post);
        }

        private HookResult EventPlayerDeathHandler(EventPlayerDeath eventInfo, GameEventInfo gameEventInfo)
        {
            var attacker = eventInfo.Attacker;
            var victim = eventInfo.Userid;

            var multiplier = TryGetMultiplier(attacker, victim, (HitGroup_t)eventInfo.Hitgroup);
            if (multiplier == null)
                return HookResult.Continue;

            var heal = (int)(eventInfo.DmgHealth * multiplier);

            Heal(attacker, heal);

            return HookResult.Continue;

        }

        private HookResult EventPlayerHurtHandler(EventPlayerHurt eventInfo, GameEventInfo gameEventInfo)
        {
            var attacker = eventInfo.Attacker;
            var victim = eventInfo.Userid;

            var multiplier = TryGetMultiplier(attacker, victim, (HitGroup_t)eventInfo.Hitgroup);
            if (multiplier == null)
                return HookResult.Continue;

            var heal = (int)(eventInfo.DmgHealth * multiplier);

            Heal(attacker, heal);

            return HookResult.Continue;
        }

        private void Heal(CCSPlayerController player, int heal)
        {
            var playerPawn = player.PlayerPawn.Value;

            playerPawn.Health = Math.Clamp(playerPawn.Health + heal, 0, playerPawn.MaxHealth);

            Utilities.SetStateChanged(playerPawn, "CBaseEntity", "m_iHealth");
        }

        private float? TryGetMultiplier(CCSPlayerController attacker, CCSPlayerController victim, HitGroup_t hitGroup)
        {
            if (attacker == null || !attacker.IsValid)
                return null;

            if (victim == null || !victim.IsValid)
                return null;

            var permission = TryGetPermissionConfig(attacker);
            if (permission == null || !permission.Enabled)
                return null;

            if (permission.OnHeadShotOnly && hitGroup != HitGroup_t.HITGROUP_HEAD)
                return null;

            if (permission.OnKillOnly == (victim.PlayerPawn.Value.LifeState == (byte)LifeState_t.LIFE_ALIVE))
                return null;

            return permission.Multiplier;
        }

        private PermissionData TryGetPermissionConfig(CCSPlayerController playerController)
        {
            Config.Permissions.TryGetValue("*", out var any);

            return Config.Permissions
                .Where(t => AdminManager.PlayerHasPermissions(playerController, t.Key))
                .Select(t => t.Value)
                .Append(any)
                .FirstOrDefault();
        }
    }
}
