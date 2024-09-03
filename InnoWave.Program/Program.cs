using Innowave.Service;
using Innowave.Service.Interface;

Console.WriteLine(DateTime.Now.ToString("yyyy/mm/dd HH:mm"));
Console.WriteLine("Welcome to the Transfer Batch Processing Program!");
Console.WriteLine("Please write the path to the `transfers.csv` file:");
string filePath = Console.ReadLine();

ITransferService transferService = new TransferService();

var csvResult = transferService.ReadCsvFile(filePath);

if (csvResult.IsFaulted)
{
    Console.WriteLine(csvResult.Exception.Message.ToString());
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey(true);
    return;
}

if (csvResult.Result.Count == 0)
{
    Console.WriteLine("Could not find any transfer to process.");
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey(true);
    return;
}

var batchResult = await transferService.CalculateTotalComissions(csvResult.Result);

Console.WriteLine($"Transfer Batch Result:");
foreach (var item in batchResult)
{
    Console.WriteLine(string.Join(",", item.Account_ID.ToString(), item.Total_Commission.ToString("0.00")));
}
Console.WriteLine("Press any key to exit...");
Console.ReadKey(true);
return;