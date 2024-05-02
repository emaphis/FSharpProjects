// Lesson 5 - Modules and Namespaces
// Some tests

#load "Modeling.fs"
#load "Code.fs"

open MyProject


let john    = Registered ("John", true)
let mary    = Registered ("Mary",  true)
let richard = Registered ("Richard", false)
let sarah   = Unregistered "Sarah"


// Informal assertions as tests - should all evaluate to true
let assertMary    = Customer.calculateTotal(mary, 99.0M) = 99.0M
let assertJohn    = Customer.calculateTotal(john, 100.0M) = 90.0M
let assertRichard = Customer.calculateTotal(richard, 100.0M) = 100.0M
let assertSarah   = Customer.calculateTotal(sarah, 100.0M) = 100.0M
