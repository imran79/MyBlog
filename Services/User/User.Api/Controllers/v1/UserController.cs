using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Domain.Entities;
using User.Api.Models.v1;
using User.Service.v1.Command;
using User.Service.v1.Query;
using System.Threading;

namespace User.Api.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a new user in the database.
        /// </summary>
        /// <param name="userModel">Model to create a new user</param>
        /// <returns>Returns the created user</returns>
        /// <response code="200">Returned if the user was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<UserModel>> Users(CreateUserModel userModel)
        {
            try
            {
				var user=  await _mediator.Send(new CreateUserCommand
				{
                    User = _mapper.Map<UserEntity>(userModel)
                });

                return _mapper.Map<UserModel>(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        ///     Action to update a new user in the database.
        /// </summary>
        /// <param name="userModel">Model to create a new user</param>
        /// <returns>Returns the created user</returns>
        /// <response code="200">Returned if the user was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<UserModel>> UpdateUser(UpdateUserModel userModel)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery
                {
                    Id = userModel.Id
                });

                if (user == null)
                {
                    return BadRequest($"No customer found with the id {userModel.Id}");
                }
                var uUser = _mapper.Map(userModel, user);
                var updatedUser = await _mediator.Send(new UpdateUserCommand
                {
                    User = _mapper.Map(userModel, user)
                });
                var updatedUserModel = new UserModel();
                return _mapper.Map(updatedUser, updatedUserModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        ///     Action to retrieve all users.
        /// </summary>
        /// <returns>Returns a list of all userModel or an empty list, if no user is created yet</returns>
        /// <response code="200">Returned if the list of users was retrieved</response>
        /// <response code="400">Returned if the users could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> Users()
        {
            try
            {
                var userEntities = await _mediator.Send(new GetAllUsersQuery());
                var users = new List<UserModel>();
                await foreach (var item in userEntities)
				{
                    users.Add(_mapper.Map<UserModel>(item));
				}

                return users;              
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Get User By Id.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>Returns the user for given guid</returns>
        /// <response code="200">Returned if the user was present with the given guid</response>
        /// <response code="400">Returned if the user could not be found with the provided id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> UserById(Guid id)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery
                {
                    Id = id
                });

                if (user == null)
                {
                    return BadRequest($"No user found with the id {id}");
                }

                return _mapper.Map<UserModel>(user);                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}