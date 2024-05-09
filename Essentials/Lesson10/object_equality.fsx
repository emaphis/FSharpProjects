// Lesson 10 - Object Equality

open System

[<AllowNullLiteral>]
type GPSCoordinates(latitude:decimal, longitude:decimal) =

    let equals (other: GPSCoordinates) =
        if isNull other then
            false
        else
            latitude = other.Latitude
            && longitude = other.Longitude

    member _.Latitude = latitude
    member _.Longitude = longitude

    override this.GetHashCode() =
        hash (this.Latitude, this.Longitude)

    override _.Equals(obj) =
        match obj with
        | :? GPSCoordinates as other -> equals other
        | _ -> false

    interface IEquatable<GPSCoordinates> with
        member _.Equals(other: GPSCoordinates) =
            equals other


    static member op_Equality(this: GPSCoordinates, other: GPSCoordinates) =
        this.Equals(other)


let c1 = GPSCoordinates(25.0m, 11.98m)
let c2 = GPSCoordinates(25.0m, 11.98m)

let bool = c1 = c2

