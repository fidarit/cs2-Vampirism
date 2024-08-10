using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using VampirismCS2.Models;
using System;
using System.Linq;

namespace VampirismCS2.Controllers
{
    internal class DamageController : BaseController
    {
        public DamageController(Plugin plugin) : base(plugin)
        {
            plugin.RegisterEventHandler<EventPlayerHurt>(EventPlayerHurtHandler, HookMode.Pre);
        }

        private HookResult EventPlayerHurtHandler(EventPlayerHurt eventInfo, GameEventInfo gameEventInfo)
        {
            var attacker = eventInfo.Attacker;
            var victim = eventInfo.Userid?.PlayerPawn?.Value;

            if (eventInfo.DmgHealth <= 0)
                return HookResult.Continue;

            if (attacker == null || !attacker.IsValid)
                return HookResult.Continue;

            if (victim == null || !victim.IsValid)
                return HookResult.Continue;

            var multiplier = TryGetMultiplier(attacker, eventInfo);
            if (multiplier == null)
                return HookResult.Continue;

            var heal = (int)(eventInfo.DmgHealth * multiplier);

            attacker.Health = Math.Clamp(attacker.Health + heal, 0, attacker.MaxHealth);

            return HookResult.Continue;
        }

        private float? TryGetMultiplier(CCSPlayerController playerController, EventPlayerHurt eventInfo)
        {
            var permission = TryGetPermissionConfig(playerController);
            if (permission == null || !permission.Enabled)
                return null;

            if (permission.OnHeadShotOnly && eventInfo.Hitgroup != (int)HitGroup_t.HITGROUP_HEAD)
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
