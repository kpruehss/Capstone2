module Capstone2.Auditing

open System.IO
open Capstone2.Domain

let consoleAudit account message =
    printfn $"Account {account.AccountId}: {message}"

let fileSystemAudit account message =
    Directory.CreateDirectory(sprintf @"./Logs/%s" account.Owner.Name)
    |> ignore

    let filePath =
        sprintf @"./Logs/%s/%O.txt" account.Owner.Name account.AccountId

    File.AppendAllLines(filePath, [ message ])
