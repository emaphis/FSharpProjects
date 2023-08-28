// 14 - Creating an API with Giraffe

module GiraffeExample.Todos

open System
open System.Collections.Generic
open Microsoft.AspNetCore.Http
open Giraffe
open Giraffe.EndpointRouting
open GiraffeExample.TodoStore

module Handler =
    
    let viewTodosHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            let store = ctx.GetService<TodoStore>()
            store.GetAll()
            |> ctx.WriteJsonAsync

    let viewTodoHandler (id:Guid) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let store = ctx.GetService<TodoStore>()
                return!
                    (match store.Get(id) with
                    | Some todo -> json todo
                    | None -> RequestErrors.NOT_FOUND "Not Found") next ctx
            }

    let createTodoHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! newTodo = ctx.BindJsonAsync<NewTodo>()
                let store = ctx.GetService<TodoStore>()
                let created = 
                    { 
                        Id = Guid.NewGuid()
                        Description = newTodo.Description
                        Created = DateTime.UtcNow
                        IsCompleted = false 
                    }
                    |> store.Create
                return! json created next ctx
            }
    
    let updateTodoHandler (id:Guid) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! todo = ctx.BindJsonAsync<Todo>()
                let store = ctx.GetService<TodoStore>()
                return!
                    (match store.Update(todo) with
                    | true -> json true
                    | false -> RequestErrors.GONE "Gone") next ctx
            }

    let deleteTodoHandler (id:Guid) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let store = ctx.GetService<TodoStore>()
                return!
                    (match store.Get(id) with
                    | Some existing -> 
                        let deleted = store.Delete(KeyValuePair<TodoId, Todo>(id, existing))
                        json deleted
                    | None -> RequestErrors.GONE "Gone") next ctx
            }
    

// Endpoint list
let apiTodoRoutes = 
    [
        GET [
            routef "/%O" Handler.viewTodoHandler
            route "" Handler.viewTodosHandler
        ]
        POST [ 
            route "" Handler.createTodoHandler
        ]
        PUT [
            routef "/%O" Handler.updateTodoHandler
        ]
        DELETE [
            routef "/%O" Handler.deleteTodoHandler
        ]
    ]
