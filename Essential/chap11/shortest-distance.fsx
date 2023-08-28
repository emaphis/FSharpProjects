// 11 - Recursion

// Recursion with Hierarchical Data

open System.IO

/// A simple generic hierarchical discriminated union
type Tree<'T> =
    | Branch of 'T * Tree<'T> seq
    | Leaf of 'T

//// A record type that defines a waypoint on our route. It will include data on
/// where we are, how we got there, and how far we have travelled to get here.
type Waypoint = { Location:string; Route:string list; TotalDistance:int }

/// A record type fo hold data that we load from the CSV file
type Connection = { Start:string; Finish:string; Distance:int }


/// A function that takes a file path and returns the loaded data
let loadData path =
    path
    |> File.ReadLines
    |> Seq.skip 1
    |> fun rows -> [
        for row in rows do
            match row.Split(",") with
            | [|start;finish;distance|] -> 
                { Start = start; Finish = finish; Distance = int distance }
                { Start = finish; Finish = start; Distance = int distance }
            | _ -> failwith "Row is badly formed"
    ]
    |> List.groupBy (fun cn -> cn.Start)
    |> Map.ofList


// Find Possible Routes

/// A function that determines where we can go to next that we haven't already been to:
let getUnvisited connections current =
    connections
    |> List.filter (fun cn -> current.Route |> List.exists (fun loc -> loc = cn.Finish) |> not)
    |> List.map (fun cn -> { 
        Location = cn.Finish
        Route = cn.Start :: current.Route
        TotalDistance = cn.Distance + current.TotalDistance })


/// Convert our tree structure to a list of Waypoints, so that we can
/// find the shortest route:
let rec treeToList tree =
    match tree with 
    | Leaf x -> [x]
    | Branch (_, xs) -> List.collect treeToList (xs |> Seq.toList)


/// Generate our Tree structure that defines all of the possible routes between
/// the start and the finish:
let findPossibleRoutes start finish (routeMap:Map<string, Connection list>) =
    let rec loop current =
        let nextRoutes = getUnvisited routeMap[current.Location] current
        if nextRoutes |> List.isEmpty |> not && current.Location <> finish then
            Branch (current, seq { for next in nextRoutes do loop next })
        else 
            Leaf current
    loop { Location = start; Route = []; TotalDistance = 0 }
    |> treeToList
    |> List.filter (fun wp -> wp.Location = finish)


// Select Shortest Route

/// Determine the shortest route from the output:
let selectShortestRoute routes =
    routes 
    |> List.minBy (fun wp -> wp.TotalDistance)
    |> fun wp -> wp.Location :: wp.Route |> List.rev, wp.TotalDistance


/// Run the program - Call loadData with a start and finish location, 
let run start finish = 
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "data.csv") 
    |> loadData
    |> findPossibleRoutes start finish 
    |> selectShortestRoute
    |> printfn "%A"

let result = run "Cogburg" "Leverstorm"

//(["Cogburg"; "Copperhold"; "Leverstorm"], 1616)
