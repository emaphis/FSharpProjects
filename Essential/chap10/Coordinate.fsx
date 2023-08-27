// 10 - Object Programming

// Equality

// Most of the types in F# support structural equality but class types do not.
// Instead, they rely on reference equality like most things in .NET. 


/// A simple class type to store GPS coordinates
type Coordinate(latitude: float, longitude: float) =
    member _.Latitude = latitude
    member _.Longitude = longitude

// test equality
let c1 = Coordinate(25.0, 11.98)
let c2 = Coordinate(25.0, 11.98)
let c3 = c1
c1 = c2 // false
c1 = c3 // true 

// To support something that works like structural equality, we need to override
// the GetHashCode and Equals functions, implement IEquatable<'T>
//
// If we are going to use it in other .NET languages, we need to handle the equality
// operator using op_Equality and apply the AllowNullLiteral attribute:

open System


[<AllowNullLiteral>]
type GpsCoordinate(latitude: float, longitude: float) =
    let equals (other: GpsCoordinate) =
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
        | :? GpsCoordinate as other -> equals other
        | _ -> false

    interface IEquatable<GpsCoordinate> with
        member _.Equals(other: GpsCoordinate) =
            equals other

    static member op_Equality(this: GpsCoordinate, other: GpsCoordinate) =
        this.Equals(other)

let c4 = GpsCoordinate(25.0, 11.98)
let c5 = GpsCoordinate(25.0, 11.98)
c4 = c5 // true
