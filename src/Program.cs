Console.WriteLine("Starting dddns");

using Utils utils = new();
using PeriodicTimer timer = new(new TimeSpan(1, 0, 0));

while (true)
{
    Console.WriteLine("Checking IPs...");

    var hostIp = await utils.GetHostIp();
    var currentIp = await utils.GetCurrentIp();

    Console.WriteLine($"Host IP: {hostIp}, current IP: {currentIp}");

    if (hostIp != currentIp)
    {
        Console.WriteLine("IP has changed, updating DNS...");
        await utils.UpdateHostIp(currentIp);
        Console.WriteLine("Done!");
    }

    Console.WriteLine("Waiting until next scheduled check time");
    await timer.WaitForNextTickAsync();
}
