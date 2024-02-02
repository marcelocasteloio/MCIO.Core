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
        var tenantInfoA = TenantInfo.TenantInfo.FromExistingCode(existingCode);
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
    public void TenantInfo_Should_Implicited_Converted_From()
    {
        // Arrange
        var existingCode = Guid.NewGuid();

        // Act
        var tenantInfo = TenantInfo.TenantInfo.FromExistingCode(existingCode);

        // Assert
        tenantInfo.IsValid.Should().BeTrue();
        tenantInfo.Code.Should().Be(existingCode);
    }
}
