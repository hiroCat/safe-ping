namespace Shared

open System
open System.Net.NetworkInformation

type Status = OK=0 | KO=1 | TO=2

type EndpointStatus =
    {   Name: string
        Status: Status}

type EnviromentStatus =
    {   Id: Guid
        Name: string
        PingStatus: Status
        TimeStamp: DateTime
        EndpointsQ: EndpointStatus list}

//Add get support 
type ApiEndpConfig =
    {   Name: string
        Endpoint : string
        Body : string }

type EnviromentConfig =
    {   Id:Guid
        Name : string
        CreationDate : DateTime
        Ip: string
        ApiEndpointsConfig: ApiEndpConfig list}


module other = 
    open System.Net

    let status (st:IPStatus) =
        match st with
        | IPStatus.Success -> Status.OK
        | IPStatus.TimedOut -> Status.TO
        | _ -> Status.KO

    let statusC (st:HttpStatusCode) =
        match st with
        | HttpStatusCode.OK -> Status.OK
        | HttpStatusCode.RequestTimeout
        | HttpStatusCode.GatewayTimeout -> Status.TO
        | _ -> Status.KO

