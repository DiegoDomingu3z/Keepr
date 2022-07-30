using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault Create(Vault vaultData, string id)
        {
            return _repo.Create(vaultData);
        }

        internal Vault GetById(int id)
        {
            Vault found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;

        }

        internal Vault Edit(int id, string userId, Vault vaultData)
        {
            Vault original = GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;

            _repo.Edit(original);
            return original;
        }

        internal Vault Delete(int id, string userId)
        {
            Vault vault = GetById(id);
            if (vault.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            _repo.Delete(id);
            return vault;


        }
    }
}