// 1.8 Consider the declarations:
let a = 5
let f a = a + 1
let g b = (f b) + a

// Find the environment obtained from these declarations and
// write the evaluations of the expressions f 3 and g 3.

//       | a  -> 5
// env = | f -> fun a = a + 1
//       | g -> fun b = f b + 5

f 3
3 + 1
4

g 3
(f 3) + 5
3 + 1 + 5
9
