using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        private readonly VaultsRepository _vr;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vr)
        {
            _repo = repo;
            _vr = vr;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
        {

            Vault found = _vr.GetById(vaultKeepData.VaultId);
            if (found.CreatorId != userId)
            {
                throw new System.Exception("Forbidden");
            }
            return _repo.Create(vaultKeepData);
        }


        private VaultKeep GetById(int id)
        {
            VaultKeep found = _repo.GetById(id);
            if (found == null)
            {
                throw new System.Exception("invalid Id");
            }

            return found;
        }



        internal string Delete(int id, string userId)
        {
            VaultKeep found = GetById(id);
            if (found.CreatorId != userId)
            {
                throw new System.Exception("Forbidden");
            }
            return _repo.Delete(id);

        }


    }
}