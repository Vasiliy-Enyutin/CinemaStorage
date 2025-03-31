namespace MyProject.Core.Exceptions;

public class InvalidCredentialsException() : ApiException("Invalid username or password", 401);