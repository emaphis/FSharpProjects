module ObservableAdd


open System
open System.Windows.Forms
open System.Drawing

// Example 8-16. Subscribing to events with Observable.add

 let form = new Form(Text="Keep out of the bottom!", TopMost=true)
 
 form.MouseMove
 |> Observable.filter (fun moveArgs -> moveArgs.Y > form.Height / 2)
 |> Observable.add    (fun moveArgs -> MessageBox.Show("Moved into bottom half!")
                                       |> ignore)
 
 form.ShowDialog()

 (*
[<EntryPoint; STAThread>]
let main2 argv =
   
    Application.Run(form)
    0
    *)