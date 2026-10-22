module ConcurrentStack

// Stack with concurrency support

type ConcurrentStack<'a>() =
    let mutable _stack: List<'a> = []

    member this.Push value =
        lock _stack (fun () ->
            match _stack with
            | result :: remainder ->
                _stack <- remainder
                result
            | [] -> failwith "Empty stack"
         )
 