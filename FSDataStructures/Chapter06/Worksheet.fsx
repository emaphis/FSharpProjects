// Chapter 6 - See the Forest for the Trees

// Tree as a data structure

type tree' = Node' of tree' list

let rec bal_tree = function
    | 0 -> Node' []
    | n ->
        Node' [bal_tree(n-1); bal_tree(n-1)]

let btree = bal_tree 2
//val btree: tree' =
//  Node' [Node' [Node' []; Node' []]; Node' [Node' []; Node' []]]

let rec CountLeafNodes = function
    | Node' [] -> 1
    | Node' list ->
        Seq.fold (fun s t -> s + CountLeafNodes t) 0 list

CountLeafNodes btree
//val it: int = 4


// Navigating the tree
// From Tree.fs

#load "Tree.fs"
open Tree
open Tree.Algorithms

let myTree =
    Tree("D", Tree("B", Leaf("A"), Leaf("C")), Leaf("E"))

let mySeq1 = inOrder myTree
printfn $"%A{mySeq1}"
// seq ["A"; "B"; "C"; "D"; ...]

let mySeq2 = preOrder myTree
mySeq2
printfn $"%A{mySeq2}"


// Abstract syntax trees

