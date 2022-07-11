import {CommandBuilder} from "../src/CommandBuilder";
import {Command} from "../src/Command";
import { readFileSync } from 'fs';

test('test', () => {
	expect(0).toBe(0);
})

function getCommandsFromCommandsFile(): Command[] {
	const stringCommands: string[] = readFileSync('test/submarineCommands.txt', 'utf-8').split(/\r?\n/);
	return stringCommands.map(command => CommandBuilder.FromText(command));
}