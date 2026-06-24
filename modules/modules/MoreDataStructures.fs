// Object-Oriented Programming
// Modules and Namespaces

// Adding to Namespace from Multiple Files
// Controlling Class and Module Accessibility

namespace Princess.Collections

type 'a tree when 'a :> System.IComparable<'a> =
    | EmptyTree
    | TreeNode of 'a * 'a tree * 'a tree


// InvisibleModule is only accessible by classes or
// modules inside the Princess.Collections namespace
module private InvisibleModule =
    let msg = "I'm invisible!"


module Tree =
    // InvisibleClass is only accessible by methods
    // inside the Tree module
    type private InvisibleClass() =
        member x.Msg() = "I'm invisible too!"

    let rec insert (x: #System.IComparable<'a>) = function
        | EmptyTree -> TreeNode(x, EmptyTree, EmptyTree)
        | TreeNode(y, l, r) as Node ->
            match x.CompareTo y with
            | 0  -> Node
            | 1  -> TreeNode(y, l, insert x r)
            | -1 -> TreeNode(y, insert x l, r)
            | _  -> failwith "CompareTo returned illegal value"
