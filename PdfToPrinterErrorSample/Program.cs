using PDFtoPrinter;
using System.Reflection;
string printerName;
string filePath;
Console.WriteLine($"args:{string.Join(", ", args)}");
Console.WriteLine($"BaseDirectory: {AppContext.BaseDirectory}");
Console.WriteLine($"Assembly.Location: {(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location}");
AppContext.SetSwitch("Switch.System.Reflection.Assembly.SimulatedLocationInBaseDirectory", true);
Console.WriteLine($"(fallback) BaseDirectory: {AppContext.BaseDirectory}");
if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
{
    Console.WriteLine("arg 1: printerName");
    Console.WriteLine("arg 2: input pdf file name (option)");
    return;
}
if (args.Length == 1)
{
    printerName = args[0];
    string? filePath_;
    do
    {
        Console.WriteLine("input pdf filepath.");
        filePath_ = Console.ReadLine();
        if (string.IsNullOrEmpty(filePath_))
        {
            Console.WriteLine("exit.");
            return;
        }
        if (!File.Exists(filePath_))
        {
            Console.Error.WriteLine($"not found filePath: {filePath_}");
            filePath_ = null;
        }
    } while (string.IsNullOrEmpty(filePath_));
    filePath = filePath_;
}
else
if (args.Length != 2)
{
    throw new ArgumentException("required argument 2.");
}
else
{
    printerName = args[0];
    filePath = args[1];
    if (!File.Exists(filePath))
        throw new FileNotFoundException($"{filePath} is not found.");
}
try
{
    var printer = new PDFtoPrinterPrinter();
    PrintingOptions option = new(printerName, filePath);
    Console.WriteLine($"use option {option}");
    printer.Print(new(printerName, filePath));
}
catch
{
    throw;
}