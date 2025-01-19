using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextGen.Enum;
using NextGen.Service;

namespace NextGen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService){
            _transactionService = transactionService;
        }

        [HttpPut]
        [Route("endTransaction/{transactionId}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public IActionResult EndTransaction([FromRoute] int transactionId){
            string subject = User.Claims.FirstOrDefault(c => c.Type == "User_Name")?.Value;

            return Ok(_transactionService.EndPlayTransaction(transactionId,subject));
        }

        [HttpPut]
        [Route("endTransaction/{transactionId}/payment/{paymentMethod}")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public IActionResult Payment([FromRoute] int transactionId,[FromRoute] PaymentMethod paymentMethod){
            string subject = User.Claims.FirstOrDefault(c => c.Type == "User_Name")?.Value;
            _transactionService.Payment(transactionId,paymentMethod,subject);
            return Ok();
        }

        [HttpGet]
        [Route("transactions")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        public IActionResult Get(){
            
            return Ok(_transactionService.GetAllTransaction());
        }
    }
}