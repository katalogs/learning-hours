namespace Demo.Greeter
{

    public class Controller
    {
        private readonly IEmailGateway _emailGateway;

        public Controller(IEmailGateway emailGateway)
        {
            _emailGateway = emailGateway;
        }

        public void GreetUser(string userEmail)
        {
            _emailGateway.SendGreetingsEmail(userEmail);
        }
    }

}