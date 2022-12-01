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

// 1. The list xs is a proper prefix of ys:

[1; 2; 3] < [1; 2; 3; 4]
['1'; '2'; '3'] < ['1'; '2'; '3'; '4']
[] < [1; 2; 3]

[] < [[]; [(true,2)]]
[1; 2; 3; 0; 9; 10] < [1; 2; 3; 4]

// 2. The lists agree on the first k elements and xk+1 < yk+1.

[1; 2; 3; 0; 9; 10] < [1; 2; 3; 4]
["research"; "articles"] < ["research"; "books"]

// The other comparison relations can be defined in terms of = and < 
[1; 1; 6; 10] >= [1; 2]  // false

// compare function

compare [1; 1; 6; 10] [1; 2] // -1
compare [1;2] [1; 1; 6; 10]  // 1


// 4.2 Construction and decomposition of lists

// The cons operator

let x = 2::[3;4;5] 
let y = ""::[]

// The operator associates to the right, so x0::x1::xs means x0::(x1::xs)
let z = 2::3::[4;5]
// val z: int list = [2; 3; 4; 5]

// List patterns

let n::ns = [1;2;3]
n
ns

let [x0;x1;x2] = [(1,true); (2,false); (3, false)]
x0
x1
x2

let (p1, p2)::ps = [(1,[1]); (2, [2]); (3, [3]); (4,[4])]
p1
p2
ps

// Simple list expressions

[ 1..10 ]
[1..2..10]
[2.4 .. 3.0 ** 1.7]
[6 .. -1 .. 2]
[0.0 .. System.Math.PI/2.0 .. 2.0*System.Math.PI]


// 4.3 Typical recursions over lists

// Function declarations with two clauses

let rec sum1 = function
    | []    -> 0
    | x::xs -> x + sum1 xs

0 = sum1 []
6 = sum1 [1;2;3]

// Function declarations with several clauses

let rec altsum = function
    | []         -> 0
    | [x]        -> x
    | x0::x1::xs -> x0 - x1 + altsum xs

altsum [2; -1; 3]

// Layered patterns

let rec succPairs = function
    | x0::(x1::_ as xs) -> (x0,x1) :: succPairs xs
    | _          -> [] 

succPairs [1;2;3]
succPairs [1;2;3;4]

// Pattern matching on result of recursive call

let rec sumProd = function
    | []      -> (0,1)
    | x::rest ->
        let (rSum,rProd) = sumProd rest
        (x+rSum, x*rProd)

sumProd [2;5]
sumProd [1;2;3]

let rec unzip = function
    | []          -> ([],[])
    | (x,y)::rest ->
        let (xs,ys) = unzip rest
        (x::xs,y::ys)


unzip [(1,"a");(2,"b");(3,"c")]

// Pattern matching on pairs of lists

let rec mix = function
    | (x::xs,y::ys) -> x::y::(mix (xs,ys))
    | ([],[]) -> []
    | _ -> failwith "mix: parameter error"

mix ([1;2;3],[4;5;6])


// 4.4 Polymorphism

// List membership

/// List membership
let rec isMember x = function
    | y::ys -> x=y || (isMember x ys)
    | []    -> false

// x: 'a -> _arg1: 'a list -> bool when 'a: equality

true = isMember 1 [0;1;2;3]
false = isMember 1 [0;2;3]

// Append and reverse. Two built-in functions
List.append  // ('a list -> 'a list -> 'a list)
List.rev     // ('a list -> 'a list)
(@)   // ('a list -> 'a list -> 'a list)

let rec (@) xs ys =
    match xs with
    | []     -> ys
    | x::xs' -> x::(xs' @ ys)
// xs: 'a list -> ys: 'a list -> 'a list

[1;2] @ [3;4]       = [1; 2; 3; 4]
[[1]; [2; 3]; [4]]  = [[1];[2;3]] @ [[4]]

// precdence
[1; 2; 3] = [1] @ 2 :: [3]
[1] @ (2 :: [3])

[1; 2; 3] = 1 :: [2] @ [3]
(1 :: [2]) @ [3]


/// rev
let rec naiveRev xls =
    match xls with
    | []    -> []
    | x::xs -> naiveRev xs @ [x]

[3;2;1] = naiveRev [1;2;3]


// 4.5 The value restrictions on polymorphic expressions

// polymorphic value expressions
let z1 = []
(5,[[]])
let p = (fun x -> [x])

[1] = p 1

//List.rev []
//naiveRev []
// Value restriction. The value 'it' has been inferred to have generic
naiveRev ([] : char list)  // but


// 4.6 Examples. A model-based approach

// Example: Cash register
// Consider an electronic cash register that contains a data register associating the name of the
// article and its price to each valid article code. A purchase comprises a sequence of items,
// where each item describes the purchase of one or several pieces of a specific article.

// The task is to construct a program that makes a bill of a purchase. For each item the bill
// must contain the name of the article, the number of pieces, and the total price, and the bill
// must also contain the grand total of the entire purchase.

type ArticleCode = string
type ArticleName = string

type Price       = int    // pr where pr >= 0

type Register    = (ArticleCode * (ArticleName*Price)) list

let reg = [ ("a1",("cheese",25));
            ("a2",("herring",4));
            ("a3",("soft drink",5)) ]

type NoPieces    = int   // np where np >= 0
type Item        = NoPieces * ArticleCode
type Purchase    = Item list

// example register
let pur = [(3,"a2"); (1,"a1")]


type Info    = NoPieces * ArticleName * Price
type Infoseq = Info list
type Bill    = Infoseq * Price

// example bll
([(3,"herring",12); (1,"cheese",25)],37)


let rec findArticle ac = function
    | (ac',adesc)::_
            when ac=ac' -> adesc
    | _::reg            -> findArticle ac reg
    | _                 ->
                failwith (ac + " is an unkown article code")

let rec makeBill reg = function
    | []            -> ([], 0)
    | (np,ac)::pur  ->
            let (aname,aprice) = findArticle ac reg
            let tprice         = np * aprice
            let (billtl,sumtl) = makeBill reg pur
            ((np,aname,tprice)::billtl,tprice+sumtl)


// example
makeBill reg pur
// ([(3, "herring", 12); (1, "cheese", 25)], 37)


// Example: Map colouring

type Country = string
type Map     = (Country * Country) list

// example map
let exMap = [("a","b"); ("c","d"); ("d","a")]

/// a map is represented by the set of countries having this colour
type Colour    = Country list
/// a colouring is described by a list of mutually disjoint colours
type Colouring = Colour list

// a colouring
[["a";"c"]; ["b"; "d"]]

/// determine for a given map whether two countries are neighbours
let areNb m c1 c2 =
    isMember (c1,c2) m || isMember (c2,c1) m

/// predicate to determine for a given map whether a colour can be
//   extended by a country
let rec canBeExtBy m col c =
    match col with
    | []        -> true
    | c'::col'  -> not (areNb m c' c) && canBeExtBy m col' c

true = canBeExtBy exMap ["c"] "a"
false = canBeExtBy exMap ["a"; "c"] "b"


/// for a given map extend a partial colouring by a country
let rec extColouring m cols c =
    match cols with
    | []          -> [[c]]
    | col::cols'  -> if canBeExtBy m col c
                     then (c::col)::cols'
                     else col::extColouring m cols' c

[["a"]]        = extColouring exMap [] "a"
[["a"; "c"]]   = extColouring exMap [["c"]] "a"
[["b"]; ["a"]] = extColouring exMap [["b"]] "a"


/// add unless already a member
let addElem x ys = if isMember x ys then ys else x::ys

/// extract a list of countries without repeated elements from a map 
let rec countries = function
    | []         -> []
    | (c1,c2)::m -> addElem c1 (addElem c2 (countries m))

/// a function to colour a list of countries given a map:
let rec colCntrs m = function
    | []        -> []
    | c::cs     -> extColouring m (colCntrs m cs) c

/// function giving a colouring for a given map
let colMap m = colCntrs m (countries m)

[["c"; "a"]; ["b"; "d"]] = colMap exMap
