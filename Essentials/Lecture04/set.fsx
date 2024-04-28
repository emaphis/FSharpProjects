// Lesson 4  - Set

let set1 = Set [1..10]

let set2 = Set [7..15]

let data1 = [1..10] @ [5..13] |> Set.ofList


let set3 = Set.intersect set1 set2

let diff1 = Set.difference set1 set2

let diff2 = Set.difference set2 set1


let bool1 = Set.isProperSubset (Set [1..9]) set1

let bool2 = Set.isSubset (Set [1..10]) set1

let max1 = set1.MaximumElement

let set4 = Set.union set1 set2
