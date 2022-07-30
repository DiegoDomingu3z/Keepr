using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Profile GetProfile(string id)
        {
            string sql = @"
            SELECT 
            a.* 
            FROM accounts a
            WHERE id = @id";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id });
        }

        internal List<Keep> GetProfileKeeps(string creatorId)
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE creatorId = @creatorId";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { creatorId }).ToList();
        }

        internal List<Vault> GetProfileVaults(string creatorId)
        {
            string sql = @"
            SELECT
            v.*,
            a.* 
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE creatorId = @creatorId
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { creatorId }).ToList();
        }
    }
}