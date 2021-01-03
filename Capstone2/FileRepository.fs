module Capstone2.FileRepository

open Capstone2.Domain
open System.IO
open System

let private accountsPath =
    let path = @"accounts"
    Directory.CreateDirectory path |> ignore
    path

let private findAccountFolder owner =
    let folders =
        Directory.EnumerateDirectories(accountsPath, sprintf $"%s{owner}_*")

    if Seq.isEmpty folders then
        ""
    else
        let folder = Seq.head folders
        DirectoryInfo(folder).Name

let private buildPath (owner, accountId: Guid) =
    sprintf @"%s\%s_%O" accountsPath owner accountId

// Logs to the file System
let writeTransaction accountId owner message =
    let path = buildPath (owner, accountId)
    path |> Directory.CreateDirectory |> ignore

    let filePath =
        sprintf $"%s{path}/%d{DateTime.UtcNow.ToFileTimeUtc()}"

    File.WriteAllText(filePath, message)
