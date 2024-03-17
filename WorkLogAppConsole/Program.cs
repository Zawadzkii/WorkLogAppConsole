using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
	static Dictionary<string, string> users = new Dictionary<string, string>(); //Dictionary storing usernames and passwords
	static Dictionary<string, List<string>> wordLogs = new Dictionary<string, List<string>>(); //Dictionary storing user word logs

	static void Main(string[] args)
	{
		InitializeUsers(); // Initialize users (add sample account)
		LoadWordLogs(); // Load existing word logs from file

		bool loggedIn = false;
		string username = "";

		while (!loggedIn)
		{
			Console.WriteLine("Welcome to WordLogApp!");
			Console.WriteLine("1. Log in");
			Console.WriteLine("2. Register new account");
			Console.Write("Choose an option: ");

			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					Console.Write("Enter username: ");
					username = Console.ReadLine();
					Console.Write("Enter password: ");
					string password = Console.ReadLine();
					loggedIn = Login(username, password);
					break;
				case "2":
					Console.Write("Enter new username: ");
					string newUsername = Console.ReadLine();
					Console.Write("Enter password: ");
					string newPassword = Console.ReadLine();
					Register(newUsername, newPassword);
					Console.WriteLine("Account successfully registered.");
					break;
				default:
					Console.WriteLine("Invalid choice. Please choose again.");
					break;


			}
		}

		while (true)
		{
			Console.WriteLine("Please tell me, employee of Dominik Bretschneider, which position out of five (1,2,3,4) would you like to choose :)");
			Console.WriteLine("1. Work hours registration");
			Console.WriteLine("2. Building Site Address");
			Console.WriteLine("3. Kind of work");
			Console.WriteLine("4. Report export");
			string option = Console.ReadLine();

			switch (option)
			{
				case "1":
					Console.Write("number of hours:");
					string newWord = Console.ReadLine();
					
					AddWordToLog(username, newWord);
					Console.WriteLine("The word has been added to the app...:)..");
					break;
				
				case "2":
					Console.Write("Street address:, city:");
					string neWord = Console.ReadLine();
					 
					
					Console.WriteLine("The word has been added to the app...:)..");
					break;


				case "3":
					Console.Write(" Floor layers: , Tilers: ,Drywall contractors:, Painters:");
					string newWo = Console.ReadLine();
					break;


				case "4":
					SaveWordLogs(); // Saving the history of added words to a file before logging out
					Console.WriteLine("You have been logged out.");
					return;
				default:
					Console.WriteLine("Invalid choice. Please choose again.");
					break;
			}
		}
	}

	static void InitializeUsers()
	{
		users.Add("user1", "password1"); 
	}

	static bool Login(string username, string password)
	{
		if (users.ContainsKey(username) && users[username] == password)
		{
			return true;
		}
		else
		{
			Console.WriteLine("Incorrect username or password. Please try again.");
			return false;
		}
	}

	static void Register(string username, string password)
	{
		if (!users.ContainsKey(username))
		{
			users.Add(username, password);
			wordLogs.Add(username, new List<string>()); 
		}
		else
		{
			Console.WriteLine("The user with the given username already exists. Please choose a different username.");
		}
	}

	static void AddWordToLog(string username, string word)
	{
		wordLogs[username].Add(word);
	}

	static void DisplayWordLog(string username)
	{
		if (wordLogs.ContainsKey(username))
		{
			List<string> userWordLog = wordLogs[username];
			if (userWordLog.Any())
			{
				Console.WriteLine("History of added words: "
);
				foreach (var word in userWordLog)
				{
					Console.WriteLine("- " + word);
				}
			}
			else
			{
				Console.WriteLine("No words added yet.");
			}
		}
		else
		{
			Console.WriteLine("There is no word log for the given user.");
		}
	}

	static void LoadWordLogs()
	{
		if (File.Exists("wordlogs.txt"))
		{
			string[] lines = File.ReadAllLines("wordlogs.txt");
			foreach (var line in lines)
			{
				string[] parts = line.Split(':');
				string username = parts[0];
				List<string> userWordLog = parts[1].Split(',').ToList();
				wordLogs.Add(username, userWordLog);
			}
		}
	}

	static void SaveWordLogs()
	{
		using (StreamWriter writer = new StreamWriter("wordlogs.txt"))
		{
			foreach (var entry in wordLogs)
			{
				string username = entry.Key;
				string wordLog = string.Join(",", entry.Value);
				writer.WriteLine(username + ":" + wordLog);
			}
		}
	}
}