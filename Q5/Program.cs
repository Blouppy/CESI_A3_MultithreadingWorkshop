// Semaphore: https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphore?view=net-10.0
// semaphore.WaitOne(): https://learn.microsoft.com/en-us/dotnet/api/system.threading.waithandle.waitone?view=net-10.0#system-threading-waithandle-waitone(system-int32-system-boolean)

var semaphore = new Semaphore(initialCount: 3, maximumCount: 3);
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

    if (semaphore.WaitOne(millisecondsTimeout: 200))
    {
        FctExclusiveAccess(name);
    
        semaphore.Release();
    }
    else
    {
        // The else block is not thread-safe as we don't use the semaphore.
        // So ForegroundColor and ResetColor may be interleaved with other threads outputs
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Thread {0} Delay > 200ms", name);
        Console.ResetColor();
    }
    
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