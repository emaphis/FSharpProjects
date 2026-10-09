(* using the Choose function *)
let array16 = [| 1 .. 20 |]

let array17  = (Array.choose (fun elem ->
    if elem % 3 = 0 then
        Some(float elem)
    else
        None) array16)
