namespace Spoticry.ErrorHandling
{
    public class ErrorHandling
    {
        public List<ErrorMessage> Messages { get; set; } = new List<ErrorMessage>();
        public String ErrorDescription = "Aconteceram erros ao processar sua requisição";
    }
}
