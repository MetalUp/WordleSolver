WordleSolver - An automated solver for Wordle Puzzles that never takes > 5 attempts, and with average of 3.55

The program will open a console window, and after a short delay the solver will display its first attempt word. You should then enter this attempt into the online Wordle Puzzle to get it ‘marked’ (each letter as green, yellow, or grey).

Back on the Wordle Solver screen, you must then enter these marks on the line underneath the attempt word  (the cursor will already be positioned there), character by character, using the symbols:

* for an in-place match (equivalent to a ‘green’ on the online puzzle)
+ for an out-of-place match (equivalent to a ‘yellow’ in the online puzzle)
_ (underscore) for a non-match (equivalent to a ‘grey’ in the online puzzle)

and then hit Return/Enter after entering five symbols. The solver will respond with its next attempt word. This process continues until the answer is identified – entering ***** will then just exit the program. 

Running the C# version
To run this as-is, you will need Visual Studio, with .NET 6.0 (or later) and C# 9 (or later) already installed. Then just open the Wordle_Solver.csproj file with Visual Studio and Run the project to see the console.

Running the VB.NET version
To run this as-is, you will need Visual Studio, with .NET 6.0 (or later) hen just open the Wordle_Solver.vbproj file with Visual Studio and Run the project to see the console.

Running the Python version
The code is set up to run as a Visual Studio project, though you will need to have set up Visual Studio to run Python projects. Alternatively you can simply copy the program.py file from the repository and run it within your own preferred Python IDE. Please note, the Python version runs significantly slower than the other two - frustratingly slow. So the console program has been modified in two ways:

1. The first attempt word is  'hard-wired' RAISE (which the solver would always come up with anyway)
2. The program runs in the equivalent of Wordle's 'hard' mode. This is much faster because the attempt words evaluated are limited to
words that could be a possible answer. However, this is provably less effective both in terms of the average number of attempts required
to solve a puzzle, and the percentage that are solved in six attempts or fewer.


