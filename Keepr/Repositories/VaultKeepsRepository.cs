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

        internal List<VaultKeep> GetVaultsKeeps(int id)
        {
            string sql = @"
            SELECT
            vk.*
            FROM vaultKeeps vk
            WHERE id = @id";
            return _db.Query<VaultKeep>(sql, new { id }).ToList();
        }
    }
}