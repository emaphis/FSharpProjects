// Chapter 4 Lists


// 4.1 The concept of a list

[2]
[3; 2]
[2; 3; 4]

// List constants in F#
let xs = [2;3;4]

let ys = ["Big"; "Mac"]

[("b",2);("c",3);("e",5)]  // (string * int) list

// list of records
type P = { name: string; age: int }

let recList = [{name = "Brown"; age = 25}; {name = "Cook"; age = 45}]
// val recList: P list

let lof = [sin; cos]  // (float -> float) list

let lOfl = [[2;3];[3];[2;3;3]]  // int list list

// The type constructor list

// not legal
//["a"; 1]
//error FS0001: All elements of a list must be implicitly convertible to the type of the first element, which he

// Equality of lists

[2;3;2] = [2;3]
[2;3;2] = [2;3;3]
[2;3;2] = [2;3;2]

// Ordering of lists

[1; 2; 3] < [1; 2; 3; 4]
['1'; '2'; '3'] < ['1'; '2'; '3'; '4']
[] < [1; 2; 3]

[] < [[]; [(true,2)]]


