// 2.1.4 Working with a smarter compiler


// Listing 2.1  1 A domain model of a train syste

open System

/// A train carriage can have a number of different features.
type Feature = Quite | Wifi | Tolet

/// A carriage can be either first or second class.
type CarriageClass = First | Second

/// Carriages can be either for passengers or the buffet cart
type CarriageKind =
    | Passenger of CarriageClass
    | Buffet of {| ColdFood : bool; HotFood : bool |}


/// A carriage has a unique number on the train
type CarriageNumber = CarriageNumber of int

/// A carriage has a number, kind, a list of features and a finite number of seats.
type Carriage =
    {
        Number : CarriageNumber
        Kind : CarriageKind
        Features : Feature Set
        NumberOfSeats : int
    }


type TrainId = TrainId of string
type Station = Station of string

/// Each stop is a station and a time of arrival
type Stop = Station * DateTime

/// A train has a unique id, and a list of carriages. It always has an origin and destination,
/// and may a list of stops in between. It *might* also have a station where the driver changes.
type Train =
    {
        Id : TrainId
        Carriages : Carriage list
        Origin : Stop
        Stops : Stop list
        Destination : Stop
        DriverChange : Station option
    }
