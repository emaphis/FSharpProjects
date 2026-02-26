module MasteringLists

module ListOperations =

    // * Cons *

    let x = [2; 3; 4]
    // val x: int list = [2; 3; 4]

    let x2 = 1 :: x
    // val x2: int list = [1; 2; 3; 4]

    x
    // val x: int list = [2; 3; 4]

    // * Append *

    let y = [5; 6; 7]
    // val y: int list = [5; 6; 7]

    let x3 = x @ y
    //  val x3: int list = [2; 3; 4; 5; 6; 7]


module UsingLists =

    // Slow implementation ...
    let removeConsecutiveDupes1 lst =

        let foldFunc acc item =
            let lastNumber, dupesRemoved =  acc
            match lastNumber with
            | Some(c) when c = item ->
                Some(c), dupesRemoved
            | Some _ -> Some(item), dupesRemoved @ [item]
            | None  -> Some(item), [item]

        let _, dupeRemoved = List.fold foldFunc (None, []) lst
        dupeRemoved


    // Fast implementation ...
    let removeConsecutiveDupes2 lst =
        let f item acc =
            match acc with
            | [] -> [item]
            | hd :: _ when hd <> item ->
                item  :: acc
            | _  -> acc

        List.foldBack f lst []


    let example =  [1; 1; 2; 2; 3; 4]

    let lst1 = removeConsecutiveDupes1 example
    //val lst1: int list = [1; 2; 3; 4]

    let lst2 = removeConsecutiveDupes2 example
    //val lst2: int list = [1; 2; 3; 4]
