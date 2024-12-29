using System;
using System.Collections.Generic;
using System.Linq;

namespace Distributor
{
	internal static class Program
	{
		private static Random rnd = new Random();

		static void Main()
		{
			Console.Write("Введите количество вопросов: ");
			int numQuestions = GetPositiveIntInput();

			Console.Write("Введите количество участников: ");
			int numParticipants = GetPositiveIntInput();

			Dictionary<int, string> questionsPerParticipant = new Dictionary<int, string>();

			List<int> quesPerParticipants = new List<int>();
			
			int remainder = numQuestions % numParticipants;

			List<string> questions = new List<string>();

            int questionsPerParticipantCount = remainder < 1? numQuestions / numParticipants : numQuestions / numParticipants + 1;
            questions.Add($"{1}-{questionsPerParticipantCount}");

			int lastQuestion = questionsPerParticipantCount;

            for (int i = 1; i < numParticipants; i++)
			{
                questionsPerParticipantCount = i + 1 > remainder ? numQuestions / numParticipants : numQuestions / numParticipants + 1;
				questions.Add($"{lastQuestion + 1}-{lastQuestion + questionsPerParticipantCount}");
                lastQuestion += questionsPerParticipantCount;
            }

			List<int> participants = Enumerable.Range(1, numParticipants).ToList();

			Shuffle(participants);

			for (int i = 0; i < participants.Count; i++)
			{
				questionsPerParticipant.Add(participants[i], questions[i]);
			}

			PrintResult(questionsPerParticipant);

			Console.ReadLine();
		}

		static void PrintResult(Dictionary<int, string> dictionary)
		{
            Console.WriteLine("Участник: Вопросы");

            foreach (var item in dictionary.OrderBy(kv => kv.Key))
            {
                Console.WriteLine($"Участник {item.Key}: {item.Value}");
            }
        }

		static int GetPositiveIntInput()
		{
			int result;
			while (!int.TryParse(Console.ReadLine(), out result) || result <= 0)
			{
				Console.Write("Введите положительное целое число: ");
			}
			return result;
		}

		static void Shuffle<T>(List<T> array)
		{
			int n = array.Count;
			while (n > 1)
			{
				int k = rnd.Next(n--);
				T temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}
	}
}
