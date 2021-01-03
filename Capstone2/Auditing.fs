module Capstone2.Auditing

open System.IO
open Capstone2.Operations
open Capstone2.Domain

let printTransaction _ accountId message =
    printfn $"Account {accountId}: {message}"

// Logs to both console and file system
let composedLogger =
    let loggers =
        [ FileRepository.writeTransaction
          printTransaction ]

    fun accountId owner message ->
        loggers
        |> List.iter (fun logger -> logger accountId owner message)
