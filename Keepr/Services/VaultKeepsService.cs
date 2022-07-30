using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData, string id)
        {

            return _repo.Create(vaultKeepData);
        }

        internal List<VaultKeepViewModal> GetVaultKeeps(int id)
        {
            return _repo.GetVaultsKeeps(id);
        }

        internal string Delete(int id, string userId)
        {
            return _repo.Delete(id);

        }
    }
}