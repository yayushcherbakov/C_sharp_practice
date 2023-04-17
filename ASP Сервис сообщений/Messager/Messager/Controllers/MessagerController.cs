using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messager.Entities;
using Messager.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Messager.Controllers
{
    /// <summary>
    /// API methods to operate with favorite apartments list.
    /// </summary>
    /// <response code="200">OK.</response>
    /// <response code="400">Bad request.</response>
    [ApiController]
    [Route("Messager")]
    public class MessagerController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessagerController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Random generation. Only for initialization. Throw error if users or messages already exist.
        /// </summary>
        /// <param name="userCount">Count of users(from 1 to 20).</param>
        [HttpPost]
        [Route("RandomGeneration")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult RandomGeneration(int userCount)
        {
            if (userCount < 1 || userCount > 20)
            {
                return BadRequest("Invalid count. User count from 1 to 20");
            }
            this._messageService.CreateRandomUsers(userCount);
            return Ok();
        }

        /// <summary>
        /// Delete all users and messages.
        /// </summary>
        [HttpPost]
        [Route("DeleteUsersAndMessages")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult DeleteUsersAndMessages()
        {
            this._messageService.DeleteUsersAndMessages();
            return Ok();
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="user">User data.</param>
        [HttpPost]
        [Route("RegisterUser")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult RegisterUser([FromBody] User user)
        {
            this._messageService.RegisterUser(user);
            return Ok();
        }

        /// <summary>
        /// Get user information.
        /// </summary>
        /// <param name="userId"> User id(email). </param>
        /// <returns>User information.</returns>
        [HttpGet]
        [Route("GetUserInfo")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult GetUserInfo(string userId)
        {
            var user = _messageService.GetUserInfo(userId);
            return Ok(user);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of all users by params.</returns>
        [HttpGet]
        [Route("GetAllUsers")]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GetAllUsers(int limit, int offset)
        {
            if (offset < 0)
            {
                return BadRequest("Offset must be positive or zero");
            }

            if (limit < 1)
            {
                return BadRequest("Limit must be positive");
            }
            var user = _messageService.GetAllUsers(limit, offset);
            return Ok(user);
        }

        /// <summary>
        /// Get messages by sendler and receiver ids.
        /// </summary>
        /// <param name="sendlerId"> Sendler Id (email). </param>
        /// <param name="receiverId"> Receiver Id (email). </param>
        /// <returns>List messages.</returns>
        [HttpGet]
        [Route("GetMessages")]
        [ProducesResponseType(typeof(List<Letter>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GetMessages(string sendlerId, string receiverId)
        {
            var user = _messageService.GetMessages(sendlerId, receiverId);
            return Ok(user);
        }

        /// <summary>
        /// Get messages by sendler id.
        /// </summary>
        /// <param name="sendlerId"> Sendler Id (email). </param>
        /// <returns>List messages.</returns>
        [HttpGet]
        [Route("GetMessagesBySendler")]
        [ProducesResponseType(typeof(List<Letter>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GetMessagesBySendler(string sendlerId)
        {
            var user = _messageService.GetMessagesBySender(sendlerId);
            return Ok(user);
        }

        /// <summary>
        /// Get messages by receiver id.
        /// </summary>
        /// <param name="receiverId"> Receiver Id (email). </param>
        /// <returns>List messages.</returns>
        [HttpGet]
        [Route("GetMessagesByReceiver")]
        [ProducesResponseType(typeof(List<Letter>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GetMessagesByReceiver(string receiverId)
        {
            var user = _messageService.GetMessagesByReceiver(receiverId);
            return Ok(user);
        }

        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="message">Message to send.</param>
        [HttpPost]
        [Route("SendMessage")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult SendMessage([FromBody] Letter message)
        {
            _messageService.SendMessage(message);
            return Ok();
        }
    }
}
