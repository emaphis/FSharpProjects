// Discriminated unions

type Title = string
type Rating = string
type Plot = string
type Ranking = float
type Year = int

type Movie = Movie of Title * Rating * Plot * Ranking * Year

let Serenity = Movie("Serenity", "PG-13",
    "The crew of the ship Serenity tries to evade an assassin sent to recapture one of/
their number who is telepathic.",
    8.0,
    2005)


type Human = { first: string; last: string }

type IntelligentBeing =
    | Robot of float * int // model and year
    | H of Human

let unit1 = Robot (8.5, 2052)
let unit2 = H {first = "Stephen"; last="Hawking"}
