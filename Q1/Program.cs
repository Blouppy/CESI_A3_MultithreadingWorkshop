// See https://aka.ms/new-console-template for more information

var nbThreadInProgress = 0;

const int nbThread = 300;
for (var i = 0; i < nbThread; i++)
{
    var myThreadFct = new Thread(() => FctA($"name_{i}"));
    // The execution order of threads is random. e.g: Thread 2 may be executed after Thread 3.
    myThreadFct.Start();
    Thread.Sleep(10);
}

return;

void FctA(string name)
{
    // Moreover, at the end of the execution, it is possible to have nbThreadInProgress != 0.  
    Console.WriteLine("Thread {0} is at the start of FctA: {1}", name, ++nbThreadInProgress);
    
    Thread.Sleep(30);
    
    Console.WriteLine("Thread {0} is at the end of FctA: {1}", name, --nbThreadInProgress);
}