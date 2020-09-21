// Learn more about F# at http://fsharp.org

open System

type Instruction =
    | L // Turn 90 degrees left
    | M // Move forward 1 space
    | R // Turn 90 degrees right

module Instruction =
    let tryParse = function
        | 'L' -> Some L
        | 'M' -> Some M
        | 'R' -> Some R
        | _ -> None

type Direction =
    | N // North
    | E // East
    | S // South
    | W // West

module Direction =
    let rotateLeft = function
        | N -> W
        | E -> N
        | S -> E
        | W -> S

    let rotateRight = function
        | N -> E
        | E -> S
        | S -> W
        | W -> N

    let tryParse(str: string) =
        match str.ToUpper() with
        | "N" -> Some N
        | "E" -> Some E
        | "S" -> Some S
        | "W" -> Some W
        | _ -> None

type Position = { X: int; Y: int }

module Int32 =
    let tryParse (str: string) =
        match Int32.TryParse str with
        | true, x -> Some x
        | _ -> None

module Position =
    let translate position direction =
        match direction with
        | N -> { position with Y = position.Y + 1 }
        | E -> { position with X = position.X + 1 }
        | S -> { position with Y = position.Y - 1 }
        | W -> { position with X = position.X - 1 }

type RoverPosition =
    {
        Position: Position;
        Direction: Direction
    }

module RoverPosition =
    let tryParse (str:string) =
        let parts = str.Trim().Split([|" "|], StringSplitOptions.RemoveEmptyEntries)
        let x =  Int32.tryParse parts.[0]
        let y = Int32.tryParse parts.[1]
        let direction = Direction.tryParse parts.[2]
        match (x,y,direction) with
        | Some x, Some y, Some direction -> Some { Position = { X = x; Y = y; }; Direction = direction }
        | _ -> None

type Grid =
    {
        Top: int
        Right: int
    }
    member this.Bottom = 0
    member this.Left = 0
    member this.Contains(position: Position) =
        position.Y >= this.Bottom && position.Y <= this.Top &&
        position.X >= this.Left && position.Y <= this.Right

type Move = Grid -> RoverPosition -> Instruction -> RoverPosition

let move (grid:Grid) (roverPosition: RoverPosition) (instruction: Instruction) =
    let expectedPosition =
        match instruction with
        | L -> { roverPosition with Direction = Direction.rotateLeft roverPosition.Direction }
        | R -> { roverPosition with Direction = Direction.rotateRight roverPosition.Direction }
        | M -> { roverPosition with Position = Position.translate roverPosition.Position roverPosition.Direction }
    if grid.Contains(expectedPosition.Position) then
        expectedPosition
    else
        printfn "Attempted to go out of bounds (%d, %d)" expectedPosition.Position.X expectedPosition.Position.Y
        roverPosition

let parseInstructions (instructions: string) =
    instructions |> Array.ofSeq |> Array.choose Instruction.tryParse

let handleRoverPostionAndInstructions (grid: Grid) (lines: string array) =
    let roverPosition = RoverPosition.tryParse lines.[0]
    match roverPosition with
    | Some roverPosition ->
        let move' = move grid
        let instructions = parseInstructions lines.[1]
        let finalPosition = Seq.fold move' roverPosition instructions
        Some(finalPosition)
    | _ -> None

let handleGridLine (str: string) =
    let parts = str.Trim().Split([|" "|], StringSplitOptions.RemoveEmptyEntries)
    let top =  Int32.tryParse parts.[0]
    let right = Int32.tryParse parts.[1]
    match top, right with
    | Some top, Some right -> Some { Top = top; Right = right }
    | _ -> None

let handleSimulationInput (str: string) =
    let lines = str.Split([|"\r"; "\n" |], StringSplitOptions.RemoveEmptyEntries)
    let grid = handleGridLine lines.[0]
    match grid with
    | Some grid ->
        let lineCount = lines |> Seq.length
        if lineCount > 1 && not (lineCount % 2 = 1) then
            Error("Invalid number of instructions provided")
        else
            Ok (lines |> Seq.skip 1 |> Seq.chunkBySize 2 |> Seq.choose (handleRoverPostionAndInstructions grid))
    | None -> Error(sprintf "Couldn't parse the grid coordinates. Got the following string: %s" lines.[0])

let instructions =
    "5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM"

let result = handleSimulationInput instructions

result


