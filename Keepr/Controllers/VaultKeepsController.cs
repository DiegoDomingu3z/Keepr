using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VaultKeepsController : Controller
    {

        private readonly VaultKeepsService _vks;

        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }




        [HttpPost]

        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();

                vaultKeepData.CreatorId = userInfo.Id;
                vaultKeepData.Creator = userInfo;
                VaultKeep newVaultKeep = _vks.Create(vaultKeepData, userInfo?.Id);
                return Ok(newVaultKeep);

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                string deletedVaultKeep = _vks.Delete(id, userInfo.Id);
                return Ok("deleted");

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}