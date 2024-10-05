/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/
/*
    Complete the code in Flock.cs. (1 function per group member; 
    bubblesort is a must - someone must choose this).
    Time the run for each function with 
    number of drones varying from 100 to 1000000.
    (run each multiple times - unless each run takes too long).
    Save result to CSV (google up).
    Plot the runtime for each function (using Excel/Sheet)
*/
using System.IO;
using System;
class HelloWorld {
  static void Main() {
      
      int numRepeat = 100;
      int max = 7000; //1000000;
      int min = 100;
      int stepsize = 100;
      int numsteps = (max-min) / stepsize;
      
      // repeat this declaration for all other functions
      float[] timeAverage = new float[numsteps];
      float[] timeMax = new float[numsteps];
      float[] timeMin = new float[numsteps];
      float[] timeBubbleSort = new float[numsteps];
      float[] timeInsertionSort = new float[numsteps];
      for (int i=0; i<numsteps; i++)
      {
          int numdrones = i * stepsize + min;
          Console.WriteLine("Current num drones = "+numdrones);
          
          Flock flock = new Flock(numdrones);
          flock.Init((int) (0.9*numdrones)); // fill up 90%
          
          // calculate time for average
          var watch = new System.Diagnostics.Stopwatch();
        //average
          watch.Start();
          for (int rep=0; rep<numRepeat; rep++)
          {
              flock.average();
          }
          watch.Stop();   
          long time = watch.ElapsedMilliseconds;
          // store value
          timeAverage[i] = time / numRepeat;
          
        //max
          watch.Restart();
          for (int rep = 0; rep < numRepeat; rep++) {
            flock.max();
          }
          watch.Stop();
          timeMax[i] = watch.ElapsedMilliseconds / (float)numRepeat;
        //min
          watch.Restart();
          for (int rep = 0; rep < numRepeat; rep++) {
              flock.min();
          }
          watch.Stop();
          timeMin[i] = watch.ElapsedMilliseconds / (float)numRepeat;
        //bubblesort
          watch.Restart();
          for (int rep = 0; rep < numRepeat; rep++) {
              flock.bubblesort();
          }
          watch.Stop();
          timeBubbleSort[i] = watch.ElapsedMilliseconds / (float)numRepeat;
        //insertionsort
          watch.Restart();
        for (int rep = 0; rep < numRepeat; rep++) {
              flock.insertionsort();
          }
          watch.Stop();
          timeInsertionSort[i] = watch.ElapsedMilliseconds / (float)numRepeat;
      }
      
      // write results to csv files
      // see https://www.csharptutorial.net/csharp-file/csharp-write-csv-files/
      string csvFilePath = "timing_results.csv";
        using (StreamWriter writer = new StreamWriter(csvFilePath)) {
            // Write CSV header
            writer.WriteLine("Number of Drones,Average (ms),Max (ms),Min (ms),BubbleSort (ms),InsertionSort (ms)");

            // Write the results to CSV
            for (int i = 0; i < numsteps; i++) {
                int numdrones = i * stepsize + min; // Calculate the current number of drones
                writer.WriteLine($"{numdrones},{timeAverage[i]},{timeMax[i]},{timeMin[i]},{timeBubbleSort[i]},{timeInsertionSort[i]}");
            }
        }
  }
}