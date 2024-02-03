using FluentAssertions;

namespace MCIO.Core.UnitTests.TenantInfoTests;

public class TenantInfoTest
{
    [Fact]
    public void TenantInfo_Should_Created_From_Existing_Code()
    {
        // Arrange
        var existingCode = Guid.NewGuid();

        // Act
        var tenantInfoA = TenantInfo.TenantInfo.FromExistingCode(existingCode).Output!.Value;
        var tenantInfoB = (TenantInfo.TenantInfo) existingCode;
        var tenantCodeA = tenantInfoA.Code;
        var tenantCodeB = (Guid) tenantInfoB;

        // Assert
        tenantInfoA.IsValid.Should().BeTrue();
        tenantInfoA.Code.Should().Be(existingCode);
        tenantCodeA.Should().Be(existingCode);

        tenantInfoB.IsValid.Should().BeTrue();
        tenantInfoB.Code.Should().Be(existingCode);
        tenantCodeB.Should().Be(existingCode);
    }

    [Fact]
    public void TenantInfo_Should_Invalid_If_Created_From_Default()
    {
        // Arrange
        var expectedIsValid = false;
        var expectedCode = Guid.Empty;

        // Arrange and Act
        var tenantInfo = default(TenantInfo.TenantInfo);

        // Assert
        tenantInfo.IsValid.Should().Be(expectedIsValid);
        tenantInfo.Code.Should().Be(expectedCode);
    }

    [Fact]
    public void TenantInfo_Shoul_Not_Created_From_Invalid_Code()
    {
        // Arrange
        var existingCode = Guid.Empty;

        // Act
        var createTenantInfoOutput = TenantInfo.TenantInfo.FromExistingCode(existingCode);

        // Assert
        createTenantInfoOutput.IsSuccess.Should().BeFalse();
        createTenantInfoOutput.HasOutput.Should().BeFalse();

        createTenantInfoOutput.HasOutputMessage.Should().BeTrue();
        createTenantInfoOutput.OutputMessageCollectionCount.Should().Be(1);
        createTenantInfoOutput.OutputMessageCollection.First().Code.Should().Be(TenantInfo.TenantInfo.TENANT_INFO_CODE_SHOULD_REQUIRED_MESSAGE_CODE);
        createTenantInfoOutput.OutputMessageCollection.First().Description.Should().Be(TenantInfo.TenantInfo.TENANT_INFO_CODE_SHOULD_REQUIRED_MESSAGE_DESCRIPTION);
    }
}
