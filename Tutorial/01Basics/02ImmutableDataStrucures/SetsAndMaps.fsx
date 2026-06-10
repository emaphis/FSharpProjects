// Immutable Data Structures
// Sequences


// Sets

// Adding an item to an empty set
// The Set module contains a useful function Set.empty which returns an empty set to start with

Set.empty.Add(1).Add(2).Add(3)
//val it: Set<int> = set [1; 2; 3]


// Converting lists and sequences into sets Additionally, the we can use Set.ofList and Set.ofSeq to convert an entire collection into a set:

Set.ofList ["Mercury"; "Venus"; "Earth"; "Mars"; "Jupiter"; "Saturn"; "Uranus"; "Neptune"]
//val it: Set<string> =
//  set
//    ["Earth"; "Jupiter"; "Mars"; "Mercury"; "Neptune"; "Saturn"; "Uranus";
//     "Venus"]


// The Set Module

// val add : 'a -> Set<'a> -> Set<'a>
// Return a new set with an element added to the set. No exception is raised if the set already contains the given element.

// val compare : Set<'a> -> Set<'a> -> int
// Compare two sets. Places sets into a total order.

// val count : Set<'a> -> int
// Return the number of elements in the set. Same as "size".

// val difference : Set<'a> -> Set<'a> -> Set<'a>
// Return a new set with the elements of the second set removed from the first. That is a set containing only those items from the first set that are not also in the second set.


let a = Set.ofSeq [ 1 .. 10 ]
let b = Set.ofSeq [ 5 .. 15 ]
let c = Set.ofSeq [ 2; 4; 5; 9 ]

Set.difference a b
//al it: Set<int> = set [1; 2; 3; 4]

a - b
// val it: Set<int> = set [1; 2; 3; 4]

// val exists : ('a -> bool) -> Set<'a> -> bool
// Test if any element of the collection satisfies the given predicate.

// val filter : ('a -> bool) -> Set<'a> -> Set<'a>
// Return a new collection containing only the elements of the collection for which the given predicate returns "true".

// val intersect : Set<'a> -> Set<'a> -> Set<'a>
// Compute the intersection, or overlap, of the two sets.

Set.iter (printf "%O ") (Set.intersect a b)
//5 6 7 8 9 10


// val map : ('a -> 'b) -> Set<'a> -> Set<'b>
// Return a new collection containing the results of applying the given function to each element of the input set.

// val contains: 'a -> Set<'a> -> bool
// Evaluates to true if the given element is in the given set.

// val remove : 'a -> Set<'a> -> Set<'a>
// Return a new set with the given element removed. No exception is raised if the set doesn't contain the given element.

// val count: Set<'a> -> int
// Return the number of elements in the set.

// val isSubset : Set<'a> -> Set<'a> -> bool
// Evaluates to "true" if all elements of the first set are in the second.

// val isProperSubset : Set<'a> -> Set<'a> -> bool
// Evaluates to "true" if all elements of the first set are in the second, and there is at least one element in the second set which is not in the first.

Set.isSubset c a
// val it: bool = true

Set.isSubset c b
// val it: bool = false

// val union : Set<'a> -> Set<'a> -> Set<'a>
// Compute the union of the two sets.

Set.iter (fun x -> printf $"{x} ") (Set.union a b)
//  2 3 4 5 6 7 8 9 10 11 12 13 14 15 v

Set.iter (fun x -> printf $"{x} ") (a + b)   // + calculates union
// 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15


// Examples

open System

let shakespeare = "O Romeo, Romeo! wherefore art thou Romeo?"
let shakespeareArray =
    shakespeare.Split([| ' '; ','; '!'; '?' |], StringSplitOptions.RemoveEmptyEntries)
let shakespeareSet = shakespeareArray |> Set.ofSeq

let main() =
    printfn $"shakespeare: %A{shakespeare}"

    let printCollection msg coll =
        printfn $"%s{msg}"
        Seq.iteri (fun index item -> printfn $" %i{index}: %O{item}") coll

    printCollection "shakespeareArray" shakespeareArray
    printCollection "shakespeareSet" shakespeareSet

    //Console.Read |> ignore

main ()


// Maps

// A map is a special kind of set: it associates keys with values.

let holidays =
    Map.empty. (* Start with empty Map *)
        Add("Christmas", "Dec. 25").
        Add("Halloween", "Oct. 31").
        Add("Darwin Day", "Feb. 12").
        Add("World Vegan Day", "Nov. 1")

// holidays: Map<string,string> =

let monkeys =
    [ "Squirrel Monkey", "Simia sciureus";
        "Marmoset", "Callithrix jacchus";
        "Macaque", "Macaca mulatta";
        "Gibbon", "Hylobates lar";
        "Gorilla", "Gorilla gorilla";
        "Humans", "Homo sapiens";
        "Chimpanzee", "Pan troglodytes" ]
    |> Map.ofList

// val monkeys: Map<string,string>


// You can use the .[key] to access elements in the map

holidays["Christmas"]
//val it: string = "Dec. 25"

monkeys["Marmoset"]
//val it: string = "Callithrix jacchus"


// The Map Module

// al add : 'key -> 'a -> Map<'key,'a> -> Map<'key,'a>
// Return a new map with the binding added to the given map.

// val empty<'key,'a> : Map<'key,'a>
// Returns an empty map.

// val exists : ('key -> 'a -> bool) -> Map<'key,'a> -> bool
// Return true if the given predicate returns true for one of the bindings in the map.

// val filter : ('key -> 'a -> bool) -> Map<'key,'a> -> Map<'key,'a>
// Build a new map containing only the bindings for which the given predicate returns true.

// val find : 'key -> Map<'key,'a> -> 'a
// Lookup an element in the map, raising KeyNotFoundException if no binding exists in the map.

// val containsKey: 'key -> Map<'key,'a> -> bool
// Test if an element is in the domain of the map.

// val remove : 'key -> Map<'key,'a> -> Map<'key,'a>
// Remove an element from the domain of the map. No exception is raised if the element is not present.

// val tryFind : 'key -> Map<'key,'a> -> 'a option
// Lookup an element in the map, returning a Some value if the element is in the domain of the map and None if not.


// Examples

open System

let capitals =
    [("Australia", "Canberra"); ("Canada", "Ottawa"); ("China", "Beijing");
        ("Denmark", "Copenhagen"); ("Egypt", "Cairo"); ("Finland", "Helsinki");
        ("France", "Paris"); ("Germany", "Berlin"); ("India", "New Delhi");
        ("Japan", "Tokyo"); ("Mexico", "Mexico City"); ("Russia", "Moscow");
        ("Slovenia", "Ljubljana"); ("Spain", "Madrid"); ("Sweden", "Stockholm");
        ("Taiwan", "Taipei"); ("USA", "Washington D.C.")]
    |> Map.ofList

let rec main2 () =
    Console.Write("Find a capital by country (Type a 'q' to quit.")
    match Console.ReadLine() with
    | "q"  -> Console.WriteLine("Bye bye")
    | country ->
        match capitals.TryFind(country) with
        | Some(capital) -> Console.WriteLine("The capital of {0} is {1}\n", country, capital)
        | None -> Console.WriteLine("Country not found.\n")
        main2 ()

main2 ()
