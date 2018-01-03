using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;

/*
    Sudoku solver using A recursive backtracking algorithm. The program is ported and adapted from C source code through the following article:
    https://spin.atomicobject.com/2012/06/18/solving-sudoku-in-c-with-recursive-backtracking/
    Optimization was attempted by considering class based implementation of sudoku cells.
    For comparison, puzzles of the following source where solved:
    https://www.dcc.fc.up.pt/~acm/sudoku.pdf 
    This algorithm seems to be about 8 times slower.
    The main Program class
    Contains two methods, both executed in main. The backtrack algorithm is run on the main thread, and A second thread for displaying the progress.
*/

/// <summary>
/// The main <c>Program</c> class.
/// Contains two methods, both executed in main. The algorithm runs on the main thread. A second thread displays the progress asynchronously.
/// <list type="bullet">
/// <item>
/// <term>Main</term>
/// <description>Main entry point for this program.</description>
/// </item>
/// <item>
/// <term>solve</term>
/// <description>The solve function initiates the recursion/backtracking algorithm.</description>
/// </item>
/// <item>
/// <term>showProgress</term>
/// <description>displays the intermediate result of the puzzle.</description>
/// </item>
/// </list>
/// </summary>

public class Program
{
    ///<summary>
    ///A trigger used in both threads. terminates <see cref="showProgress"/> when A solution is found.
    ///</summary>

    static bool foundSolution = false;

    ///<summary>
    ///Main entry point for this program.
    ///</summary>
    
    static void Main(string[] args)
    {
        //puzzle input
        Console.WriteLine("enter puzzle text file name,eg: puzzle.txt\n");
        string filename = Console.ReadLine().ToString();

        while (!File.Exists(filename))
        {
            Console.WriteLine("\ninvalid filename, please try again:\n");
            filename = Console.ReadLine().ToString();
        }

        Sudoku.Board.init(filename);

        Console.WriteLine(Sudoku.Board.display() + "\n\nSudoku has " + Sudoku.Board.clues + " clues\npress any key to solve puzzle\n");
        Console.ReadKey();

        //show progress via A seperate thread.
        Thread progressIndicator = new Thread(showProgress);
        progressIndicator.Start();

        //performance timing
        var watch = System.Diagnostics.Stopwatch.StartNew();

        //solve on main thread
        solve();

        //stop timer
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;

        //output
        Console.Clear();
        Console.WriteLine(Sudoku.Board.display() + "\n\nDone\nSolved in " + elapsedMs + " milliseconds");
        Console.ReadKey();
    }

    ///<summary>
    ///initiates the algorithm.
    ///</summary>
    
    static void solve()
    {
        foundSolution = Sudoku.Board.recurse(0, 0);
    }

    ///<summary>
    ///displays the intermediate result of the puzzle.
    ///</summary>
    
    static void showProgress()
    {
        int counter = 0;

        while (!foundSolution)
        {
            counter += 16;
            if (counter % 100000000 == 0)
            {
                Console.Clear();
                Console.WriteLine(Sudoku.Board.display());
            }
        }
    }
}
