// 4.2 Declare function downto1: int -> int list such that the value of downto1 n is the list
//     [n; n-1; ...; 1]


let downto1 n =
    let rec loop x acc =
        match x with
        | 0 -> acc
        | x -> loop (x-1) ((n-x+1) :: acc)
    loop n []


// examples
[1]   = downto1 1
[2; 1]  = downto1 2
[5; 4; 3; 2; 1] = downto1 5
