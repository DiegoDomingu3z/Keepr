using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {

        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetAll(string query = "")
        {
            string stringQuery = "%" + query + "%";
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.name LIKE @stringQuery";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { stringQuery }).ToList();
        }

        internal void Views(Keep found, int id)
        {
            string sql = @"
            UPDATE keeps
            SET
            views = @Views + 1
            WHERE 
            keeps.id = @id";
            _db.Execute(sql, found);
        }

        internal void Kept(Keep keep, int id)
        {
            string sql = @"
            UPDATE keeps 
            SET 
            kept = @kept
            WHERE
            keeps.id = @id";
            _db.Execute(sql, keep);
        }

        internal Keep GetById(int id)
        {
            string sql = @"
           SELECT 
           k.*,
           a.*
           FROM keeps k
           JOIN accounts a ON k.creatorId = a.id
           WHERE k.id = @id
           ";

            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }).FirstOrDefault();
        }



        internal List<Keep> GetMyKeeps(string id)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.creatorId = @id";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
            {
                keep.Creator = prof;
                return keep;
            }, new { id }).ToList();
        }


        internal Keep Create(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, views, kept, creatorId)
            VALUES
            (@name, @description, @img, @views, @kept, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            return keepData;
        }



        internal void Edit(Keep original)
        {
            string sql = @"
            UPDATE keeps
            Set
            name = @name,
            description = @description,
            img = @img";

            _db.Execute(sql, original);
        }



        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM keeps
            WHERE id = @id LIMIT 1";
            _db.ExecuteScalar(sql, new { id });
        }
    }
}