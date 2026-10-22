module Stack.Stack

type Stack<'a>() =
    let mutable _stack: List<'a> = []

    member this.Push value =
        _stack <- value :: _stack

    member this.Pop =
        match _stack with
        | result :: remainder ->
            _stack <- remainder
            result
        | [] -> failwith "Stack is Empty"

    member this.TryPop =
        match _stack with
        | result :: remainder ->
            _stack <- remainder
            result |> Some
        | [] -> None

    override this.ToString() = _stack.ToString()
