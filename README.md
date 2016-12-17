# Queens
Comparing threaded (with async/await) vs non threaded 8 queens for C#
Both .Net core and 4.6.2 projects are included in the solution, they have exact same code.
 
 There are three different implementations in this solutions.

 1. Smartersolver
 Uses an array to hold all positions, the position in the array is the row of the queen and the value is the column.
 It only places a queen in a column if it's valid based in all ealier placed queens positions

 2. Bruteforce
 The basic way of bruteforceing all solutions and testing if they are valid.
 THe bruteforce solver creates one thread per column (i.e. 8x8 board results in 8 threads) and gives each thread a board where a queen is placed on the top row in the column corresponding to thread number.
 So thread 0 gets a board where the first queen is placed in columne 0. The third thread a board where the queen is placed in column 2.

 3. Bruteforce with Producer - Consumer 
 The producers produce all possible boards (like the regular bruteforce method) and place them in the queue. The consumers take from the queue and checks if the board is valid.

 

 Result from one run on a Surface Pro 3, i5, 8x8 board. 
 All times in ms.

 | Method					| 4.6.2		| .Net Core |
 |--------------------------|-----------|-----------|
 | Smarter	NotThreaded		| 0			| 1			|
 | Smarter	Threaded		| 12		| 28		|
 | Bruteforce NotThreaded	| 371		| 349		|
 | Bruteforce Threaded		| 329		| 234		|
 | P/C	Threaded (P:1 C:4)  | 6620		| 5222		|
 
