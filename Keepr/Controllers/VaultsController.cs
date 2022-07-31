using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        public async Task<ActionResult<Vault>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.GetById(id, userInfo?.Id);
                return vault;
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]

        public async Task<ActionResult<List<VaultKeepViewModal>>> GetVaultKeeps(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<VaultKeepViewModal> vaultKeep = _vs.GetVaultKeeps(id, userInfo?.Id);
                return Ok(vaultKeep);
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