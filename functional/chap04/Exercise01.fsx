// 4.1 Declare function upto: int -> int list such that upto n = [1; 2; ... ; n].

let rec upto = function
    | 0    -> []
    | n    -> upto (n-1) @ [n]

// tests
[]   = upto 0
[1]  = upto 1
[1;2;3;4;5;6] = upto 6
