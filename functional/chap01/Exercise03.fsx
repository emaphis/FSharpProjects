// Write function expressions corresponding to the functions g and h in the exercises 1.1 and 1.2

fun n -> n + 4

fun (x, y) -> System.Math.Sqrt(x * x + y * y)

// Testing

printf "%b\n" (4 = ((fun n -> n + 4) 0))
printf "%b\n" (8 = ((fun n -> n + 4) 4))

printf "%b\n" (0.0 = (fun (x, y) -> System.Math.Sqrt(x * x + y * y) )(0.0, 0.0))
printf "%b\n" (5.0 = (fun (x, y) -> System.Math.Sqrt(x * x + y * y) )(3.0, 4.0))
 