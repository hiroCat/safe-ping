namespace Shared

open System

type Status = OK=0 | KO=1 

type EndpointStatus =
    {   Name: string
        Status: Status}

type Enviroment =
    {   Id: Guid
        Name: string
        PingStatus: Status
        TimeStamp: DateTime
        EndpointsQ: EndpointStatus list}

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


