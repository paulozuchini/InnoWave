using CsvHelper.Configuration.Attributes;
using InnoWave.Library.DTO.Interface;

namespace InnoWave.Library.DTO
{
    [Delimiter(",")]
    [CultureInfo("InvariantCulture")]
    public class TransferInputDto : ITransferInputDto
    {
        [Name("Account_ID")]
        public string AccountID { get; set; }

        [Name("Transfer_ID")]
        public string TransferID { get; set; }

        [Name("Total_Transfer_Amount")]
        public decimal TotalTransferAmount { get; set; }
    }
}
