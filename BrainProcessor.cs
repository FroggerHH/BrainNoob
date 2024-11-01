using System.Text;

namespace BrainNoob;

public class BrainProcessor(string noobCode)
{
    private const char ClearMemoryOperator = 'K';
    private const char FunctionMark = '$';
    private readonly Dictionary<string, string> _methods = new();

    /// <summary>
    /// Processes BrainNoob code and converts it to Brainfuck.
    /// </summary>
    /// <returns>Converted Brainfuck code.</returns>
    public string Process()
    {
        var resultBuilder = new StringBuilder();
        var codeLength = noobCode.Length;

        for (int i = 0; i < codeLength; i++)
        {
            var currentChar = noobCode[i];

            if (currentChar == FunctionMark)
            {
                var methodName = ExtractMethodName(ref i);

                if (TryAppendMethod(resultBuilder, methodName)) continue;

                DefineMethod(methodName, ref i);
            }
            else resultBuilder.Append(currentChar);
        }

        return resultBuilder.ToString();
    }

    private string ExtractMethodName(ref int i)
    {
        var methodNameBuilder = new StringBuilder();
        i++; // Move past initial FunctionMark

        while (i < noobCode.Length && noobCode[i] != FunctionMark)
        {
            methodNameBuilder.Append(noobCode[i]);
            i++;
        }

        return methodNameBuilder.ToString();
    }

    private bool TryAppendMethod(StringBuilder resultBuilder, string methodName)
    {
        if (_methods.TryGetValue(methodName, out var methodBody))
        {
            resultBuilder.Append(methodBody);
            return true;
        }

        return false;
    }

    private void DefineMethod(string methodName, ref int i)
    {
        var methodBodyBuilder = new StringBuilder();
        i++; // Move past closing FunctionMark of methodName

        while (i < noobCode.Length)
        {
            var methodFullTag = $"{FunctionMark}{methodName}{FunctionMark}";
            if (noobCode[i] == FunctionMark && noobCode.Substring(i, methodName.Length + 2) == methodFullTag)
            {
                i += methodName.Length + 1; // Move past closing FunctionMark sequence
                break;
            }

            methodBodyBuilder.Append(noobCode[i]);
            i++;
        }

        var methodBody = methodBodyBuilder.ToString();
        methodBody = ClearMethodMemoryBlocks(methodBody);
        _methods[methodName] = methodBody;
    }

    private static string ClearMethodMemoryBlocks(string methodBody)
    {
        if (!methodBody.Contains(ClearMemoryOperator)) return methodBody;

        var clearBodyBuilder = new StringBuilder(methodBody.Length);

        bool isInClearBlock = false;
        foreach (var ch in methodBody)
        {
            if (ch == ClearMemoryOperator)
            {
                isInClearBlock = !isInClearBlock;
                continue;
            }

            if (isInClearBlock)
            {
                clearBodyBuilder.Append(ch switch
                {
                    '+' => '-',
                    '-' => '+',
                    _ => throw new ArgumentOutOfRangeException($"Operator {ch} is not supported by K operator.")
                });
            }
            else
            {
                clearBodyBuilder.Append(ch);
            }
        }

        methodBody += ' ' + clearBodyBuilder.ToString();
        return methodBody;
    }
}