using System;
using System.Collections.Generic;
using System.Linq;

public class WordLadder
{
    public static void Main()
    {
        var dictionary = new HashSet<string>();
        string line;
        while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()!))
        {
            dictionary.Add(line.Trim());
        }

        bool firstPair = true;
        while ((line = Console.ReadLine()!) != null)//чтение пар слов
        {        
            if (string.IsNullOrWhiteSpace(line)) continue;
            var words = line.Trim().Split();
            if (words.Length != 2) continue;  
            
            string start = words[0];
            string end = words[1];

            if (!firstPair)
            {
                Console.WriteLine();
            }
            firstPair = false;

            var ladder = FindShortestLadder(start, end, dictionary);

            if (ladder != null)
            {
                foreach (var word in ladder)
                {
                    Console.WriteLine(word);
                }
            }
            else
            {
                Console.WriteLine("No solution.");
            }
        }
    }

    private static List<string>? FindShortestLadder(string start, string end, HashSet<string> dictionary)// алгоритм поиска BFS
    {
        if (start == end) return new List<string> { start };
        if (!dictionary.Contains(end)) return null;

        var queue = new Queue<List<string>>();//последовательность слов
        queue.Enqueue(new List<string> { start });

        var visited = new HashSet<string> { start };//обработанные слова

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var currentWord = path[^1]; //последнее слово в пути

            //генерация всех возможных дублетов
            for (int i = 0; i < currentWord.Length; i++)
            {
                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (c == currentWord[i]) continue;

                    var nextWord = currentWord[..i] + c + currentWord[(i + 1)..];

                    if (dictionary.Contains(nextWord) && !visited.Contains(nextWord))
                    {
                        var newPath = new List<string>(path) { nextWord };

                        if (nextWord == end)
                        {
                            return newPath;
                        }

                        visited.Add(nextWord);
                        queue.Enqueue(newPath);
                    }
                }
            }
        }

        return null;
    }
}