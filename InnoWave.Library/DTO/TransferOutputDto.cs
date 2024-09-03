using InnoWave.Library.DTO.Interface;

namespace InnoWave.Library.DTO
{
    public class TransferOutputDto : ITransferOutputDto
    {
        public string Account_ID { get; set; }
        public decimal Total_Commission { get; set; }
    }
}
