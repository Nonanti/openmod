﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using OpenMod.API.Ioc;
using OpenMod.API.Prioritization;
using OpenMod.API.Users;
using OpenMod.Core.Helpers;
using OpenMod.Core.Users;
using SDG.Unturned;
using Steamworks;

namespace OpenMod.Unturned.Users
{
    [ServiceImplementation(Lifetime = ServiceLifetime.Singleton, Priority = Priority.Lowest)]
    public class UnturnedUserDirectory : IUnturnedUserDirectory
    {
        private readonly IUserManager m_UserManager;

        public UnturnedUserDirectory(IUserManager userManager)
        {
            m_UserManager = userManager;
        }

        public UnturnedUser GetUser(Player player)
        {
            return GetOnlineUsers().FirstOrDefault(d => d.Player.Player == player);
        }

        public UnturnedPendingUser GetPendingUser(SteamPending pending)
        {
            return GetPendingUsers().FirstOrDefault(d => d.SteamPending == pending);
        }

        public UnturnedUser FindUser(CSteamID steamId)
        {
            return GetOnlineUsers().FirstOrDefault(d => d.Player.SteamId == steamId);
        }

        public UnturnedPendingUser FindPendingUser(CSteamID steamId)
        {
            return GetPendingUsers().FirstOrDefault(d => d.SteamId == steamId);

        }

        public UnturnedUser FindUser(string searchString, UserSearchMode searchMode)
        {
            return AsyncHelper.RunSync(async () =>
            {
                try
                {
                    var result = await m_UserManager.FindUserAsync(KnownActorTypes.Player, searchString, searchMode);
                    return result as UnturnedUser;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        protected UnturnedUserProvider GetUnturnedUserProvider()
        {
            return (UnturnedUserProvider) m_UserManager.UserProviders.FirstOrDefault(d => d is UnturnedUserProvider);
        }

        public ICollection<UnturnedUser> GetOnlineUsers()
        {
            return GetUnturnedUserProvider().GetOnlineUsers();
        }

        public ICollection<UnturnedPendingUser> GetPendingUsers()
        {
            return GetUnturnedUserProvider().GetPendingUsers();
        }
    }
}