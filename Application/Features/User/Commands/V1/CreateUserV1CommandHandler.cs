using Application.DTO.Response.Test;
using Core.Entities;
using Core.Redis;
using MediatR;

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
            Id = 1, Text = request.Name
        });

        var test = await _testCacheService.GetRecordAsync("test");

        return new TestResponse()
        {
            Id = test.Id,
            Text = test.Text
        };
    }
}