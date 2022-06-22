# Crappy Driven Development
## Learning Goals
- Create an anti-pattern list
- Work in mob programming
- Practice automated refactoring

## Connect - Dirty vs Clean Code (5 min)
According to you:
- What is `dirty` code?
- What is `clean` code?

## Concepts - CDD (10 min)
Let's learn a new development practice : Crappy-Driven Development.
`The secret art of making yourself indispensable by writing crappy code !!!`

![Crappy Driven Development](img/crappy-driven-development.png)

### Mob roles
You can use [mobtime](https://mobti.me/) and configure below roles :

* Turn Duration : 5 minutes
* Create the 4 roles presented below

#### Driver
![driver](img/driver.png)

* Write the code according to the navigator's specification
* Listen intently to the navigators instructions
* Ask questions wherever there is a lack of clarity

#### Navigator
![navigator](img/navigator.png)

* Dictates the code that is to be written - the 'what'
* Clearly communicates what code to write
* Explains 'why' they have chosen the particular solution to this problem
* Check for syntax / type errors as the Driver drives

#### Scribe
![scribe](img/scribe.png)

* Write down the goals of each cycle in mobtime
* Explain why it has been decided

#### Crappier
![crappier](img/crappier.png)

* Anticipate what can be crappier
* Write down ideas that emerge in his/he mind and other ideas as well

## Concrete Practice - CDD in mob (40 min)
In small groups / [mob](https://www.youtube.com/watch?v=SHOVVnRB4h0&ab_channel=GOTOConferences) :

* Open the `password-challenge` solution
* Your objective : Apply CDD to make the code so crappy that other groups won't be able to understand it
  * Follow the golden rules described above
  * Make it in Baby steps (crappy 1 thing at a time)
* You have 40 minutes to make as many cycles as possible :
  * Make it in mob
  * Be creative
  * The more brainfuck it is the better
  * Tests are green before and at the end of each refactoring

## Conclusion (5min)
Take a few minutes to reflect and ask those questions :

- Which patterns did you observe recently in your codebase?
- What did you learn from this kata?
- How this practice can be applied in your current context?

Original kata available [here](https://github.com/ythirion/crappy-driven-development)