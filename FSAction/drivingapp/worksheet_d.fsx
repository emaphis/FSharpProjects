// 6.2.4 Referencing programs from scripts

// Worksheet demostratig call in modules from a script. 

// loading the file directly

//#load "Car.fs"
//let result = Car.drive 10 8
//printfn $"Result is {result}"

// loading the copiled assempbly

#r "bin/Debug/net9.0/drivingapp.dll"
let result = Car.drive 10 8
printfn $"Result is {result}"
// Result is 7
