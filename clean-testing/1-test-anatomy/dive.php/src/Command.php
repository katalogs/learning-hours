<?php

namespace DivePhp;

class Command {
    var $text;
    var $value;

    function __construct($text, $value)
    {
        $this->text = $text;
        $this->value = $value;
    }
}