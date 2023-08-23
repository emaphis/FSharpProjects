// Try the map function

open System

let map f result = 
    match result with
    | Ok x -> Ok (f x)
    | Error ex -> Error ex
// map: f: ('a -> 'b) -> result: Result<'a,'c> -> Result<'b,'c>

let tryParseDateTime (input: string) =
    let success, value = DateTime.TryParse input
    if success then Some value else None
// val tryParseDateTime: input: string -> DateTime option

let getResult =
    try
        Ok "Hello"
    with
    | ex -> Error ex
// val getResult: Result<string,exn> = Ok "Hello"

let parseDT = getResult |> map tryParseDateTime
//val parseDT: Result<DateTime option,exn> = Ok None
