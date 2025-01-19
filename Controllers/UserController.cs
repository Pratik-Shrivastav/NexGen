using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexGen.Model;
using NextGen.Request;
using NextGen.Service;

namespace NextGen.Controllers
{
    [ApiController]
    [Route("")]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService){
            _userService = userService;
        }

        [HttpPost]
        [Route("createUser")]
        public IActionResult CreateUser([FromBody] UserRequest userRequest){
            string subject = User.Claims.FirstOrDefault(c => c.Type == "User_Name")?.Value;
            _userService.AddUser(userRequest,subject);
            return Ok();

        }

        [HttpGet]
        [Route("resendOtp/{phoneNumber}")]
        public IActionResult ResendOtp([FromRoute] string phoneNumber){
            _userService.ResendOTP(phoneNumber);
            return Ok();
        }

        [HttpGet]
        [Route("validate/{phoneNumber}/validate/{otp}")]
        public IActionResult ValidateOtp([FromRoute] string phoneNumber, [FromRoute] double otp){
            string subject = User.Claims.FirstOrDefault(c => c.Type == "User_Name")?.Value;
            _userService.ValidateOtp(phoneNumber,otp,subject);
            return Ok();
        }

        [HttpGet]
        [Route("user/{phoneNumber}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public IActionResult GetByPhoneNumber([FromRoute] string phoneNumber){
            return Ok(_userService.GetUserByPhoneNumber(phoneNumber));
        }

        [HttpPost]
        [Route("user/{phoneNumber}/membership")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public IActionResult AddMembership([FromRoute] string phoneNumber,[FromBody] MembershipRequest membershipRequest){
            string subject = User.Claims.FirstOrDefault(c => c.Type == "User_Name")?.Value;
            _userService.AddMembership(phoneNumber, membershipRequest,subject);
            return Ok();
        }

        [HttpPost]
        [Route("user/{phoneNumber}/startGame")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public IActionResult StartGame([FromRoute] string phoneNumber,[FromBody] PlayTransactionRequest playTransactionRequest){
            string subject = User.Claims.FirstOrDefault(c => c.Type == "User_Name")?.Value;
            _userService.StartPlayTime(phoneNumber, playTransactionRequest,subject);
            return Ok();
        }

        [HttpGet]
        [Route("user/{phoneNumber}/getAllTransaction")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public IActionResult GetAllTransactions([FromRoute] string phoneNumber){
            return Ok(_userService.GetAllTransactionsOfUser(phoneNumber));
        }

        [HttpGet]
        [Route("user/{phoneNumber}/getAllMembershipTransaction")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public IActionResult GetAllMembershipTransactions([FromRoute] string phoneNumber){
            return Ok(_userService.GetAllMembershipTransactionsOfUser(phoneNumber));
        }
        
    }
}