using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WordSorterApp.Models;

namespace WordSorterApp.Services
{
    public class WordSorter
    {
        // Быстрая сортировка (QuickSort)
        public (List<SortResult>, long) QuickSortWords(string text)
        {
            var stopwatch = Stopwatch.StartNew();
            var words = SplitText(text);

            if (words.Length == 0)
                return (new List<SortResult>(), 0);

            QuickSort(words, 0, words.Length - 1);

            var result = CountWords(words);
            stopwatch.Stop();

            return (result, stopwatch.ElapsedMilliseconds);
        }

        // ABC-сортировка (поразрядная для строк)
        public (List<SortResult>, long) AbcSortWords(string text)
        {
            var stopwatch = Stopwatch.StartNew();
            var words = SplitText(text);

            if (words.Length == 0)
                return (new List<SortResult>(), 0);

            var sortedWords = AbcSort(words.ToList());

            var result = CountWords(sortedWords.ToArray());
            stopwatch.Stop();

            return (result, stopwatch.ElapsedMilliseconds);
        }

        private string[] SplitText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new string[0];

            // Разбиваем текст на слова, игнорируя пунктуацию и пробелы
            char[] separators = { ' ', ',', '.', '!', '?', ';', ':', '\t', '\n', '\r' };
            return text.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                      .Where(word => !string.IsNullOrWhiteSpace(word))
                      .Select(word => word.ToLower())
                      .ToArray();
        }

        private void QuickSort(string[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right);
                QuickSort(arr, left, pivotIndex - 1);
                QuickSort(arr, pivotIndex + 1, right);
            }
        }

        private int Partition(string[] arr, int left, int right)
        {
            string pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (string.Compare(arr[j], pivot, StringComparison.Ordinal) <= 0)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, right);
            return i + 1;
        }

        private void Swap(string[] arr, int i, int j)
        {
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }

        // ABC-сортировка (рекурсивная поразрядная сортировка для строк)
        private List<string> AbcSort(List<string> words, int position = 0)
        {
            if (words.Count <= 1)
                return words;

            var groups = new Dictionary<char, List<string>>();
            var result = new List<string>();
            var currentGroup = new List<string>();

            foreach (var word in words)
            {
                if (position >= word.Length)
                {
                    result.Add(word);
                    continue;
                }

                var currentChar = word[position];
                if (!groups.ContainsKey(currentChar))
                {
                    groups[currentChar] = new List<string>();
                }
                groups[currentChar].Add(word);
            }

            // Сортируем группы по символам
            var sortedGroups = groups.OrderBy(g => g.Key);

            foreach (var group in sortedGroups)
            {
                if (group.Value.Count > 1)
                {
                    result.AddRange(AbcSort(group.Value, position + 1));
                }
                else
                {
                    result.AddRange(group.Value);
                }
            }

            return result;
        }

        private List<SortResult> CountWords(string[] words)
        {
            var results = new List<SortResult>();
            if (words.Length == 0)
                return results;

            string currentWord = words[0];
            int count = 1;

            for (int i = 1; i < words.Length; i++)
            {
                if (words[i] == currentWord)
                {
                    count++;
                }
                else
                {
                    results.Add(new SortResult { Word = currentWord, Count = count });
                    currentWord = words[i];
                    count = 1;
                }
            }

            // Добавляем последнее слово
            results.Add(new SortResult { Word = currentWord, Count = count });
            return results;
        }
    }
}