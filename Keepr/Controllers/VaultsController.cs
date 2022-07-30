using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VaultsController : Controller
    {

        private readonly VaultsService _vs;

        private readonly VaultKeepsService _vks;

        public VaultsController(VaultsService vs, VaultKeepsService vks)
        {
            _vs = vs;
            _vks = vks;
        }

        [HttpPost]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                Vault newVault = _vs.Create(vaultData, userInfo.Id);
                newVault.Creator = userInfo;
                return Ok(newVault);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}")]

        public ActionResult<Vault> GetById(int id)
        {
            try
            {
                Vault vault = _vs.GetById(id);
                return vault;
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<VaultKeep>> GetVaultKeeps(int id)
        {
            try
            {
                List<VaultKeep> vaultKeep = _vks.GetVaultKeeps(id);
                return vaultKeep;
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        [HttpPut("{id}")]

        public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault updateVault = _vs.Edit(id, userInfo.Id, vaultData);
                return Ok(updateVault);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Vault>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault deletedVault = _vs.Delete(id, userInfo.Id);
                return Ok("deleted");

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}