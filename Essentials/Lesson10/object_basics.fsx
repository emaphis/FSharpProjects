// Lesson 10  - Object basics

// Class Type
// Hold or mutate state
// Internal representation different to external
// interact with an OO codebase

type MyClass() =
    member _.SayHello = printfn "Hello, world!"


let mc = MyClass()
mc.SayHello


// Something more complicated.


type GPSCoordinates1(latitude:decimal, longitude:decimal) =
    member val Latitude = latitude
    member val Longitude = longitude


let nullIsland1 = GPSCoordinates1(0m, 0m)

let lat1 = nullIsland1.Latitude
let lng1 = nullIsland1.Longitude



// Mutable Properties

type GPSCoordinates2(latitude:decimal, longitude:decimal) =
    member val Latitude = latitude
    member val Longitude = longitude

    member val Elevation = 0m with get, set


let nullIsland2 = GPSCoordinates2(0m, 0m)
nullIsland2.Elevation <- 500m

let lat2 = nullIsland2.Latitude
let lng2 = nullIsland2.Longitude
let elev2 = nullIsland2.Elevation



// Object initialization

type GPSCoordinates4(latitude:decimal, longitude:decimal, ?elevation:decimal) =
    member val Latitude = latitude
    member val Longitude = longitude

    member val Elevation = elevation with get, set


let nullIsland4 = GPSCoordinates4(0m, 0, 25m)

let lat5 = nullIsland4.Latitude
let lng5 = nullIsland4.Longitude
let elev5 = nullIsland4.Elevation




type GPSCoordinates3(latitude:decimal, longitude:decimal, ?elevation:decimal) =
    member val Latitude = latitude
    member val Longitude = longitude

    member val Elevation = elevation with get, set


let nullIsland3 = GPSCoordinates3(0m, 0, 25m)

let lat4 = nullIsland3.Latitude
let lng4 = nullIsland3.Longitude
let elev4 = nullIsland3.Elevation


// FizzBuzz

type FizzBuss() =

    let calculate n =
        [(3, "Fizz"); (5, "Buzz")]
        |> List.map (fun (v, s) ->
                if n% v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s <> "" then s else string n

    member _.Calculate(input) = calculate input


let run =
    let fzzBzz = FizzBuss()
    [1..20]
    |> List.map fzzBzz.Calculate
