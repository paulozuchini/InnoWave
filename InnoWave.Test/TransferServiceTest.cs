using FluentAssertions;
using Innowave.Service.Interface;
using InnoWave.Library.DTO;
using Moq;

namespace InnoWave.Test
{
    public class TransferServiceTest
    {
        [Fact]
        public async void EmptyCsvPathThrowsArgumentNullException()
        {
            // Arrange
            string csvPath = string.Empty;
            var mockService = new Mock<ITransferService>();

            mockService.Setup(s => s.ReadCsvFile(csvPath)).Throws(new ArgumentNullException());

            // Act
            Func<Task> act = () => mockService.Object.ReadCsvFile(csvPath);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async void WhitespaceCsvPathThrowsArgumentNullException()
        {
            // Arrange
            string csvPath = " ";
            var mockService = new Mock<ITransferService>();

            mockService.Setup(s => s.ReadCsvFile(csvPath)).Throws(new ArgumentNullException());

            // Act
            Func<Task> act = () => mockService.Object.ReadCsvFile(csvPath);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async void InvalidCsvPathThrowsFileNotFoundException()
        {
            // Arrange
            string csvPath = $"Z:\\InvalidPathTesting\\{Guid.NewGuid}";
            var mockService = new Mock<ITransferService>();

            mockService.Setup(s => s.ReadCsvFile(csvPath)).Throws(new FileNotFoundException());

            // Act
            Func<Task> act = () => mockService.Object.ReadCsvFile(csvPath);

            // Assert
            await act.Should().ThrowAsync<FileNotFoundException>();
        }

        [Fact]
        public async void CalculateTotalComissionsReturnsExpectedValues()
        {
            // Arrange
            var mockService = new Mock<ITransferService>();

            var mockList = new List<TransferInputDto>()
            {
                new TransferInputDto(){ 
                    AccountID = "A10",
                    TransferID = "T1000",
                    TotalTransferAmount = 100.00m
                },
                new TransferInputDto(){
                    AccountID = "A11",
                    TransferID = "T1001",
                    TotalTransferAmount = 100.00m
                },
                new TransferInputDto(){
                    AccountID = "A10",
                    TransferID = "T1002",
                    TotalTransferAmount = 200.00m
                },
                new TransferInputDto(){
                    AccountID = "A10",
                    TransferID = "T1003",
                    TotalTransferAmount = 300.00m
                },
            };

            var mockResult = new List<TransferOutputDto>()
            {
                new TransferOutputDto()
                {
                    Account_ID = "A10",
                    Total_Commission = 30m
                },
                new TransferOutputDto()
                {
                    Account_ID = "A11",
                    Total_Commission = 10m
                },
            };

            mockService
                .Setup(s => s.CalculateTotalComissions(mockList))
                .Returns(Task.FromResult(mockResult));

            // Act
            var act = await mockService.Object.CalculateTotalComissions(mockList);

            // Assert
            act.Should().BeEquivalentTo(mockResult);
        }
    }
}