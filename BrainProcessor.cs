namespace BrainNoob;

public class BrainProcessor(string noobCode)
{
    const char CLEAR_MEMORY_OPERATOR = 'K';
    const char FUNCTION_MARK = '$';

    private readonly Dictionary<string, string> Methods = [];

    /// <param name="noobCode">source code in BrainNoob language</param>
    /// <returns>code in BrainFuck</returns>
    public string Process()
    {
        var charArr = noobCode.ToCharArray();
        for (int i = 0; i < charArr.Length; i++)
        {
            var @char = charArr[i];
            if (@char is not FUNCTION_MARK) continue;

            var methodName =
                new string(charArr.SkipWhile((_, id) => id <= i).TakeWhile(ch => ch is not FUNCTION_MARK).ToArray());

            if (AppendMethod(methodName, ref i, ref charArr)) continue;

            if (MemoMethod(methodName, ref i, ref charArr)) continue;
        }

        return new string(charArr);
    }

    private bool MemoMethod(string methodName, ref int i, ref char[] charArr)
    {
        var startIndex = i;

        var afterMethodName =
            new string(charArr.SkipWhile((_, id) => id < startIndex + methodName.Length + 2).ToArray());
        var endIndex = afterMethodName.IndexOf(FUNCTION_MARK + methodName + FUNCTION_MARK,
            StringComparison.InvariantCulture);
        //TODO: throw exception if method is not closed (endIndex = -1)
        var methodBody = afterMethodName[..endIndex];

        var methodBodyEndIndex = i + methodBody.Length + (methodName.Length + 2) * 2;
        var _i = i;
        charArr = charArr.Where((_, id) => id < _i || id >= methodBodyEndIndex).ToArray();

        ClearMethodMemoryAlocks(ref methodBody);
        Methods.Add(methodName, methodBody + Environment.NewLine);
        i--;
        return true;
    }

    private void ClearMethodMemoryAlocks(ref string methodBody)
    {
        if (!methodBody.Contains(CLEAR_MEMORY_OPERATOR)) return;

        var clearBody = methodBody;

        var charArr = clearBody.ToCharArray();
        bool isInClearBlock = false;
        for (int i = 0; i < charArr.Length; i++)
        {
            var @char = charArr[i];
            if (@char is CLEAR_MEMORY_OPERATOR)
            {
                isInClearBlock = !isInClearBlock;
                continue;
            }

            if (!isInClearBlock) continue;

            charArr[i] = @char switch
            {
                '+' => '-',
                '-' => '+',
                _ => throw new ArgumentOutOfRangeException($"Operator {@char} is not supported by K operator.")
            };
        }

        clearBody = new string(charArr);

        methodBody += ' ';
        methodBody += clearBody;

        methodBody = methodBody.Replace(new string([CLEAR_MEMORY_OPERATOR]), string.Empty);
    }

    private bool AppendMethod(string methodName, ref int i, ref char[] charArr)
    {
        if (Methods.TryGetValue(methodName, out var method))
        {
            var methodNameEndIndex = i + methodName.Length + 2; // 2 is length or opening $ + closing $
            var _i = i;
            charArr = charArr.Where((_, id) => id < _i || id >= methodNameEndIndex).ToArray();
            charArr = charArr.InsertAt(method.ToCharArray(), i);

            i += method.Length - 1;
            return true;
        }

        return false;
    }
}