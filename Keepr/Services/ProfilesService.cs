using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class ProfilesService
    {

        private readonly ProfilesRepository _repo;

        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal Profile GetProfile(string id)
        {
            Profile profile = _repo.GetProfile(id);
            if (profile == null)
            {
                throw new Exception("Invalid id");
            }
            return profile;
        }

        internal List<Keep> GetProfileKeeps(string id)
        {
            return _repo.GetProfileKeeps(id);
        }

        internal List<Vault> GetProfileVaults(string id)
        {
            List<Vault> vault = _repo.GetProfileVaults(id);
            return vault.FindAll(v => v.IsPrivate == false);
        }
    }
}