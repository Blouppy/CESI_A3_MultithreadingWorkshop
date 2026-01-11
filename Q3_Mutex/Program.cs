// Mutex: https://learn.microsoft.com/en-us/dotnet/api/system.threading.mutex?view=net-10.0

// Mutex can be used for inter-process synchronization
// unlike Monitor and Lock which are limited to inter-thread synchronization within the same process.
// Example of shared Mutex across processes:
//     var mutex = new Mutex(initiallyOwned: false, name: @"Q3_Mutex");
// Next, run the executable multiple times to see the inter-process synchronization in action.

var mutex = new Mutex();
var nbThreadInProgress = 0;
var countExclusiveAccess = 0;

const int nbThread = 300;
for (var i = 0; i < nbThread; i++)
{
    var myThreadFct = new Thread(() => FctA($"name_{i}"));
    myThreadFct.Start();
    Thread.Sleep(10);
}

return;

void FctA(string name)
{
    Console.WriteLine("Thread {0} is at the start of FctA: {1}", name, Interlocked.Increment(ref nbThreadInProgress));

    mutex.WaitOne();

    FctExclusiveAccess(name);
    
    mutex.ReleaseMutex();

    Console.WriteLine("Thread {0} is at the end of FctA: {1}", name, Interlocked.Decrement(ref nbThreadInProgress));
}

void FctExclusiveAccess(string name)
{
    Console.WriteLine("Thread {0} is entering into the exclusive access zone", name);

    Interlocked.Increment(ref countExclusiveAccess);

    if (countExclusiveAccess > 1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error: There are {0} in the exclusive access zone!", countExclusiveAccess);
        Console.ResetColor();
    }

    Interlocked.Decrement(ref countExclusiveAccess);

    Thread.Sleep(50);

    Console.WriteLine("Thread {0} is leaving the exclusive access zone", name);
}