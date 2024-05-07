// Lesson 9 - Improving your Domain Models
// Single-Case Discrimated-Untions

// Latitude -90 to +90, Longitude -180 to + 180

module Location =

    type GPSCoordinates = private  GPSCoordinates of Latitude:decimal * Longitude:decimal


    [<RequireQualifiedAccess>]
    module GPSCoordinates =
        
        let value input = input |>  fun (GPSCoordinates (lat, lng)) -> (lat, lng)

        let create input =
            let (lat, lng) = input
            if lat < -90m || lat > 90m then Error "latitude is out of range"
            elif lng < -180m || lng > 180m then Error "longitude is out of range"
            else GPSCoordinates (lat, lng) |> Ok


open Location


let nullIsland = GPSCoordinates.create (0m, 0m)

let location =
    match nullIsland with
    | Ok gps -> gps |> GPSCoordinates.value
    | Error err ->  (-999m, -999m)


printfn "Location of Null Island: %A" location
