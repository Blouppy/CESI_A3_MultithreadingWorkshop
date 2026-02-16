// Lock: https://learn.microsoft.com/en-us/dotnet/api/system.threading.lock?view=net-10.0

var lockObj = new Lock();
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
    
    lock (lockObj)
    {
        FctExclusiveAccess(name);
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