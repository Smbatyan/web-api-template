using Core.Entities;
using Core.Redis;
using MediatR;
using WebApi.DTO.Response.Test;

namespace Application.Features.User.Commands.V1;

public class CreateUserV1CommandHandler : IRequestHandler<CreateUserV1Command, TestResponse>
{
    private readonly ITestCacheService _testCacheService;

    public CreateUserV1CommandHandler(ITestCacheService testCacheService)
    {
        _testCacheService = testCacheService;
    }

    public async Task<TestResponse> Handle(CreateUserV1Command request, CancellationToken cancellationToken)
    {
        await _testCacheService.SetRecordAsync("test", new TestEntity
        {
            Id = 1, Text = "test"
        });

        return new TestResponse();
    }
}