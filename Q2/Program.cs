// Interlocked: https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked?view=net-10.0

var nbThreadInProgress = 0;

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
    // We protect nbThreadInProgress with Interlocked
    // Now the value is thread-safe
    Console.WriteLine("Thread {0} is at the start of FctA: {1}", name, Interlocked.Increment(ref nbThreadInProgress));
    
    Thread.Sleep(30);
    
    Console.WriteLine("Thread {0} is at the end of FctA: {1}", name, Interlocked.Decrement(ref nbThreadInProgress));
}