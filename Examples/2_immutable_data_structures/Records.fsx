(* F# - Records *)

(* Defining a Record *)

//type website =
//  { Title : string
//    Url : string }


(* Creating a Record *)

//let homepage = { Title = "TutorialsPoint"; Url = "www.tutorialspoint.com" }


(* Example 1 *)

(* defining a record type named website *)
type website =
  { Title : string
    Url : string }


(* creating some records *)
let homepage = { Title = "TutorialsPoint"; Url = "www.tutorialspoint.com" }
let cpage = { Title = "Learn C"; Url = "www.tutorialspoint.com/cprogramming/index.htm" }
let fsharppage = { Title = "Learn F#"; Url = "www.tutorialspoint.com/fsharp/index.htm" }
let csharppage = { Title = "Learn C#"; Url = "www.tutorialspoint.com/csharp/index.htm" }

(*printing records *)
printfn $"Home Page: Title: %A{homepage.Title} \n \t URL: %A{homepage.Url}"
printfn $"C Page: Title: %A{cpage.Title} \n \t URL: %A{cpage.Url}"
printfn $"F# Page: Title: %A{fsharppage.Title} \n \t URL: %A{fsharppage.Url}"
printfn $"C# Page: Title: %A{csharppage.Title} \n \t URL: %A{csharppage.Url}"


(* Example *)

type student =
  { Name : string
    ID: int
    RegistrationText : string
    IsRegistered : bool }

let getStudent name id =
  { Name = name; ID = id; RegistrationText = null; IsRegistered = false }

let registerStudent st =
  { st with
     RegistrationText = "Registered"
     IsRegistered = true }

let printStudent msg st =
    printfn $"%s{msg}: %A{st}"

let main() =
   let preRegisteredStudent = getStudent "Zara" 10
   let postRegisteredStudent = registerStudent preRegisteredStudent

   printStudent "Before Registration: " preRegisteredStudent
   printStudent "After Registration: " postRegisteredStudent


main()

(*
Before Registration: : { Name = "Zara"
  ID = 10
  RegistrationText = null
  IsRegistered = false }
After Registration: : { Name = "Zara"
  ID = 10
  RegistrationText = "Registered"
  IsRegistered = true }
*)
