namespace ThothJsonWebApi

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Giraffe.Serialization
open Thoth.Json.Giraffe
open Giraffe

type LikeCSharpPoco() =
    let mutable x = 0

    member this.X 
        with get() = x
        and set v = x <- v

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =

        services.AddGiraffe() |> ignore
        // thoth as default json serializer
        services.AddSingleton<IJsonSerializer>(ThothSerializer()) |> ignore


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseGiraffe (json(LikeCSharpPoco(X=5)))

    member val Configuration : IConfiguration = null with get, set