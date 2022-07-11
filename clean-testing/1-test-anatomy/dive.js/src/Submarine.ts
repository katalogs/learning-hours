import {Command} from "./Command";
import {Commands} from "./Commands";
import {CommandBuilder} from "./CommandBuilder";

export class Submarine {
	aim: number = 0;
	position: number = 0;
	depth: number = 0;

	execute(command: Command): Submarine{
		let clone: Submarine = this.clone();
		switch(command.text) {
			case Commands.Up: 
				clone.aim -= command.value;
				return clone;
			case Commands.Down:
				clone.aim += command.value;
				return clone;
			case Commands.Forward:
				clone.position += command.value;
				clone.depth += command.value * clone.aim
				return clone;
			default:
				throw new Error("Unknown command "+command.text);
		}
	}

	executeBatch(...commands: string[]): Submarine {
		let clone: Submarine = this.clone();
		commands.forEach((command) => clone.execute(CommandBuilder.FromText(command)));
		return clone;
	}

	clone(): Submarine {
		let clone = new Submarine();
		clone.aim = this.aim;
		clone.position = this.position;
		clone.depth = this.depth;
		return clone;
	}
}