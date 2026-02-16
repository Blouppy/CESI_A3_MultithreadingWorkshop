// Mutex: https://learn.microsoft.com/en-us/dotnet/api/system.threading.mutex?view=net-10.0

// Mutex can be used for inter-process synchronization
// unlike Monitor and Lock which are limited to inter-thread synchronization within the same process.


// Run the executable multiple times to see the inter-process synchronization in action.
// (=> in bin\Debug\net10.0)

var mutex = new Mutex(
    initiallyOwned: false,
    name: "Q3_Mutex_InterProcess",
    out var createdNew);

if (createdNew)
{
    Console.WriteLine("This is the first instance of the application.");
}
else
{
    Console.WriteLine("Another instance of the application is already running.");
    Console.ReadLine();
    return;
}

Console.WriteLine("Do some work here...");
Console.ReadLine();

mutex.ReleaseMutex();