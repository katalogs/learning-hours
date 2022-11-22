<?php

namespace DivePhp;

use Exception;

class Submarine{

    var $aim = 0;
    var $position = 0;
    var $depth = 0;

    public function execute($command)
    {
        $clone = $this->clone();

        switch($command->text){
            case Commands::UP:
                $clone->aim -= $command->value;
                return $clone;
            case Commands::DOWN:
                $clone->aim += $command->value;
                return $clone;
            case Commands::FORWARD:
                $clone->position += $command->value;
                $clone->depth += $command->value * $clone->aim;
                return $clone;
            default:
                throw new Exception("Unknown command " + $command->text);
        }
    }

    function executeBatch($commands){
        $clone = $this->clone();

        foreach($commands as $command)
        {
            $clone->execute(CommandBuilder::fromText($command));
        }
        return $clone;
    }

    function clone(){
        $clone = new Submarine();
        $clone->aim = $this->aim;
        $clone->position = $this->position;
        $clone->depth =$this->depth;

        return $clone;
    }
}