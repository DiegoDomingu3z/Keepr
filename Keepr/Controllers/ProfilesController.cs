using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : Controller
    {
        private readonly ProfilesService _ps;

        public ProfilesController(ProfilesService ps)
        {
            _ps = ps;
        }


        [HttpGet("{id}")]
        public ActionResult<Profile> GetProfile(string id)
        {
            try
            {
                Profile profile = _ps.GetProfile(id);
                return Ok(profile);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetProfileKeeps(string id)
        {
            try
            {
                List<Keep> keep = _ps.GetProfileKeeps(id);
                return Ok(keep);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> GetProfileVaults(string id)
        {
            try
            {
                List<Vault> vault = _ps.GetProfileVaults(id);
                return Ok(vault);

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}