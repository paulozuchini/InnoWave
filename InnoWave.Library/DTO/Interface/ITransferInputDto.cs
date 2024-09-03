using CsvHelper.Configuration.Attributes;

namespace InnoWave.Library.DTO.Interface
{
    public interface ITransferInputDto
    {
        string AccountID { get; set; }

        string TransferID { get; set; }

        decimal TotalTransferAmount { get; set; }
    }
}
