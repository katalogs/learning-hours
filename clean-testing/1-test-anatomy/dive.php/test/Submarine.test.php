<?php

use PHPUnit\Framework\TestCase;

class SubmarineTest extends TestCase{

    public function test_first()
    {
        $this->assertSame(false, true);
    }

    private function submarine_commands() {
        $file = fopen('submarineCommands.txt', 'r');
        $string_commands = explode("/\r|\n/", file_get_contents('submarineCommands.txt'));
        fclose($file);

        return array_map('CommandBuilder::fromText', $string_commands);
    }
}