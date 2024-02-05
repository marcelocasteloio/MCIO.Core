using FluentAssertions;
using MCIO.OutputEnvelop.Enums;

namespace MCIO.Core.UnitTests.ExecutionInfoTests;

public class ExecutionInfoTest
{
    [Fact]
    public void ExecutionInfo_Should_Created()
    {
        // Arrange
        var correlationId = Guid.NewGuid();
        var tenantInfo = TenantInfo.TenantInfo.FromExistingCode(Guid.NewGuid()).Output!.Value;
        var executionUser = Guid.NewGuid().ToString();
        var origin = Guid.NewGuid().ToString();

        // Act
        var executionInfo = ExecutionInfo.ExecutionInfo.Create(
            correlationId,
            tenantInfo,
            executionUser,
            origin
        ).Output!.Value;

        // Assert
        executionInfo.IsValid.Should().BeTrue();
        executionInfo.CorrelationId.Should().Be(correlationId);
        executionInfo.TenantInfo.Should().Be(tenantInfo);
        executionInfo.ExecutionUser.Should().Be(executionUser);
        executionInfo.Origin.Should().Be(origin);
    }

    [Fact]
    public void ExecutionInfo_Should_Invalid_If_Created_From_Default()
    {
        // Arrange
        var expectedIsValid = false;
        var correlationId = Guid.Empty;
        var tenantInfo = default(TenantInfo.TenantInfo);
        var executionUser = default(string);
        var origin = default(string);

        // Arrange and Act
        var executionInfo = default(ExecutionInfo.ExecutionInfo);

        // Assert
        executionInfo.IsValid.Should().Be(expectedIsValid);
        executionInfo.CorrelationId.Should().Be(correlationId);
        executionInfo.TenantInfo.Should().Be(tenantInfo);
        executionInfo.ExecutionUser.Should().Be(executionUser);
        executionInfo.Origin.Should().Be(origin);
    }

    [Fact]
    public void ExecutionInfo_Should_Not_Created()
    {
        // Arrange
        var correlationId = Guid.Empty;
        var tenantInfo = default(TenantInfo.TenantInfo);
        var executionUser = default(string);
        var origin = default(string);

        // Act
        var executionInfoOutput = ExecutionInfo.ExecutionInfo.Create(
            correlationId,
            tenantInfo,
            executionUser,
            origin
        );

        // Assert
        executionInfoOutput.IsSuccess.Should().BeFalse();
        executionInfoOutput.HasOutput.Should().BeFalse();
        executionInfoOutput.Output.Should().BeNull();

        executionInfoOutput.OutputMessageCollectionCount
            .Should()
            .Be(
                ExecutionInfo.ExecutionInfo.MAX_MESSAGE_COUNT
            );

        executionInfoOutput.OutputMessageCollection
            .Any(q => 
                q.Type == OutputMessageType.Error
                && q.Code == ExecutionInfo.ExecutionInfo.CORRELATION_ID_SHOULD_REQUIRED_MESSAGE_CODE
                && q.Description == ExecutionInfo.ExecutionInfo.CORRELATION_ID_SHOULD_REQUIRED_MESSAGE_DESCRIPTION
            )
            .Should()
            .BeTrue();

        executionInfoOutput.OutputMessageCollection
            .Any(q =>
                q.Type == OutputMessageType.Error
                && q.Code == ExecutionInfo.ExecutionInfo.TENANT_INFO_SHOULD_VALID_MESSAGE_CODE
                && q.Description == ExecutionInfo.ExecutionInfo.TENANT_INFO_SHOULD_VALID_MESSAGE_DESCRIPTION
            )
            .Should()
            .BeTrue();

        executionInfoOutput.OutputMessageCollection
            .Any(q =>
                q.Type == OutputMessageType.Error
                && q.Code == ExecutionInfo.ExecutionInfo.EXECUTION_USER_SHOULD_REQUIRED_MESSAGE_CODE
                && q.Description == ExecutionInfo.ExecutionInfo.EXECUTION_USER_SHOULD_REQUIRED_MESSAGE_DESCRIPTION
            )
            .Should()
            .BeTrue();

        executionInfoOutput.OutputMessageCollection
            .Any(q =>
                q.Type == OutputMessageType.Error
                && q.Code == ExecutionInfo.ExecutionInfo.ORIGIN_SHOULD_REQUIRED_MESSAGE_CODE
                && q.Description == ExecutionInfo.ExecutionInfo.ORIGIN_SHOULD_REQUIRED_MESSAGE_DESCRIPTION
            )
            .Should()
            .BeTrue();
    }
}
