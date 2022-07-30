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
                VaultKeep newVaultKeep = _vks.Create(vaultKeepData);
                return Ok(newVaultKeep);

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}