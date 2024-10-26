namespace BrainNoob;

public static class Extensions
{
    public static IEnumerable<T> DeleteAt<T>(this IEnumerable<T> source, int index) => source.Skip(index);

    public static IEnumerable<T> DeleteAt<T>(this IEnumerable<T> source, int startIndex, int endIndex)
    {
        var array = source.ToArray();
        if (startIndex < 0 || endIndex >= array.Length || startIndex > endIndex)
            throw new ArgumentOutOfRangeException("Индексы выходят за пределы массива.");
        return array.Take(startIndex).Concat(array.Skip(endIndex + 1));
    }

    public static T[] InsertAt<T>(this T[] originalArray, T[] arrayToInsert, int insertIndex)
    {
        // Проверка на корректность индекса
        if (insertIndex < 0 || insertIndex > originalArray.Length)
            throw new ArgumentOutOfRangeException(nameof(insertIndex), "Индекс должен быть в пределах массива.");

        // Создаем новый массив с учетом вставляемых элементов
        var newArray = new T[originalArray.Length + arrayToInsert.Length];

        // Копируем элементы до insertIndex
        Array.Copy(originalArray, 0, newArray, 0, insertIndex);

        // Вставляем элементы из arrayToInsert
        Array.Copy(arrayToInsert, 0, newArray, insertIndex, arrayToInsert.Length);

        // Копируем оставшиеся элементы из originalArray
        Array.Copy(originalArray, insertIndex, newArray, insertIndex + arrayToInsert.Length,
            originalArray.Length - insertIndex);

        return newArray;
    }
}