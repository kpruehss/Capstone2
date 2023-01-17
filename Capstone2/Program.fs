module Capstone2.Program

open System
open Capstone2.Domain
open Capstone2.Operations

let isValidCommand = (Set [ 'w'; 'd'; 'x' ]).Contains

let isStopCommand = (=) 'x'

let getAmount (command: char) =
    match command with
    | 'd' -> 'd', 50M
    | 'w' -> 'w', 25M
    | _ -> command, 0M

let processCommand account (command, amount) =
    if command = 'd' then
        account |> deposit amount
    else
        account |> withdraw amount

[<EntryPoint>]
let main _ =
    let name =
        Console.Write "Please enter your name: "
        Console.ReadLine()

    let withdrawWithAudit =
        auditAs "withdraw" Auditing.composedLogger withdraw

    let depositWithAudit =
        auditAs "deposit" Auditing.composedLogger deposit

    let openingAccount =
        { Owner = { Name = name }
          Balance = 0M
          AccountId = Guid.Empty }

    let closingAccount =
        // Fill in the main loop here...
        let openingAccount =
            { Owner = { Name = "Karsten" }
              Balance = 0M
              AccountId = Guid.Empty }

        let commands =
            seq {
                while true do
                    Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
                    yield Console.ReadKey().KeyChar
                    Console.WriteLine()
            }

        let getAmount command =
            Console.WriteLine()
            Console.Write "Enter Amount: "
            command, Console.ReadLine() |> Decimal.Parse

        // commands
        // |> Seq.filter isValidCommand
        // |> Seq.takeWhile (not << isStopCommand)
        // |> Seq.map getAmount
        // |> Seq.fold processCommand openingAccount

        openingAccount

    Console.Clear()
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0
