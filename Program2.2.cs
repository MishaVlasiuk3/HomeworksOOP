// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using System;
using System.Runtime.ExceptionServices;

abstract class Worker
{
    public Worker(string name) { Name = name; Position = ""; WorkDay = " "; }
    public string Relax() { return "Relax..."; }
    public string Call() { return "Doing calls..."; }
    public string WriteCode() { return "Coding..."; }
    abstract public void FillWorkDay();

    public string Position { get; set; }
    public string Name { get; set; }
    public string WorkDay { get; set; }
}
class Manager : Worker
{
    public Manager(string s) : base(s) { Position = "Manager"; }
    private Random r = new Random();
    override public void FillWorkDay()
    {
        for (int i = 0; i < r.Next(1, 10); i++)
        {
            WorkDay += Call();
        }
        WorkDay += Relax();
        for (int i = 0; i < r.Next(1, 5); i++)
        {
            WorkDay += Call();
        }

    }
}
class Developer : Worker
{
    public Developer(string s) : base(s) { Position = "Developer"; }
    override public void FillWorkDay() { WorkDay = WriteCode() + Call() + Relax() + WriteCode(); }
}
class Team
{
    private static int peopleCurrentN;
    private const int teamCapacity = 5;
    public Team(string name) { Name = name; }
    private Worker[] team = new Worker[teamCapacity];
    public void addWorker(Worker w) { if (peopleCurrentN < teamCapacity) { team[peopleCurrentN] = w; peopleCurrentN++; } }
    public string Name { get; set; }
    public void fullTeamInfo()
    {
        Console.WriteLine($"Name of the team : {Name}");
        Console.WriteLine("Team squad: ");
        for (int i = 0; i < peopleCurrentN; i++)
        {
            Console.WriteLine("");
            team[i].FillWorkDay();
            Console.WriteLine($"Worker's name:{team[i].Name}, position: {team[i].Position}");
            Console.WriteLine($"WorkDay of {team[i].Name}:{team[i].WorkDay}");
            Console.WriteLine("");

        }
    }
    public void teamInfo()
    {
        Console.WriteLine($"Name of the team : {Name}");
        Console.WriteLine("Team squad: ");

        for (int i = 0; i < peopleCurrentN; i++)
        {
            Console.WriteLine($"Worker's name:{team[i].Name}");
        }
    }
    public void fillTeam()
    {
        uint a = 1;
        while (a > 0)
        {

            Console.WriteLine("Add worker? (1 or 0) 0 to quit");
            try
            {
                a = uint.Parse(Console.ReadLine());
                if (a == 0) { break; }
                Console.WriteLine("Enter name of Worker: ");
                string n = Console.ReadLine();
                Console.WriteLine("Manager or Developer? (1 or 0)");
                uint v = uint.Parse(Console.ReadLine());
                if (v == 1)
                {
                    addWorker(new Manager(n));
                }
                if (v == 0)
                {
                    addWorker(new Developer(n));
                }
            }
            catch (FormatException) 
            { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Input error!");Console.ResetColor(); }
            catch (IOException)
            { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Input error!"); Console.ResetColor(); }
            catch (ArgumentException)
            { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Input error!"); Console.ResetColor(); }
            

        }
    }

}


namespace HomeWork // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Team team1 = new Team("Team1");

            try { team1.fillTeam(); }
            catch (ArgumentOutOfRangeException) { Console.WriteLine("Something went wrong!"); }
            catch (ArgumentNullException) { Console.WriteLine("Something went wrong!"); }

            team1.fullTeamInfo();

        }
    }
}