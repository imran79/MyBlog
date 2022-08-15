using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api.Models.v1;
using Blog.Data.Entities;
using Blog.Service.v1.Blogs.Query;
using Blog.Service.v1.Command;
using Blog.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Blog.Api.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BlogController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
                

        /// <summary>
        /// Action to create a new blog in the database.
        /// </summary>
        /// <param name="createBlogModel">Model to create a new blog</param>
        /// <returns>Returns the created blog</returns>
        /// <response code="200">Returned if the blog was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the blog couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<BlogEntity>> Blog(CreateBlogModel createBlogModel)
        {
            try
            {
                return await _mediator.Send(new CreateBlogCommand
                {
                    Blog = _mapper.Map<BlogEntity>(createBlogModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to update an existing blog
        /// </summary>
        /// <param name="updateBlogModel">Model to update an existing blog</param>
        /// <returns>Returns the updated blog</returns>
        /// <response code="200">Returned if the blog was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed or the blog couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<BlogEntity>> Blog(UpdateBlogModel updateBlogModel)
        {
            try
            {
                var blog = await _mediator.Send(new GetBlogByIdQuery
                {
                    Id = updateBlogModel.Id
                });

                if (blog == null)
                {
                    return BadRequest($"No blog found with the id {updateBlogModel.Id}");
                }

                return await _mediator.Send(new UpdateBlogCommand
                {
                    Blog = _mapper.Map(updateBlogModel, blog)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Action to retrieve all blogs.
        /// </summary>
        /// <returns>Returns a list of all blogs</returns>
        /// <response code="200">Returned if the list of blogs was retrieved</response>
        /// <response code="400">Returned if the blogs could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<BlogEntity>>> GetAllBlogs()
        {
            try
            {
                return await _mediator.Send(new GetBlogsQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Action to get the blog.
        /// </summary>
        /// <param name="id">The id of the blog</param>
        /// <returns>Returns the blog</returns>
        /// <response code="200">Returned if the blog found</response>
        /// <response code="400">Returned if the blog could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogEntity>> BlogById(Guid id)
        {
            try
            {
                var blog = await _mediator.Send(new GetBlogByIdQuery
                {
                    Id = id
                });

                if (blog == null)
                {
                    return BadRequest($"No blog found with the id {id}");
                }

               return blog;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
