# Code Challenge - Solution

This program is the resolution of the following problem:

> # Martian robots
> Here we present a problem to solve. We will value the use of tests for the code challenge resolution.
> ## The Problem
> The surface of Mars can be modelled by a rectangular grid around which robots are able to move according to instructions provided from Earth. You are to write a program that determines each sequence of robot positions and reports the final position of the robot.
> 
> A robot position consists of a grid coordinate (a pair of integers: x-coordinate followed by y-coordinate) and an orientation (N, S, E, W for north, south, east, and west). A robot instruction is a string of the letters "L", "R", and "F" which represent, respectively, the instructions:
> *   Left: the robot turns left 90 degrees and remains on the current grid point.
> *   Right: the robot turns right 90 degrees and remains on the current grid point.
> *   Forward: the robot moves forward one grid point in the direction of the current orientation and maintains the same orientation.
> 
> The direction North corresponds to the direction from grid point (x, y) to grid point (x, y+1).
> 
> There is also a possibility that additional command types may be required in the future andprovision should be made for this.
> 
> Since the grid is rectangular and bounded (...yes Mars is a strange planet), a robot that moves "off" an edge of the grid is lost forever. However, lost robots leave a robot "scent" that prohibits future robots from dropping off the world at the same grid point. The scent is left at the last grid position the robot occupied before disappearing over the edge. An instruction to move "off" the world from a grid point from which a robot has been previously lost is simply ignored by the current robot.
> 
> ## The Input
> 
> The first line of input is the upper-right coordinates of the rectangular world, the lower-left coordinates are assumed to be 0, 0.
> 
> The remaining input consists of a sequence of robot positions and instructions (two lines per robot). A position consists of two integers specifying the initial coordinates of the robot and an orientation (N, S, E, W), all separated by whitespace on one line. A robot instruction is a string of the letters "L", "R", and "F" on one line.
> 
> Each robot is processed sequentially, i.e., finishes executing the robot instructions before the next robot begins execution.
> 
> The maximum value for any coordinate is 50.
> 
> All instruction strings will be less than 100 characters in length.
> 
> ## The Output
> 
> For each robot position/instruction in the input, the output should indicate the final grid position and orientation of the robot. If a robot falls off the edge of the grid the word "LOST" should be printed after the position and orientation.
> 
> ### Sample Input
> 
> ```
> 5 3
> 1 1 E
> RFRFRFRF
> 3 2 N
> FRRFLLFFRRFLL
> 0 3 W
> LLFFFLFLFL
> ```
> 
> ### Sample Output
> 
> ```
> 1 1 E
> 3 3 N LOST
> 2 3 S
> ```


## Solution design
The design of the current solution is oriented towards 2 main points of extensibility/scalability:

- Robot's movement instructions
- User input commands


### Robot's movement instructions

Robot movements instructions are modeled based on a _Strategy_ pattern, where every allowed movement has a specific class that implements the way in which a given set of coordinates and orientation of the robot are transformed onto the destination ones.

The generation of each specific movement instance that allows the robot to move at every instruction entered is commanded by an implementation of the _Factory_ pattern, that holds a _singleton_ for each of them and resolves the correct implementation based on metadata set on the `MovementInstructionAttribute` decorating each of them.

If a new movement should be added to the rover, it would suffice to add a new class implementing `IMovement`, with the specific implementation of the `Move` method, and map it to the command character by decorating it with the `MovementInstructionAttribute`. The `MovementFactory` will automatically discover the `IMovement` implementations and retrieve the correct instance based on the attribute decorating them.

### User input commands

For the user input commands, the design follows an implementation of the _Command_ pattern by means of the library `MediatR`, which also follows the pattern _Mediator_.

Commands instances, similarly to the Movements, are generated by a `CommandFactory` that resolves the correct type to instance based on a Regex expression decorating each command with `CommandRegexAttribute`. This is similar to some implementations of Discord SDKs for bots.

By using MediatR and its DI injection, this helps simplify the wiring between Commands and Handlers as well as the dependencies injection for the later and, not less important, it totally decouples this logic from the main app's user input logic.

If there should be need for more commands, all it would be needed is to add a new class inheriting from `CommandBase`, map it to the input regex pattern by decorating it with `CommandRegexAttribute` and write its corresponding handler to implement the code to run for such command.

### Extending functionality examples

The branch `extendingFeatures` contains examples on how to extend both commands and rover instructions, as well as an updated set of unit tests for the new functionalities.