Console.WriteLine("Starting dddns");

using Utils utils = new();
using PeriodicTimer timer = new(new TimeSpan(1, 0, 0));

while (true)
{
    var dnsIp = await utils.GetDnsIp();
    var currentIp = await utils.GetCurrentIp();

    if (dnsIp != currentIp)
    {
        Console.WriteLine($"{DateTime.Now}: IP for {utils.Hostname} in DNS: {dnsIp}. Current IP: {currentIp}. Updating DNS...");
        await utils.UpdateDnsIp(currentIp);
        Console.WriteLine($"{DateTime.Now}: Done!");
    }

    await timer.WaitForNextTickAsync();
}
