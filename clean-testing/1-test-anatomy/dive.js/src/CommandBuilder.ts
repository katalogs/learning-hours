import {Command} from "./Command";
import {Commands} from "./Commands";

export class CommandBuilder {
	static FromText(text: string): Command {
		let params = text.split(' ');
		return new Command(params[0], Number(params[1]));
	}

	static Forward(value: number): Command {
		return new Command(Commands.Forward, value);
	}

	static Down(value: number): Command {
		return new Command(Commands.Down, value);
	}

	static Up(value: number): Command {
		return new Command(Commands.Up, value);
	}
}