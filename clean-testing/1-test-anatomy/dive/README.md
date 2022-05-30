### Story
It seems like the submarine can take a series of commands like `forward 1`, `down 2`, or `up 3`:
- `down X` **increases** your aim by `X` units.
- `up X` **decreases** your aim by `X` units.
- `forward X` does two things:
    - It increases your horizontal position by `X` units.
    - It increases your depth by your aim **multiplied by** `X`.

Note that since you're on a submarine, `down` and `up` affect your **aim** and do the opposite of what you might think.

The submarine seems to already have a planned course. You should probably figure out where it's going. For example:

- `forward 5`
- `down 5`
- `forward 8`
- `up 3`
- `down 8`
- `forward 2`

Your horizontal position, depth and aim all start at 0. The steps above would then modify them as follows:

- `forward 5` adds `5` to your horizontal position, a total of `5`. 
  - Because your aim is `0`, your depth does not change.
- `down 5` adds `5` to your aim, resulting in a value of `5`.
- `forward 8` adds `8` to your horizontal position, a total of `13`. 
  - Because your aim is `5`, your depth increases by `8*5=40`.
- `up 3` decreases your aim by `3`, resulting in a value of `2`.
- `down 8` adds `8` to your aim, resulting in a value of `10`.
- `forward 2` adds `2` to your horizontal position, a total of `15`.
  - Because your aim is `10`, your depth increases by `2*10=20` to a total of `60`.

After following these instructions, you would have a horizontal position of `15` and a depth of `60`.

Original instructions come from the AOC2021 and are available [here](// Advent of code instructions available here : https://adventofcode.com/2021/day/2)

### Goal
The goal of the kata is to write the following unit tests as expressed during an example mapping workshop with your business:

> Be careful we have probably missed certain Test cases...

```gherkin
Feature: Submarine
Verifying the submarine controls are correct

    Scenario: Submarine default position
        Given submarine is initialized
        Then submarine depth should be 0
        And submarine position should be 0

    Scenario: Step 1
        Given submarine is initialized
        When submarine receives command forward 5
        Then submarine depth should be 0
        And submarine position should be 5
        And submarine aim should be 0

    Scenario: Step 2
        Given submarine is initialized
        And submarine receives command forward 5
        When submarine receives command down 5
        Then submarine depth should be 0
        And submarine position should be 5
        And submarine aim should be 5

    Scenario: Step 3
        Given submarine is initialized
        And submarine receives command forward 5
        And submarine receives command down 5
        When submarine receives command forward 8
        Then submarine depth should be 40
        And submarine position should be 13
        And submarine aim should be 5

    Scenario: Step 4
        Given submarine is initialized
        And submarine receives command forward 5
        And submarine receives command down 5
        And submarine receives command forward 8
        When submarine receives command up 3
        Then submarine depth should be 40
        And submarine position should be 13
        And submarine aim should be 2

    Scenario: Step 5
        Given submarine is initialized
        And submarine receives command forward 5
        And submarine receives command down 5
        And submarine receives command forward 8
        And submarine receives command up 3
        When submarine receives command down 8
        Then submarine depth should be 40
        And submarine position should be 13
        And submarine aim should be 10

    Scenario: Step 6
        Given submarine is initialized
        And submarine receives command forward 5
        And submarine receives command down 5
        And submarine receives command forward 8
        And submarine receives command up 3
        And submarine receives command down 8
        When submarine receives command forward 2
        Then submarine depth should be 60
        And submarine position should be 15
        And submarine aim should be 10

    Scenario: Full example
        Given submarine is initialized
        When submarine receives all commands from file submarineCommands.txt
        Then submarine depth should be 987457
        And submarine position should be 2162
        And submarine aim should be 1051
```

Original kata is available [here](https://github.com/Tr00d/TcrKata)