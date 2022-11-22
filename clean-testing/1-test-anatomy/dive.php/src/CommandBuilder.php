<?php

namespace DivePhp;

use DivePhp\Commands;

class CommandBuilder{

    public static function fromText($text){
        $params = $text.str_split(' ');
        return new Command($params[0], number_format($params[1]));
    }

    public static function forward($value){
        return new Command(Commands::FORWARD, $value);
    }

    public static function down($value){
        return new Command(Commands::DOWN, $value);
    }

    public static function up($value){
        return new Command(Commands::UP, $value);
    }
}