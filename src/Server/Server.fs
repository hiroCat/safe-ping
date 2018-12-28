open System.IO
open System.Threading.Tasks

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2
open Giraffe
open Saturn
open Shared
open System

let publicPath = Path.GetFullPath "../Client/public"
let port = 8085us

let stEnd (envSt:ApiEndpConfig) =
    let (r,res) = Helpers.reqPost envSt.Endpoint envSt.Body
    {Name = envSt.Name; Status = (r.StatusCode |> other.statusC )}

let st (config:EnviromentConfig) =
    {   Id = new Guid();
        Name = config.Name;
        PingStatus = (Helpers.IpUp config.Ip 1) |> other.status;
        TimeStamp = DateTime.Now;
        EndpointsQ = (config.ApiEndpointsConfig |> List.map stEnd)}

let getDataFun (enviromentConfigs:EnviromentConfig list) = enviromentConfigs |> List.map st

let InitStatus() : Task<EnviromentStatus list> =
    task {  Helpers.obs getDataFun
            return  List.Empty }

let webApp = router {
    get "/api/init-process" (fun next ctx ->
        task {
            InitStatus()
            return! Successful.OK "" next ctx
        })
    get "/api/lastVal" (fun next ctx ->
        task {
            return! Successful.OK None next ctx
        })
}

let configureSerialization (services:IServiceCollection) =
    services.AddSingleton<Giraffe.Serialization.Json.IJsonSerializer>(Thoth.Json.Giraffe.ThothSerializer())

let app = application {
    url ("http://0.0.0.0:" + port.ToString() + "/")
    use_router webApp
    memory_cache
    use_static publicPath
    service_config configureSerialization
    use_gzip
}

run app
