namespace MyProject.Core.Exceptions;

public class JwtConfigurationException(string message) : ApiException(message, 500);