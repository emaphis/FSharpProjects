// Lesson 10 - Encapsulation


type IRecentlyUsedList<'T> =
    abstract Add : 'T -> unit
    abstract Items : 'T list


type RecentlyUsedList<'T>(capacity: int) =
    let items = ResizeArray<'T>(capacity)

    let add item =
        item |> items.Remove |> ignore
        if items.Count = items.Capacity then items.RemoveAt 0
        items.Add item

    interface IRecentlyUsedList<'T> with
        member _.Add(item) = add item
        member _.Items = items |> Seq.toList |> List.rev


let doStuff (mru: IRecentlyUsedList<string>) (item:string) =
    mru.Add item

let mostList = RecentlyUsedList<string>(5)

let iMru = mostList :> IRecentlyUsedList<string>

iMru.Add "Red"
iMru.Add "Blue"
iMru.Add "Green"

iMru.Items

doStuff mostList "Blue"

iMru.Items

let mRu = iMru :?> RecentlyUsedList<string>

