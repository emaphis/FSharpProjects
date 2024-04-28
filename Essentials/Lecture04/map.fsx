// Lesson 4 - Map


let rnd = System.Random()

let data1 =
    List.init 5 (fun i -> (i, rnd.Next(100)))
    |> Map

let num1 = data1.Item(0)
let num2 = data1[1]

let num3 = Map.tryFind 3 data1

let keys1 = data1.Keys


open System.Collections.Generic

let data2 =
    List.init 5 (fun i -> (i, rnd.Next(100)))
    |> dict


let data3 =
    List.init 5 (fun i -> (i, rnd.Next(100)))
    |> readOnlyDict
