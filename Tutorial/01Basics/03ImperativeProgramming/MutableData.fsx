// Imperative Programming
// Mutable Data 


// mutable Keyword

let v = 5
let mutable x = 5

v
x

x <- 10
x


// The mutable keyword is frequently used with record types to create mutable records:

open System

type transactionItem =
    { ID : int;
      mutable IsProcessed : bool;
      mutable ProcessedText : string; }

let getItem id =
    { ID = id;
      IsProcessed = false;
      ProcessedText = null; }

let processItems (items: transactionItem list) =
    items |> List.iter (fun item ->
        item.IsProcessed <- true
        let processedTime = DateTime.Now.ToString("hh:mm:ss")
        item.ProcessedText <- sprintf $"Processed %s{processedTime}"

        Threading.Thread.Sleep(1000)
        )

let printItems (items: transactionItem list) =
    items |> List.iter (fun item -> printfn $"%A{item}")


let main1() =
    let items = List.init 5 getItem
    
    printfn "Before process:"
    printItems items
    
    printfn "After process:"
    processItems items
    printItems items

main1()


// Limitations of Mutable Variables

let testMutable() =
    let mutable msg = "hello"
    printfn $"{msg}"

    let setMsg() =
        msg <- "world"

    setMsg()
    printfn $"{msg}"

testMutable()


// Ref cells

(*
Ref cells are defined by F# as follows:

type 'a ref = { mutable contents : 'a }

The F# library contains several built-in functions and operators for working with ref cells:

let ref v = { contents = v }      (* val ref  : 'a -> 'a ref *)
let (!) r = r.contents            (* val (!)  : 'a ref -> 'a *)
let (:=) r v = r.contents <- v    (* val (:=) : 'a ref -> 'a -> unit *)
*)

let xr = ref "hello"

xr
//val x: string ref = { contents = "hello" }

let str1 = !xr  // returns x.contents
//val str1: string = "hello"
// info FS3370: The use of '!' from the F# library is deprecated. 

let str2 = xr.Value // use instead
//val str2: string = "hello"


xr := "world"
// info FS3370: The use of ':=' from the F# library is deprecated.

xr.Value <- "world" // use this instead


// Since ref cells are allocated on the heap, they can be shared across multiple functions:


let withSideEffects (x: string ref) =
    x.Value <- "assigned from withSideEffects function"


let refTest() =
    let msg = ref "hello"
    printfn $"{msg.Value}"

    let setMsg() =
        msg.Value <- "world"

    setMsg()
    printfn $"{msg.Value}"

    withSideEffects msg
    printfn $"{msg.Value}"

let main2() =
    refTest()


main2()


// Aliasing Ref Cells

let cell1 = ref 7

let cell2 = cell1

let cell3 = cell2

cell1.Value
cell2.Value
cell3.Value

cell1.Value <- 10

cell1.Value
cell2.Value
cell3.Value


// Encapsulating Mutable State'

let incr =
    let counter = ref 0
    fun () ->
        counter.Value <- counter.Value + 1
        counter.Value
// val incr: (unit -> int)

incr()
incr()
incr()
// val it: int = 3
