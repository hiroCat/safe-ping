module Helpers
    open System.Net.NetworkInformation
    open System.Net
    open System.Reactive.Linq
    open System
    open System.Reactive.Concurrency
    open System.IO
    open System.Text
    open System.Net

    let IpUp (ip:string) (t:int) =
        let pingSender = new Ping()
        let reply = pingSender.Send(IPAddress.Parse(ip),t*1000)
        reply.Status

    let obs f =  Observable
                    .Interval(TimeSpan.FromMinutes(1.0), Scheduler.Default)
                    .StartWith(0L)
                    .Publish()
                    .RefCount()
                    .Select(fun _ -> f)

    //from f# snippets
    let reqPost (url:string) (body:string)=
        let req = HttpWebRequest.Create(url) :?> HttpWebRequest
        req.ProtocolVersion <- HttpVersion.Version10
        req.Method <- "POST"
        let postBytes = Encoding.ASCII.GetBytes(body)
        req.ContentType <- "application/json";
        req.ContentLength <- int64 postBytes.Length

        let reqStream = req.GetRequestStream() 
        reqStream.Write(postBytes, 0, postBytes.Length);
        reqStream.Close()

        let resp = req.GetResponse() 
        let stream = resp.GetResponseStream() 
        let reader = new StreamReader(stream) 
        resp:?>HttpWebResponse,reader.ReadToEnd()
