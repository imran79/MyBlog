﻿using MediatR;
using User.Domain;
using User.Domain.Entities;

namespace User.Service.v1.Command
{
    public class CreateUserCommand : IRequest<UserEntity>
    {
        public UserEntity User { get; set; }
    }
}
