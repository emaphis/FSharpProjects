(* F# - Mutable Data *)

(* Example *)

let x = 10
let y = 20
let z = x + y

printfn $"x: %i{x}"
printfn $"y: %i{y}"
printfn $"z: %i{z}"

// Won't compile, immutable
//let x = 15
//let y = 20
//let z = x + y


(* Mutable Variables *)

let mutable xm = 10
let yi = 20
let mutable zm = xm + yi

printfn $"xm: %i{xm}"
printfn $"yi: %i{yi}"
printfn $"zm: %i{zm}"

printfn "Let us change the value of x"
printfn "Value of z will change too."

xm <- 15
zm <- xm + yi

printfn "New Values:"
printfn $"xm: %i{xm}"
printfn $"yi: %i{yi}"
printfn $"zm: %i{zm}"


(* Uses of Mutable Data *)

open System

type studentData =
    {  ID : int
       mutable IsRegistered : bool
       mutable RegisteredText : string  }

let getStudent id =
  { ID = id;
    IsRegistered = false;
    RegisteredText = null; }

let registerStudents (students : studentData list) =
   students |> List.iter(fun st ->
      st.IsRegistered <- true
      st.RegisteredText <- sprintf "Registered %s" (DateTime.Now.ToString "hh:mm:ss")

      (* Putting thread to sleep for 1 second to simulate processing overhead. *)
      Threading.Thread.Sleep 1000)

let printData (students: studentData list) =
   students |> List.iter (fun x -> printfn "%A" x)

let main() =
   let students = List.init 3 getStudent

   printfn "Before Process:"
   registerStudents students
   printfn "After Process:"
   printData students
   printfn "End Program:"

   //Console.ReadKey true |> ignore

main()
