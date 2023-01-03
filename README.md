# WordleSolver - An automated solver for Wordle Puzzles that never takes > 5 attempts, and with average of 3.55

To run this as-is, you will need Visual Studio, with .NET 6.0 (or later) and C# 9 (or later) already installed. You may either replicate the repository, or download a .zip of the code and unzip it on your local system. Then just open the Wordle_Solver.csproj file with Visual Studio and Run the project to see the console.

The program will open a console window, and after a short delay the solver will display its first attempt word. You should then enter this attempt into the online Wordle Puzzle to get it ‘marked’ (each letter as green, yellow, or grey).

Back on the Wordle Solver screen, you must then enter these marks on the line underneath the attempt word  (the cursor will already be positioned there), character by character, using the symbols:

* for an in-place match (equivalent to a ‘green’ on the online puzzle)
+ for an out-of-place match (equivalent to a ‘yellow’ in the online puzzle)
_ (underscore) for a non-match (equivalent to a ‘grey’ in the online puzzle)

and then hit Return/Enter after entering five symbols. The solver will respond with its next attempt word. This process continues until the answer is identified – entering ***** will then just exit the program. 

After running you may also open Test > Test Explorer and then run the unit tests to test all 14 of the core functions (which are located in WordleFunctions.cs).
Finally, you can edit the Progam.cs file, commenting-out the 9-line main program, and uncomment the ‘alternative program’ to analyse the performance of the solver for all 2,309 possible answer words and run it again (this may take 20+ minutes to complete). See Effectiveness of solution for more on this.
