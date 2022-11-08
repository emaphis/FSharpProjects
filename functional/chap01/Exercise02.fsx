// 1.2 Declare a function h: float * float -> float, where h(x, y) = x2 + y2.
// Hint: Use the function System.Math.Sqrt

let h(x,y) = 
    System.Math.Sqrt(x * x + y * y)

// testing
printfn "%b"  (0.0 = h(0.0, 0.0))
printfn "%b"  (5.0 = h(3.0, 4.0))
