module PrgrammingWithFunctions

module PartialApplication =

    // * Passing functions as parameters

    // maps the function `f` to all the elements of a list

    let lst = [1; 2; 3; 4; 5]
    let f x = x + 1

    let list2  =
        List.map (fun x -> f x) lst
    
    //val list2: int list = [2; 3; 4; 5; 6]

    // This can be simplified as

    let list3 =
        List.map f lst

    //val list3: int list = [2; 3; 4; 5; 6]


    // * eliminating Reduncant Code

    [<Measure>]
    type usd

    type Entree = { Name: string; Price: float<usd>; Calories: int }

    // Picks the cheapest item on the menue
    let pickCheapest menuItems =
        List.reduce
            (fun acc item ->
                        if item.Price < acc.Price
                        then item
                        else acc) menuItems

    let pickHealthiest menuItem =
        List.reduce
            (fun acc item ->
                        if item.Calories = acc.Calories
                        then item
                        else acc) menuItem

    
    // Factor out the comparison code
    let private pickItem cmp menuItems  =
        let reduceFunc acc item =
            match cmp acc item with
            | true  -> acc
            | false -> item
        
        List.reduce reduceFunc menuItems

    let pickCheapest2 = pickItem (fun acc item -> item.Price > acc.Price)
    let pickHealthiest2 = pickItem (fun acc item -> item.Calories > acc.Calories)

    
    let items =
        [ { Name = "cheese"; Price = 3.20<usd>; Calories = 200 }
          { Name = "bacon"; Price = 4.00<usd>; Calories = 250 }
          { Name = "letticc"; Price = 2.00<usd>; Calories = 50 } ]

    let cheap = pickCheapest2 items
    let heathy = pickHealthiest2 items


module Closures =
    
    // Closes over i   
    let mult i lst =
        List.map (fun x -> x * i) lst

    let lst1 = mult 10 [1; 3; 5; 7]
    //al lst1: int list = [10; 30; 50; 70]


    // Example 7-18. Data encapsulation via closures

    // Data type for a set. Notice the implementation is
    // stored in record fields..

    type Set =
        { // Add an item to the set
          Add : int -> Set
          // Checks if an item exists in  the set
          Exists : int -> bool }

        // Returns an empty set
        static member Empty =
            let rec makeSet lst =
                { Add    = (fun item -> makeSet (item :: lst))
                  Exists = (fun item -> List.exists ((=) item) lst) }
            makeSet []


    // Set in action
    let s    = Set.Empty
    let s'   = s.Add(1)
    let s''  = s'.Add(2)
    let s''' = s''.Add(3)

    let b1 = s.Exists 2     // false
    let b2 = s'''.Exists 2  // true
