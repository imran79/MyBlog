using System;
using MediatR;
using User.Domain;
using User.Domain.Entities;

namespace User.Service.v1.Query
{
   public class GetUserByIdQuery : IRequest<UserEntity>
    {
        public Guid Id { get; set; }
    }
}
