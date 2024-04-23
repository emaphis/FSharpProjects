// Lecture 01 - Tuples

type MyTuple = bool * int

let myTuple: MyTuple = (true, 23)

let newTuple = ("Hello", true, 34)

// construction
let coordinate = (0m, 0m)

// destructing
let (latitue, logitue) = coordinate

open System

let (success, dt) = DateTime.TryParse("2020-02-07")
