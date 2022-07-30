using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }
        internal List<Keep> GetAll()
        {
            return _repo.GetAll();
        }

        internal Keep GetById(int id)
        {
            Keep found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;

        }

        internal Keep Create(Keep keepData, string id)
        {
            return _repo.Create(keepData);
        }

        internal Keep Edit(int id, Keep keepData, string userId)
        {
            Keep original = GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            original.Name = keepData.Name ?? original.Name;
            original.Description = keepData.Description ?? original.Description;
            original.Img = keepData.Img ?? original.Img;

            _repo.Edit(original);
            return original;
        }

        internal Keep Delete(int id, string userId)
        {
            Keep keep = GetById(id);
            if (keep.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            _repo.Delete(id);
            return keep;
        }
    }
}