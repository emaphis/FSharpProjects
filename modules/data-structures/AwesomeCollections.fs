namespace AwesomeCollections


type 'a stack =
    | EmptyStack
    | StackNode of 'a * 'a stack


module Stack =

    let hd = function
        | EmptyStack -> failwith "Empty stack"
        | StackNode(hd, _) -> hd

    let tl = function
        | EmptyStack -> failwith "Empty stack"
        | StackNode(_, tl) -> tl

    let cons hd tl = StackNode(hd, tl)

    let empty = EmptyStack

    let rec update index value stk =
        match index, stk with
        | index, EmptyStack -> failwith "Index out of range"
        | 0, StackNode(hd, tl) -> StackNode(value, tl)
        | n, StackNode(hd, tl) -> StackNode(hd, update (index - 1) value tl)

    let rec map fn = function
        | EmptyStack -> EmptyStack
        | StackNode(hd, tl) -> StackNode(fn hd, map fn tl)

    let rev stk =
        let rec loop acc = function
            | EmptyStack -> acc
            | StackNode(hd, tl) -> loop (StackNode(hd, acc)) tl
        loop EmptyStack stk

    let rec contains x = function
        | EmptyStack -> false
        | StackNode(hd, tl) -> hd = x || contains x tl

    let rec fold fn seed = function
        | EmptyStack -> seed
        | StackNode(hd, tl) -> fold fn (fn seed hd) tl


type Queue<'a>(item: stack<'a>) =
    member this.hd
        with get() = Stack.hd (Stack.rev item)

    member this.tl
         with get() = Queue(item |> Stack.rev |> Stack.tl |> Stack.rev)

    member this.enqueue(x) = Queue(StackNode(x, item))

    override this.ToString() = sprintf $"%A{item}"

    static member empty = Queue<'a>(Stack.empty)
