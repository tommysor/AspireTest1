namespace DomainSpesificLanguage;

public class AssertionException : Exception
{
    public AssertionException(string message) 
        : base(message) {}
}
