// 10 - Object Programming

// Encapsulation - encapsulate a mutable member

type IRecentlyUsedList =
    abstract member IsEmpty : bool
    abstract member Size : int
    abstract member Capacity : int
    abstract member Clear : unit -> unit
    abstract member Add : string -> unit
    abstract member TryGet : int -> string option

type RecentlyUsedList(capacity: int) =
    let items = ResizeArray<string>(capacity)

    let add item =
        items.Remove item |> ignore
        if items.Count = items.Capacity then items.RemoveAt 0
        items.Add item

    let get index =
        if index >= 0 && index < items.Count
        then Some items[items.Count - index - 1]
        else None

    interface IRecentlyUsedList with
        member _.IsEmpty = items.Count = 0
        member _.Size = items.Count
        member _.Capacity = items.Capacity
        member _.Clear() = items.Clear()
        member _.Add(item) = add item
        member _.TryGet(index) = get index


// Let's test our recently used list with a capacity of five:

let mrul = RecentlyUsedList(5) :> IRecentlyUsedList

mrul.Capacity = 5

mrul.Add "Test"
mrul.Size = 1
mrul.Capacity = 5
mrul.IsEmpty = false

mrul.Add "Test2"
mrul.Add "Test3"
mrul.Add "Test4"
mrul.Add "Test"
mrul.Add "Test6"
mrul.Add "Test7"
mrul.Add "Test"

mrul.Size = 5
mrul.Capacity = 5
mrul.TryGet(0) = Some "Test"
mrul.TryGet(4) = Some "Test3"
