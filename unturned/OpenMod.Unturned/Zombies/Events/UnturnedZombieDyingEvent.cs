﻿using SDG.Unturned;
using UnityEngine;

namespace OpenMod.Unturned.Zombies.Events
{
    public class UnturnedZombieDyingEvent : UnturnedZombieDamagingEvent
    {
        public UnturnedZombieDyingEvent(UnturnedZombie zombie, ushort damageAmount, Vector3 ragdoll, ERagdollEffect ragdollEffect, bool trackKill, bool dropLoot, EZombieStunOverride stunOverride) : base(zombie, damageAmount, ragdoll, ragdollEffect, trackKill, dropLoot, stunOverride)
        {

        }
    }
}
