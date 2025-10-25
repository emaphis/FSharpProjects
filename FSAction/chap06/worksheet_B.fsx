// Chapter 6 - Functions and modules

// Organizing code
// Moving from scripts to applications

// 6.2 Organizing code

// 6.2.1 Namespaces

// Listing 6.6 Working with namespaces in F#

namespace Foo

type Order = { Name : string }


//  Listing 6.7 Opening a namespace

namespace Bar.Baz
open Foo

type Customer =
    {
        Name : string
        LastOrder : Order
    }
