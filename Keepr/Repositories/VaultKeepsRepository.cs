using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {


        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            string sql = @"
           INSERT INTO vaultKeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@creatorId, @vaultId, @keepId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
            vaultKeepData.Id = id;
            return vaultKeepData;
        }

        internal VaultKeep GetById(int id)
        {
            string sql = @"
           SELECT
           a.*,
           vk.*
           FROM vaultKeeps vk
           JOIN accounts a ON vk.creatorId = a.id
           WHERE vk.id = @id";
            return _db.Query<Profile, VaultKeep, VaultKeep>(sql, (profile, vaultK) =>
            {
                vaultK.CreatorId = profile.Id;
                return vaultK;
            }, new { id }).FirstOrDefault();
        }

        internal List<VaultKeepViewModal> GetVaultsKeeps(int id)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id AS VaultId,
            a.*
            FROM vaultKeeps vk
            JOIN keeps k ON vk.keepId = k.id
            JOIN accounts a ON k.creatorId = a.id
            WHERE vk.vaultId = @id;";
            return _db.Query<VaultKeepViewModal, Profile, VaultKeepViewModal>(sql, (VaultKeep, profile) =>
            {
                VaultKeep.Creator = profile;
                return VaultKeep;
            }, new { id }).ToList();
        }

        internal string Delete(int id)
        {
            string sql = @"
            DELETE FROM vaultKeeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });

            return ("deleted");
        }

    }
}