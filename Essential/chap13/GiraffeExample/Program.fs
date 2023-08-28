// 13 - Introduction to Web Programming with Giraffe

open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Http
open Giraffe
open Giraffe.EndpointRouting
open Giraffe.ViewEngine


// Creating a View
let indexView =
    html [] [
        head [] [
            title [] [ str "Giraffe Example" ]
        ]
        body [] [
            h1 [] [ str "I |> F#"]
            p [ _class "somm-css-class"; _id "someId"] [
                str "Hello World from the Giraffe View Enging"
            ]
        ]
    ]

 // element of html has the following structure
 // element [attibutes] [sub-elements/data]
 // html [] []



// Creating a Custom HttpHandler

// using a task
let sayHelloNameHandler' (name:string) : HttpHandler =
    fun (next:HttpFunc) (ctx:HttpContext) ->
        task {
            let msg = $"hello {name}, how are you?"
            return! json {| Response = msg |} next ctx
        }

// using built-in HttpContext extensions
let sayHelloNameHandler (name:string) : HttpHandler =
    fun (next:HttpFunc) (ctx:HttpContext) ->
        {| Response = $"Hello {name}, how are you?" |}
        |> ctx.WriteJsonAsync


// Endpoint list
let apiRoutes = 
    [
        GET [
            route "" (json {| Response = "Hello world!!" |})
            routef "/%s" sayHelloNameHandler
        ]
    ]

// Endpong list 
let endpoints =
    [
        GET [
            route "/" (htmlView indexView)
        ]
        subRoute "/api" apiRoutes
    ]


let notFoundHandler =
    "Not Found"
    |> text
    |> RequestErrors.notFound


let configureApp (appBulder: IApplicationBuilder) =
    appBulder
        .UseRouting()
        .UseGiraffe(endpoints)
        .UseGiraffe(notFoundHandler)


let configureServices(services : IServiceCollection) =
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
    0 // Exit code
