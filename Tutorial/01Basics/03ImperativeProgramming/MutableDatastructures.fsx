// Imperative Programming
// Mutable Collections

// List<'T> Class

open System.Collections.Generic

let myList = List<string>()

do myList.Add("hello")
do myList.Add("world")

let str1 = myList[0]

myList |> Seq.iteri (fun index _ -> printfn $"{index}: {myList[index]}")


// Underlying Implementation
// Wrapper over an array that starts with 4 elements.
// Capacity doubles when capacity is exhausted.

let items = List<string>()

let printList (l: List<_>) =
    printfn $"{l.Count}, l.Capacity: {l.Capacity}"
    printfn "items:"
    l |> Seq.iteri (fun index _ ->
        printfn $"{index}: {l[index]}")
    printfn "__________"


let main1() =
    printList items

    items.Add("monkey")
    printList items

    items.Add("kitty")
    items.Add("bunny")
    printList items

    items.Add("doggy")
    items.Add("octopussy")
    items.Add("ducky")
    printList items

    printfn "Removing entry for \"doggy\"\n--------\n"
    items.Remove("doggy") |> ignore
    printList items

    printfn "Removing item at index 3\n--------\n"
    items.RemoveAt(3)
    printList items


main1()


// If you know the maximum size of the list beforehand, it is possible to avoid
// the performance hit by calling the List<'T>(size : int) constructor instead
//  The following sample demonstrates how to add 1000 items to a list without
// resizing the internal array

let myList2 = List<int>(1000)

myList2.Count, myList2.Capacity
//val it: int * int = (0, 1000)

seq { 1 .. 1000 } |> Seq.iter (fun x -> myList2.Add(x))

myList2.Count, myList2.Capacity
//val it: int * int = (1000, 1000)


// LinkedList<'T> Class

// A LinkedList<'T> represented a doubly-linked sequence of nodes which allows
// efficient O(1) inserts and removal, supports forward and backward traversal,
// but its implementation prevents efficient random access.

//open System.Collections.Generic
let items2 = LinkedList<string>()

items2.AddLast("AddLast1")
items2.AddLast("AddList2")


let firstItem = items2.AddFirst("AddFirst1")
let addAfter = items2.AddAfter(firstItem, "addAfter")
let addBefore = items2.AddBefore(addAfter, "addBefore")

for item in items2 do
    printfn $"item = {item}"


// Stack<'T> Class

let stack = Stack<string>()

stack.Push("First")
stack.Push("Second")
stack.Push("Third")

stack.Pop()
stack.Pop()
stack.Pop()


// Queue<'T> Class

let queue = Queue<string>()

queue.Enqueue("First")
queue.Enqueue("Second")
queue.Enqueue("Third")

queue.Dequeue()
queue.Dequeue()
queue.Dequeue()


// HashSet<'T>, and Dictionary<'TKey, 'TValue> Classes

let nums_1to10 = HashSet<int>()
let nums_5to15 = HashSet<int>()

let main2() =
    let printCollection msg targetSet =
        printf $"%s{msg}: "
        targetSet |> Seq.sort |> Seq.iter (fun x -> printf $"%O{x} ")
        printfn ""

    let addNums min max (targetSet: ICollection<_>) =
        seq { min .. max } |> Seq.iter(fun x -> targetSet.Add(x))

    addNums 1 10 nums_1to10
    addNums 5 15 nums_5to15

    printCollection "nums_1to10 (before)" nums_1to10
    printCollection "nums_5to15 (before)" nums_5to15

    nums_1to10.IntersectWith(nums_5to15)  // mutates nums_1to10
    printCollection "nums_1to10 (after)" nums_1to10
    printCollection "nums_5to15 (after)" nums_5to15


main2()


// Using the Dictionary<'TKey, 'TValue>

let dict = Dictionary<string, string>()

dict.Add("Garfield", "Jim Davis")
dict.Add("Farside", "Gary Larson")
dict.Add("Calvin and Hobbes", "Bill Watterson")
dict.Add("Peanuts", "Charles Schultz")

dict["Farside"]
//val it: string = "Gary Larson"
