type Header =
    { header: string
      level: int
      lineNumber: int }


type SummarizeToken =
    { headers: Header list }

    static member Default = { headers = List.empty }


module Summarizer =
    let private addHeaderToList token header =
        match header with
        | Some header -> { token with headers = header :: token.headers }
        | None -> token

    let private sumWhileHash acc (c: char) =
        match acc with
        | (true, count) ->
            if c.Equals('#') then
                (true, count + 1)
            else
                (false, count)
        | halt -> halt

    let private getLevel header =
        header |> Seq.fold sumWhileHash (true, 0) |> snd

    let private buildHeader (header: string) lineNumber level =
        match level with
        | 0 -> None
        | n ->
            Some
                { header = header.Substring(level + 1)
                  lineNumber = lineNumber
                  level = n }

    let swap x f = f x

    let addHeader (header: string) lineNumber token =
        header
        |> getLevel
        |> buildHeader header lineNumber
        |> addHeaderToList token

    let collect token = List.rev token.headers

    let parse lines =
        lines
        |> Seq.map (fun (line: string) -> line.Trim())
        |> Seq.indexed
        |> Seq.fold (fun token (i, line) -> addHeader line (i + 1) token) SummarizeToken.Default
        |> collect

    let init = SummarizeToken.Default

module Runner =
    let private repeat text times = String.replicate times text

    let run (headers: Header seq) =
        headers
        |> Seq.iter (fun header ->
            printfn "%s- %s (line: %d)" (repeat "  " (header.level - 1)) header.header header.lineNumber)

[<EntryPoint>]
let main (args: string []) =
    if args.Length <> 1 then
        failwith "Error: Expected argument <file>"

    args
    |> Seq.head
    |> System.IO.File.ReadLines
    |> Summarizer.parse
    |> Runner.run

    0
