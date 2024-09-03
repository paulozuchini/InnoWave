using CsvHelper;
using CsvHelper.Configuration;
using Innowave.Service.Interface;
using InnoWave.Library.DTO;
using System.Globalization;

namespace Innowave.Service
{
    public class TransferService : ITransferService
    {
        const string fileName = "transfers.csv";

        public async Task<List<TransferOutputDto>> CalculateTotalComissions(List<TransferInputDto> transferInputs)
        {
            var result = new List<TransferOutputDto>();

            if(transferInputs.Count() == 0)
            {
                return result;
            }

            var highestTransfer = transferInputs.Max(t => t.TotalTransferAmount);

            result = transferInputs
                .Where(t => t.TotalTransferAmount != highestTransfer)
                .GroupBy(t => t.AccountID)
                .Select(t => new TransferOutputDto()
                {
                    Account_ID = t.Key,
                    Total_Commission = t.Sum(t => t.TotalTransferAmount) * 0.1m
                }).ToList();

            return await Task.FromResult(result);
        }

        public async Task<List<TransferInputDto>> ReadCsvFile(string csvPath)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(csvPath))
                    throw new ArgumentNullException(nameof(csvPath));

                if (!File.Exists(string.Concat(csvPath, fileName)))
                {
                    throw new FileNotFoundException($"Invalid path. Could not find: {string.Concat(csvPath, fileName)}");
                }

                var records = new List<TransferInputDto>();

                using var reader = new StreamReader(string.Concat(csvPath, fileName));
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false, Delimiter = ","};
                using var csv = new CsvReader(reader, config);
                records = csv.GetRecords<TransferInputDto>().ToList();

                return await Task.FromResult(records);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
