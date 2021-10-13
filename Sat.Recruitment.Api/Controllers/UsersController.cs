using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models.Common;
using Sat.Recruitment.Core.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager _usersManager;

        public UsersController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserModel user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ResultModel.CreateFailedResultModel(ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage)));

            try
            {
                var resultUser = await _usersManager.CreateUser(user);
                return Created("api/users", resultUser);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResultModel.CreateFailedResultModel(new List<string>() { ex.Message }));
            }
        }
    }
}
