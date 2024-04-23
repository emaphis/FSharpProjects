// Lecture 01 - Records

type Coordinate = {
    Latitude : decimal
    Longitude : decimal
}


type Coordinate1 = {
    Latitude : decimal
    Longitude : decimal
}

// constructing

let nullIsland = {  // becomes the closest type
    Latitude = 0m
    Longitude = 0m
}

// deconstructing
let latitude = nullIsland.Latitude
let longitude = nullIsland.Longitude
