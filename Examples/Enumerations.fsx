(* F# - Enumerations *)

(* Declaring Enumerations

type enum-name =
   | value1 = integer-literal1
   | value2 = integer-literal2
...

*)

(* Example *)

type Days =
    | Sun = 0
    | Mon = 1
    | Tues = 2
    | Wed = 3
    | Thurs = 4
    | Fri = 5
    | Sat = 6

// Use of an enumeration
let weekend1 : Days = Days.Sat
let weekend2 : Days = Days.Sun
let weekDay1 : Days = Days.Mon

printfn $"Monday: %A{weekDay1}"
printfn $"Saturday: %A{weekend1}"
printfn $"Sunday: %A{weekend2}"
