using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api.Models.v1;
using Blog.Data.Entities;
using Blog.Service.v1.Command;
using Blog.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers.v1 {
    [Produces ("application/json")]
    [Route ("v1/[controller]")]
    [ApiController]
    public class PostController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostController (IMapper mapper, IMediator mediator) {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Action to create a new Post in the database.
        /// </summary>
        /// <param name="createPostModel">Model to create a new Post</param>
        /// <returns>Returns the created Post</returns>
        /// <response code="200">Returned if the Post was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the Post couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Post>> Post (CreatePostModel createPostModel) {
            try {
                return await _mediator.Send (new CreatePostCommand {
                    Post = _mapper.Map<Post> (createPostModel)
                });
            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        /// <summary>
        /// Action to update an existing Post
        /// </summary>
        /// <param name="updatePostModel">Model to update an existing Post</param>
        /// <returns>Returns the updated Post</returns>
        /// <response code="200">Returned if the Post was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed or the Post couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status422UnprocessableEntity)]
        [HttpPut]
        public async Task<ActionResult<Post>> Post (UpdatePostModel updatePostModel) {
            try {
                var Post = await _mediator.Send (new GetPostByIdQuery {
                    Id = updatePostModel.Id
                });

                if (Post == null) {
                    return BadRequest ($"No Post found with the id {updatePostModel.Id}");
                }

                return await _mediator.Send (new UpdatePostCommand {
                    Post = _mapper.Map (updatePostModel, Post)
                });
            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        /// <summary>
        ///     Action to retrieve all Posts.
        /// </summary>
        /// <returns>Returns a list of all Posts</returns>
        /// <response code="200">Returned if the list of Posts was retrieved</response>
        /// <response code="400">Returned if the Posts could not be retrieved</response>
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [HttpGet ("blog/{id}/posts")]
        public async Task<ActionResult<List<Post>>> PostsByBlogId (Guid id) {
            try {
                return await _mediator.Send (new GetPostsByBlogIdQuery {
                    BlogId = id
                });
            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }
        }

        /// <summary>
        ///     Action to get the Post.
        /// </summary>
        /// <param name="id">The id of the Post</param>
        /// <returns>Returns the Post</returns>
        /// <response code="200">Returned if the Post found</response>
        /// <response code="400">Returned if the Post could not be retrieved</response>
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [HttpGet ("{id}")]
        public async Task<ActionResult<Post>> PostById (Guid id) {
            try {
                var Post = await _mediator.Send (new GetPostByIdQuery {
                    Id = id
                });

                if (Post == null) {
                    return BadRequest ($"No Post found with the id {id}");
                }

                return Post;
            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }
        }
    }
}