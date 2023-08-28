// 13 - Introduction to Web Programming with Giraffe


open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Giraffe.EndpointRouting
open Giraffe.ViewEngine
open GiraffeExample.TodoStore
open GiraffeExample


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

// Endpong list 
let endpoints =
    [
       GET [
           route "/" (htmlView indexView)
       ]
       subRoute "/api/todo" Todos.apiTodoRoutes
       //subRoute "/api" apiRoutes
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
        .AddSingleton<TodoStore>(TodoStore())
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
