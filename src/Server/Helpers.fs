module Helpers
    open System.Net.NetworkInformation
    open System.Net
    //open System.Reactive.Linq

    let IpUp (ip:string) (t:int) =
        let pingSender = new Ping()
        let reply = pingSender.Send(IPAddress.Parse(ip),t*1000)
        reply.Status = IPStatus.Success

    let p f =  Observable
                .Interval(TimeSpan.FromMinutes(1.0), Scheduler.Default)
                .StartWith(0L)
                .Publish()
                .RefCount()
                .Select(fun _ -> f) 