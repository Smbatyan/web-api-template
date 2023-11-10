using Application.DTO.Response.Test;
using MediatR;

namespace Application.Features.User.Commands.V1;

public class CreateUserV1Command : IRequest<TestResponse>
{
    public string Name { get; set; }
}