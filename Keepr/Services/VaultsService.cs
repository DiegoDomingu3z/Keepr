using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        private readonly VaultKeepsRepository _kRep;

        public VaultsService(VaultsRepository repo, VaultKeepsRepository kRep)
        {
            _repo = repo;
            _kRep = kRep;
        }

        internal Vault Create(Vault vaultData, string id)
        {

            return _repo.Create(vaultData);
        }

        internal Vault GetById(int id, string userId)
        {
            Vault found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }

            if (found.IsPrivate == true && found.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }

            return found;

        }

        internal List<VaultKeepViewModal> GetVaultKeeps(int id, string userId)
        {
            Vault vault = _repo.GetById(id);
            if (vault.IsPrivate == true && vault.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }

            return _kRep.GetVaultsKeeps(id);
        }



        internal List<Vault> GetMyVaults(string id)
        {

            return _repo.GetMyVaults(id);
        }



        internal Vault Edit(int id, string userId, Vault vaultData)
        {
            Vault original = GetById(id, userId);
            if (original.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            original.Img = vaultData.Img ?? original.Img;
            original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;

            _repo.Edit(original);
            return original;
        }

        internal Vault Delete(int id, string userId)
        {
            Vault vault = GetById(id, userId);
            if (vault.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            _repo.Delete(id);
            return vault;


        }


    }
}