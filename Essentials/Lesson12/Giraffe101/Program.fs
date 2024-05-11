open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Http
open Giraffe
open Giraffe.EndpointRouting
open Giraffe.ViewEngine

(* Web App Configurations *)

// Creating a view
let indexView =
    html [] [
        head [] [
            title [] [ str "Giraffe Example"]
        ]
        body [] [
            h1 [] [ str "I |> F#" ]
            p [ _class "some-css-class"; _id "someId" ] [
                str "Hello Wolrd from the Geraffe View Engine"
            ]
        ]
    
    ]

let sayHelloNameHandler (name:string) : HttpHandler =
    fun (next:HttpFunc) (ctx:HttpContext) ->
        {| Response = $"Hello {name}, how are you?" |}
        |> ctx.WriteJsonAsync


let apiRoutes = 
    [
        route "" (json {| Response = "Hello word!"  |})    
        routef "/%s" sayHelloNameHandler
    ]

let endPoints =
    [
        GET [
            route "/" (htmlView indexView)
        ]
        subRoute "/api" apiRoutes
    ]


let notFoundHander =
    "Not Found"
    |> text
    |> RequestErrors.notFound


let configureApp (app : IApplicationBuilder) =
    app
        .UseRouting()
        .UseGiraffe(endPoints)
        .UseGiraffe(notFoundHander)


let configureServices (services : IServiceCollection) =
    services
        .AddRouting()
        .AddGiraffe()
    |> ignore

        
[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    configureServices builder.Services

    let app = builder.Build()

    if app.Environment.IsDevelopment() then
        app.UseDeveloperExceptionPage() |> ignore
      
    configureApp app
    app.Run()
    0
