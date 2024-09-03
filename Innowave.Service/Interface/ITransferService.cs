using InnoWave.Library.DTO;

namespace Innowave.Service.Interface
{
    public interface ITransferService
    {
        /// <summary>
        /// This method is used to read the csv file and get the list of input transfers from the file;
        /// Where csvPath is the path to where the file "transfers.csv" is located
        /// </summary>
        /// <param name="csvPath">
        /// The path where the file "transfers.csv" is located
        /// </param>
        Task<List<TransferInputDto>> ReadCsvFile(string csvPath);

        /// <summary>
        /// This method is used to calculate the total commissions that should be charged for each account on a given day,
        /// , with the following rules:
        /// * Accounts should be charged by 10% of the total value on every transfer
        /// * The transfer with the highest value of the day will not be subject to commission
        /// </summary>
        /// <param name="transferInputs"></param>
        /// <returns></returns>
        Task<List<TransferOutputDto>> CalculateTotalComissions(List<TransferInputDto> transferInputs);
    }
}
