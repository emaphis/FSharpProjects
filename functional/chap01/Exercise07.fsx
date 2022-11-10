// 1.7 Determine a type for each of the expressions:
//      (System.Math.PI, fact -1)
//      fact(fact 4)
//      power(System.Math.PI, fact 2)
//      (power, fact)

let rec fact = function
    | 0 -> 1
    | n -> n * fact(n-1);;  

let rec power = function
    | (x,0) -> 1.0
    | (x,n) -> x * power(x,n-1)



let a = (System.Math.PI, fact -1)  // (float * int)  // error
let b = fact(fact 4)  // int
let c = power(System.Math.PI, fact 2)  // float
let d = (power, fact)  // float * int -> float) * (int -> int) 
