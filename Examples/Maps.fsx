(* F# - Maps *)

(* Creating Maps *)

(* Create an empty Map *)
let students =
   Map.empty. (* Creating an empty Map *)
      Add("Zara Ali", "1501").
      Add("Rishita Gupta", "1502").
      Add("Robin Sahoo", "1503").
      Add("Gillian Megan", "1504");;

printfn "Map - students: %A" students


(* Convert a list to Map *)
let capitals =
   [ "Argentina", "Buenos Aires";
      "France ", "Paris";
      "Chili", "Santiago";
      "Malaysia", " Kuala Lumpur";
      "Switzerland", "Bern" ]
   |> Map.ofList;;

printfn "Map capitals : %A" capitals


printfn "Map - students: %A" students
(*Accessing an element using key *)
printfn "%A" students.["Zara Ali"]


(* Basic Operations on Maps*)

(* Example *)

(* Create an empty Map *)
let students2 =
   Map.empty. (* Creating an empty Map *)
      Add("Zara Ali", "1501").
      Add("Rishita Gupta", "1502").
      Add("Robin Sahoo", "1503").
      Add("Gillian Megan", "1504").
      Add("Shraddha Dubey", "1505").
      Add("Novonil Sarker", "1506").
      Add("Joan Paul", "1507")

printfn "Map - students: %A" students2
printfn "Map - number of students: %d" students2.Count

(* finding the registration number of a student*)
let found = students.TryFind "Rishita Gupta"
match found with
| Some x -> printfn "Found %s." x
| None -> printfn "Did not find the specified value."
