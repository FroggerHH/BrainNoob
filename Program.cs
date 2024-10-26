namespace BrainNoob;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: BrainNoob.exe <input-file>");
            return;
        }

        string noobCode = ReadFile(args[0]);
        string fuckCode = BrainProcessor.Process(noobCode);
        Console.WriteLine(fuckCode);
    }

    private static string ReadFile(string fileFullName)
    {
        FileStream? fileStream = null;
        StreamReader? streamReader = null;
        try
        {
            fileStream = new FileStream(fileFullName, FileMode.Open);
            streamReader = new StreamReader(fileStream);

            var fileContent = streamReader.ReadToEnd();
            fileContent = new string(fileContent
                .Replace(" ", string.Empty)
                .Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .ToCharArray()
                .ToArray());

            return fileContent;
        }
        finally
        {
            fileStream?.Dispose();
            streamReader?.Dispose();
        }
    }
}