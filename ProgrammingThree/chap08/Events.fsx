module Events

module CreatingEvents =

    // Example 8-11. Events and the Event<_,_> type

    type SetAction = Added | Removed

    type SetOperationEventArgs<'a>(value: 'a, action: SetAction) =
        inherit System.EventArgs()

        member this.Action = action
        member this.Value = value

    type SetOperationDelegate<'a> = delegate of obj * SetOperationEventArgs<'a> -> unit
    
    /// Contains a set of items that fires events whenever
    /// items are added.
    type NoisySet<'a when 'a: comparison>() =
        let mutable m_set = Set.empty: Set<'a>

        let m_itemAdded =
            Event<SetOperationDelegate<'a>, SetOperationEventArgs<'a>>()

        let m_itemRemoved =
            Event<SetOperationDelegate<'a>, SetOperationEventArgs<'a>>()

        member this.Add(x) =
            m_set <- m_set.Add(x)
            // Fire the 'Add' event
            m_itemAdded.Trigger(this,  new SetOperationEventArgs<_>(x, Added))

        member this.Remove(x) =
            m_set <- m_set.Remove(x)
            // Fire the 'Remove' event
            m_itemRemoved.Trigger(this, new SetOperationEventArgs<_>(x, Removed))

        // Publish the events so others can subscribe to them
        member this.ItemAddedEvent   = m_itemAdded.Publish
        member this.ItemRemovedEvent = m_itemRemoved.Publish



    // Example 8-12. Adding and removing event handlers
    // shows the NoisySet<_> type in action.
 

    // Using events
    let s = new NoisySet<int>()

    let setOperationHandler =
        new SetOperationDelegate<int>(
            fun sender args ->
                printf $"%d{args.Value} was %A{args.Action}"
        )

    s.ItemAddedEvent.AddHandler(setOperationHandler)
    s.ItemRemovedEvent.AddHandler(setOperationHandler)

    s.Add 9

    s.Remove 9


    // * The Event<_,_> Class *
    // The Event<'Del, 'Arg> type keeps track of the delegates associated with the event, and 
    // makes it easier to fire and publish an event.

    // Example 8-13. The DelegateEvent type
    // defines a clock type with a single event that gets fired every second,
    // notifying subscribers of the current hour, minute, and second.

    open System

    type ClockUpdateDelegate = delegate of int * int * int -> unit

    type Clock() =

        let m_event = DelegateEvent<ClockUpdateDelegate>()

        member this.Start() =
            printfn "Started ..."
            while true do
                // Sleep on second
                Threading.Thread.Sleep(1000)

                let hour   = DateTime.Now.Hour
                let minute = DateTime.Now.Minute
                let second = DateTime.Now.Second

                m_event.Trigger( [| box hour; box minute; box second |])

        member this.ClockUpdate = m_event.Publish


    // Run non-standard even types
    let c = Clock()
    
    // Adding an event hadler
    c.ClockUpdate.AddHandler(
        new ClockUpdateDelegate(
            fun h m s -> printfn $"[{h}:{m}:{s}]"
        )
    )

module ObservableModule =

    // Observable<_> interface, which allows you to treat events like first-class citizens much 
    // like objects and functions. 

    // Example 8-14. Compositional events
    // defines a JukeBox type that raises an event whenever a new song is played. 

    [<Measure>]
    type minute

    [<Measure>]
    type bpm = 1/minute

    type MusicGenre = Classical | Pop | HipHop | Rock | Latin | Country

    type Song = { Title : string; Genre : MusicGenre; BPM : int<bpm> }


    type SongChangeArgs(title: string, genre: MusicGenre, bpm: int<bpm>) =
        inherit System.EventArgs()

        member this.Title = title
        member this.Genre = genre
        member this.BeatsPerMinute = bpm


    type SongChangeDelegate = delegate of obj * SongChangeArgs -> unit


    type JukeBox() =
        let m_songStartedEvent = Event<SongChangeDelegate, SongChangeArgs>()

        member this.PlaySong(song) =
            m_songStartedEvent.Trigger(
                this,
                new SongChangeArgs(song.Title, song.Genre, song.BPM)
            )

        [<CLIEvent>]
        member this.SontStartedEvent = m_songStartedEvent.Publish


    // Example 8-15. Using the Event module

    // Use the Observable module to only subscribe to specific events

    let jb = JukeBox()

    let fastSongEvent, slowSongEvent =
        jb.SontStartedEvent
        // Filter event to just dance music
        |> Observable.filter (fun songArgs ->
             match songArgs.Genre with
             | Pop | HipHop | Latin | Country -> true
             | _ -> false)
        // Split the event into 'fast song' and 'slow song'
        |> Observable.partition(fun songChangeArgs ->
                songChangeArgs.BeatsPerMinute >= 120<bpm>)


    // Add event handlers to the IObservable
    slowSongEvent.Add(fun args ->
        printfn "You hear '%s' and start to dance slowly..." args.Title)

    fastSongEvent.Add(fun args ->
        printfn "You hear '%s' and start to dance fast!"  args.Title)

    do jb.PlaySong( { Title = "Burnin Love"; Genre = Pop; BPM = 120<bpm> } )


    // Observable.add
    // val add : ('a -> unit) -> IObservable<'a> -> unit

    // Example 8-16. Subscribing to events with Observable.add

    // System.Windows.Forms


    
    ()