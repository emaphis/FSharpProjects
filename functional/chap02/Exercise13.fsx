// 2.13 The functions curry and uncurry of types
//
//         curry : (’a * ’b -> ’c) -> ’a -> ’b -> ’c
//         uncurry : (’a -> ’b -> ’c) -> ’a * ’b -> ’c
//
//     are defined in the following way:
//
//     curry f is the function g where g x is the function h where h y = f(x, y).
//     uncurry g is the function f where f(x, y) is the value h y for the function h = g x.
//
//     Write declarations of curry and uncurry.

let curry f =
    fun a b -> f (a, b)

let uncurry f =
    fun (a, b) -> (f a b)


// testing
let funA (a, b) = a + b
5 = funA(2, 3)

let funB = curry funA
5 = funB 2 3

let funC = uncurry funB
5 = funC (2, 3)
