# Refactoring test inputs with Test Data Builders

## Learning Goals

- Use test data builders to create test inputs
- Explain how to make test inputs more expressive and readable

## Preparation

- **Connect**:
  - Notes with the name of some following patterns, which can be used to create test inputs:
    - a class constructor
    - a class constructor with default values
    - a factory method
    - object mother
    - test data builder
    - test data builder + object mother combination
  - For each pattern, provide a note with an example
  - Randomize the notes
- **Concept** 
  - Slides to explain [the 4 rules of test data builders](http://www.natpryce.com/articles/000714.html)
  - An IDE with the kata you chose opened (some code generation capabilities are welcome)

## Connect (3 + 4 min)
**Map code examples with a pattern**

- Ask attendees to map code examples of test inputs construction with the corresponding patterns. (3 min)
- Discuss pros and cons of each pattern usage in a unit test suite context. (4 min)

## Concepts

### Presentation: 4 rules of a Test Data Builder (3 min)
Make a presentation of the 4 rules of `Test Data Builders`:
1. Has an instance variable for each constructor parameter
2. Initialises its instance variables to commonly used or safe values
3. Has a `build` method that creates a new object using the values in its instance variables
4. Has "chainable" public methods for overriding the values in its instance variables.

### Demo (10 min)
Demo the refactoring of test inputs using a test data builder on an existing test.
- use only with...() method names in a first time
- rename methods to make the object build more expressive (like natural language)
- replace usage of  overriding methods when the value set is not relevant for the test case

#### Tips
- You can use the [test-data-builders-kata](../../mikado-method/test-data-builders-kata), choosing the language of your choice, refactoring the Country creation.
- Use the [generate code from usage](../../generate-code-from-usage/Facilitation.md) method to focus on writing the code as you need to use it, and keep the demo short and clear.

## Concrete Practice (35 min)

Ask attendees to: 
- reproduce the demo on the same input
- apply the same technique to all objects created in the `arrange` part of the tests 

## Conclusion (5min) - Explain the main idea
If you had to explain the main idea of using test data builders to someone else, in 1 sentence, what would you say ?