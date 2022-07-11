# FP in Real Life

## Learning Goals

- Apply FP principles on a real life example

## Connect - Remember the Future of the past 🤪

Complete the sentences below:

- ... holds one of two values `Left` or `Right`.
- ... is a monadic container which represents a computation that may either throw an exception or successfully
  completes.
- ... don't refer to any global state -> same inputs will always return the same output.
- ... is a type safe alternative to null values.
- ... is the `using` to rule them all.
- ... is any type that defines how map works.

![Back to the future](img/back-to-the-future.webp)


<details>
  <summary markdown='span'>
  Correction
  </summary>

- `Either` holds one of two values `Left` or `Right`.
- `Try` is a monadic container which represents a computation that may either throw an exception or successfully
  completes.
- `Pure Functions` don't refer to any global state -> same inputs will always return the same output.
- `Option` is a type safe alternative to null values.
- `using static LanguageExt.Prelude` is the `using` to rule them all
- A `functor` is any type that defines how map works.

</details>

## Concepts

You already know what you need to use those concepts in your code base.
So today let's focus only on practicing.

## Concrete Practice - FP in Real Life

Open `RealLifeExample` and refactor the code in a more readable / functional way.
We propose to follow those steps:

- Understand what is implemented
- Define the pipeline you would like to design
    - What are the ins / outs?
- Then refactor to tend to this pipeline

Step-by-step solution available [here](step-by-step.md)

## Conclusion - Compare

Let's compare our refactored code with the original one.

- How do you rate the readability of those 2?
- Can you spot any remaining code smells?
- How good is the test coverage?