// 10 - Object Programming

// Equality

type Coordinate(latitude: float, longitude: float) =
    member _.Latitude = latitude
    member _.Longitude = longitude

// test equality
let c1 = Coordinate(25.0, 11.98)
let c2 = Coordinate(25.0, 11.98)
let c3 = c1
c1 = c2 // false
c1 = c3 // true 
