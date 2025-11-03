using GPScoreTracker.Domain.Entities;
using Xunit;

namespace GPScoreTracker.Domain.Tests.Entities;

/// <summary>
/// UserProfile エンティティのテスト
/// </summary>
public class UserProfileTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidValues_CreatesInstance()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();

        // Act
        var userProfile = new UserProfile(userProfileId, "Test Profile", "hashed-api-key-123");

        // Assert
        Assert.Equal(userProfileId, userProfile.UserProfileId);
        Assert.Equal("Test Profile", userProfile.ProfileName);
        Assert.Equal("hashed-api-key-123", userProfile.ApiKeyHash);
    }

    [Fact]
    public void Constructor_NullProfileName_ThrowsArgumentNullException()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new UserProfile(userProfileId, null!, "hashed-api-key"));
        Assert.Equal("profileName", exception.ParamName);
    }

    [Fact]
    public void Constructor_NullApiKeyHash_ThrowsArgumentNullException()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new UserProfile(userProfileId, "Test Profile", null!));
        Assert.Equal("apiKeyHash", exception.ParamName);
    }

    [Fact]
    public void Constructor_EmptyProfileName_ThrowsArgumentException()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(
            () => new UserProfile(userProfileId, "", "hashed-api-key"));
        Assert.Contains("ProfileName cannot be empty", exception.Message);
    }

    [Fact]
    public void Constructor_EmptyApiKeyHash_ThrowsArgumentException()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(
            () => new UserProfile(userProfileId, "Test Profile", ""));
        Assert.Contains("ApiKeyHash cannot be empty", exception.Message);
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameUserProfileId_ReturnsTrue()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();
        var userProfile1 = new UserProfile(userProfileId, "Profile A", "hash1");
        var userProfile2 = new UserProfile(userProfileId, "Profile B", "hash2"); // 異なるプロパティ

        // Act & Assert
        Assert.Equal(userProfile1, userProfile2);
        Assert.True(userProfile1 == userProfile2);
        Assert.False(userProfile1 != userProfile2);
    }

    [Fact]
    public void Equals_DifferentUserProfileId_ReturnsFalse()
    {
        // Arrange
        var userProfileId1 = Guid.NewGuid();
        var userProfileId2 = Guid.NewGuid();
        var userProfile1 = new UserProfile(userProfileId1, "Profile A", "hash1");
        var userProfile2 = new UserProfile(userProfileId2, "Profile A", "hash1");

        // Act & Assert
        Assert.NotEqual(userProfile1, userProfile2);
        Assert.False(userProfile1 == userProfile2);
        Assert.True(userProfile1 != userProfile2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();
        var userProfile = new UserProfile(userProfileId, "Test Profile", "hashed-api-key");

        // Act & Assert
        Assert.False(userProfile.Equals(null));
        Assert.False(userProfile == null);
        Assert.True(userProfile != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();
        var userProfile = new UserProfile(userProfileId, "Test Profile", "hashed-api-key");

        // Act & Assert
#pragma warning disable CS1718 // 意図的に同じ変数を比較
        Assert.True(userProfile.Equals(userProfile));
        Assert.True(userProfile == userProfile);
#pragma warning restore CS1718
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameUserProfileId_ReturnsSameHashCode()
    {
        // Arrange
        var userProfileId = Guid.NewGuid();
        var userProfile1 = new UserProfile(userProfileId, "Profile A", "hash1");
        var userProfile2 = new UserProfile(userProfileId, "Profile B", "hash2");

        // Act & Assert
        Assert.Equal(userProfile1.GetHashCode(), userProfile2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentUserProfileId_ReturnsDifferentHashCode()
    {
        // Arrange
        var userProfileId1 = Guid.NewGuid();
        var userProfileId2 = Guid.NewGuid();
        var userProfile1 = new UserProfile(userProfileId1, "Profile A", "hash1");
        var userProfile2 = new UserProfile(userProfileId2, "Profile A", "hash1");

        // Act & Assert
        Assert.NotEqual(userProfile1.GetHashCode(), userProfile2.GetHashCode());
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        var userProfileId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var userProfile = new UserProfile(userProfileId, "Test Profile", "hashed-api-key-123");

        // Act
        var result = userProfile.ToString();

        // Assert
        Assert.Contains("UserProfileId:12345678-1234-1234-1234-123456789abc", result);
        Assert.Contains("ProfileName:Test Profile", result);
        Assert.Contains("ApiKeyHash:hashed-api-key-123", result);
    }

    #endregion
}