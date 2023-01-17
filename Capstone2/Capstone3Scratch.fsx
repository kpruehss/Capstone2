#load "Domain.fs"
#load "Operations.fs"

open Capstone2.Domain
open Capstone2.Operations
open System

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

let openingAccount =
    { Owner = { Name = "Karsten" }
      Balance = 0M
      AccountId = Guid.Empty }

let account =
    let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w' ]

    commands
    |> Seq.filter isValidCommand
    |> Seq.takeWhile (not << isStopCommand)
    |> Seq.map getAmount
    |> Seq.fold processCommand openingAccount
