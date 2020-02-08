using Moq;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Shared.DTOs;
using Xunit;

namespace WallIT.Logic.Test.Repositories
{
    public class UserRepositoryTest
    {
        [Fact]
        public void UserRepository_GetsUserWithExistingId_GetsAnExistingUser()
        {
            var userRepository = new Mock<IUserRepository>();

            // Arrange
            var userDto = new UserDTO
            {
                Email = "test@email.com",
                PasswordHash = "",
                Name = "Test User",
                UserName = "Test User",
                NormalizedUserName = "TEST USER",
                Id = 1
            };

            userRepository
                .Setup(x => x.Get(It.Is<int>(i => i == 1)))
                .Returns(userDto);

            userRepository
                .Setup(x => x.Get(It.Is<int>(i => i != 1)))
                .Returns((UserDTO)null);

            // Act
            var actualUser = userRepository.Object.Get(1);
            var nullUser = userRepository.Object.Get(2);

            // Assert
            Assert.NotNull(actualUser);
            Assert.Equal(actualUser.Id, userDto.Id);
            Assert.Null(nullUser);
        }
    }
}
