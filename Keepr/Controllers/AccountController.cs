using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        private readonly VaultsService _vs;

        private readonly KeepsService _ks;

        public AccountController(AccountService accountService, VaultsService vs, KeepsService ks)
        {
            _accountService = accountService;
            _vs = vs;
            _ks = ks;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Account>> Edit([FromBody] Account accountData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Account account = _accountService.Edit(accountData, userInfo.Email);
                return Ok(account);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("vaults")]
        public async Task<ActionResult<List<Vault>>> GetMyVaults()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Vault> vaults = _vs.GetMyVaults(userInfo.Id);
                return Ok(vaults);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("keeps")]
        public async Task<ActionResult<List<Vault>>> GetMyKeeps()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Keep> keeps = _ks.GetMyKeeps(userInfo.Id);
                return Ok(keeps);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }


}