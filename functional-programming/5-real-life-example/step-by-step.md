# Real Life Example
### Understand what is implemented

By checking the code we can understand this logic :

```text
1. Given an id
2. Retrieve a user details: email, first name and password (not really secured ;-)
3. Register an account on Twitter
4. Authenticate on Twitter
5. Tweet *Hello I am ...*
6. Update the user details with the Twitter account id
7. Log something in case of success
8. Return the tweet URL
9. In case of error anywhere log something also
```

This code is easy to follow but has a lot of repetitions (check null values -> return null)

### Define the pipeline

```C#
// Given an id 
_userService.FindById(id) // Retrieve a user details
  | _twitterService.Register(user.Email, user.Name) // Register an account on Twitter
  | _twitterService.Authenticate(user.Email, user.Password) // Authenticate on Twitter
  | _twitterService.Tweet(twitterToken, "Hello I am " + user.Name) // Tweet *Hello I am ...*
  | _userService.UpdateTwitterAccountId(id, accountId) // Update the user details with the Twitter account id
  | _businessLogger.LogSuccessRegister(id) // Log something in case of success
  | _businessLogger.LogFailureRegister(id, ex) // In case of error anywhere log it
  | tweetUrl // Return the tweet URL or null in case of failure / Exception
```

## How to ?

* We want to avoid a lot of null checks so we could use `Option` or `Try`

* Here we will use `Try` monad because our business logic is already inside a `try/catch` block
    * We will chain calls through it

This is a step by step guide with a "*naive*" approach :

* Don't anticipate and introduce concepts before we need it
* At everytime the test will compile and tests are green

### 1) Extract "Retrieve a user details" method

Extract `_userService.FindById(id);` into a function returning a Try monad

```C#
public string Register(Guid id)
{
    try
    {
        var user = RetrieveUserDetails(id).IfFailThrow();

        if (user == null) return null;

        var accountId = _twitterService.Register(user.Email, user.Name);

        if (accountId == null) return null;

        var twitterToken = _twitterService.Authenticate(user.Email, user.Password);

        if (twitterToken == null) return null;

        var tweetUrl = _twitterService.Tweet(twitterToken, "Hello I am " + user.Name);

        if (tweetUrl == null) return null;

        _userService.UpdateTwitterAccountId(id, accountId);
        _businessLogger.LogSuccessRegister(id);

        return tweetUrl;
    }
    catch (Exception ex)
    {
        _businessLogger.LogFailureRegister(id, ex);

        return null;
    }
}

private Try<User> RetrieveUserDetails(Guid id)
    => () => _userService.FindById(id);
```

### 2) Extract "Register an account on Twitter" method

```C#
public string Register(Guid id)
{
    try
    {
        var user = RetrieveUserDetails(id).IfFailThrow();

        if (user == null) return null;

        var accountId = RegisterAccountOnTwitter(user).IfFailThrow();

        if (accountId == null) return null;

        var twitterToken = _twitterService.Authenticate(user.Email, user.Password);

        if (twitterToken == null) return null;

        var tweetUrl = _twitterService.Tweet(twitterToken, "Hello I am " + user.Name);

        if (tweetUrl == null) return null;

        _userService.UpdateTwitterAccountId(id, accountId);
        _businessLogger.LogSuccessRegister(id);

        return tweetUrl;
    }
    catch (Exception ex)
    {
        _businessLogger.LogFailureRegister(id, ex);

        return null;
    }
}

private Try<User> RetrieveUserDetails(Guid id)
    => () => _userService.FindById(id);

private Try<string> RegisterAccountOnTwitter(User user)
    => () => _twitterService.Register(user.Email, user.Name);
```

### 3) Extract "Authenticate on Twitter" method

```C#
public string Register(Guid id)
{
    try
    {
        var user = RetrieveUserDetails(id).IfFailThrow();

        if (user == null) return null;

        var accountId = RegisterAccountOnTwitter(user).IfFailThrow();

        if (accountId == null) return null;

        var twitterToken = AuthenticateOnTwitter(user).IfFailThrow();

        if (twitterToken == null) return null;

        var tweetUrl = _twitterService.Tweet(twitterToken, "Hello I am " + user.Name);

        if (tweetUrl == null) return null;

        _userService.UpdateTwitterAccountId(id, accountId);
        _businessLogger.LogSuccessRegister(id);

        return tweetUrl;
    }
    catch (Exception ex)
    {
        _businessLogger.LogFailureRegister(id, ex);

        return null;
    }
}

private Try<User> RetrieveUserDetails(Guid id)
    => () => _userService.FindById(id);

private Try<string> RegisterAccountOnTwitter(User user)
    => () => _twitterService.Register(user.Email, user.Name);

private Try<string> AuthenticateOnTwitter(User user)
    => () => _twitterService.Authenticate(user.Email, user.Password);
```

### 4) A few methods later

```C#
public string Register(Guid id)
{
    try
    {
        var user = RetrieveUserDetails(id).IfFailThrow();
        if (user == null) return null;

        var accountId = RegisterAccountOnTwitter(user).IfFailThrow();
        if (accountId == null) return null;

        var twitterToken = AuthenticateOnTwitter(user).IfFailThrow();
        if (twitterToken == null) return null;

        var tweetUrl = Tweet(twitterToken, user).IfFailThrow();
        if (tweetUrl == null) return null;

        UpdateTwitterAccount(id, accountId);
        _businessLogger.LogSuccessRegister(id);

        return tweetUrl;
    }
    catch (Exception ex)
    {
        _businessLogger.LogFailureRegister(id, ex);
        return null;
    }
}

private Try<User> RetrieveUserDetails(Guid id)
    => () => _userService.FindById(id);

private Try<string> RegisterAccountOnTwitter(User user)
    => () => _twitterService.Register(user.Email, user.Name);

private Try<string> AuthenticateOnTwitter(User user)
    => () => _twitterService.Authenticate(user.Email, user.Password);

private Try<string> Tweet(string twitterToken, User user)
    => () => _twitterService.Tweet(twitterToken, "Hello I am " + user.Name);

private void UpdateTwitterAccount(Guid id, string accountId)
    => _userService.UpdateTwitterAccountId(id, accountId);
```

### 5) Pipeline ?

*Where is the pipeline ? You sold me a pipeline... all I see is IfFailThrow and no gain in it*

Indeed, so our next step is to put all together : you may have already seen the problem

> Take a look at the method signatures : we are not working on the same kind of data

If we chain our calls we will have something like this :  
![1](../img/1.png)

#### What is the solution ?

* Use a `Context` object that will help to chain calls

```C#
private Try<Context> DoSomething(Context context)
```

* Create a Context class that will serve as a container of the needed data in the pipeline
* We can use a `record` to avoid having to write boilerplate code

```C#
public record RegistrationContext(
    Guid Id,
    string Email,
    string Name,
    string Password,
    string AccountId = "",
    string Token = "",
    string Url = ""
);
```

* We can add an extension method to easily instantiate the Context based on the User Infos

```C#
public static class RegistrationExtensions
{
    public static RegistrationContext ToContext(this User user)
        => new(user.Id, user.Email, user.Name, user.Password);
}
```

* Then chain the calls by changing method signatures :

```C#
private Try<RegistrationContext> RetrieveUserDetails(Guid userId) =>
    Try(() => _userService.FindById(userId))
        .Map(user => user.ToContext());

// example of with usage on records
private Try<RegistrationContext> RegisterOnTwitter(RegistrationContext context) =>
    Try(() => _twitterService.Register(context.Email, context.Name))
        .Map(twitterAccountId => context with {AccountId = twitterAccountId});
```

* Repeat it for each method
* At the end you should have a pipeline looking like

```C#
return RetrieveUserDetails(id)
        .Bind(RegisterOnTwitter)
        .Bind(AuthenticateOnTwitter)
        .Bind(Tweet)
        .Bind(UpdateUser);
```

* Add the logging part to it

```C#
return RetrieveUserDetails(id)
        .Bind(RegisterOnTwitter)
        .Bind(AuthenticateOnTwitter)
        .Bind(Tweet)
        .Bind(UpdateUser)
        .Do(context => _businessLogger.LogSuccessRegister(context.Id))
        .Map(context => context.Url)
        .IfFail(failure =>
        {
            _businessLogger.LogFailureRegister(id, failure);
            return null;
        });
```

* We need to adapt the consumers : aka the tests here

```C#
[Fact]
public void Register_BudSpencer_should_return_a_new_tweet_url()
{
    var tweetUrl = _accountService.Register(BudSpencer);
    tweetUrl.GetUnsafe().Should().Be("TweetUrl");
}

[Fact]
public void Register_an_unknown_user_should_return_an_error_message()
{
    var tweetUrl = _accountService.Register(UnknownUser);
    tweetUrl.IsNone.Should().BeTrue();
}
```

* We end up with something like :

```C#
public class AccountService
{
    private readonly IBusinessLogger _businessLogger;
    private readonly TwitterService _twitterService;
    private readonly UserService _userService;

    public AccountService(
        UserService userService,
        TwitterService twitterService,
        IBusinessLogger businessLogger)
    {
        _userService = userService;
        _twitterService = twitterService;
        _businessLogger = businessLogger;
    }

    private Try<RegistrationContext> RetrieveUserDetails(Guid userId) =>
        Try(() => _userService.FindById(userId))
            .Map(user => user.ToContext());

    private Try<RegistrationContext> RegisterOnTwitter(RegistrationContext context) =>
        Try(() => _twitterService.Register(context.Email, context.Name))
            .Map(twitterAccountId => context with {AccountId = twitterAccountId});

    private Try<RegistrationContext> AuthenticateOnTwitter(RegistrationContext context) =>
        Try(() => _twitterService.Authenticate(context.Email, context.Password))
            .Map(token => context with {Token = token});

    private Try<RegistrationContext> Tweet(RegistrationContext context) =>
        Try(() => _twitterService.Tweet(context.Token, "Hello I am " + context.Name))
            .Map(tweetUrl => context with {Url = tweetUrl});

    private Try<RegistrationContext> UpdateUser(RegistrationContext context) =>
        Try(() =>
        {
            _userService.UpdateTwitterAccountId(context.Id, context.AccountId);
            return context;
        });

    public Option<string> Register(Guid id)
    {
        return RetrieveUserDetails(id)
            .Bind(RegisterOnTwitter)
            .Bind(AuthenticateOnTwitter)
            .Bind(Tweet)
            .Bind(UpdateUser)
            .Do(context => _businessLogger.LogSuccessRegister(context.Id))
            .Map(context => context.Url)
            .IfFail(failure =>
            {
                _businessLogger.LogFailureRegister(id, failure);
                return null;
            });
    }
}
```

What do you think about it ?