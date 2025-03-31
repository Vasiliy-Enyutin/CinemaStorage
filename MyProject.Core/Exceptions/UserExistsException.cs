namespace MyProject.Core.Exceptions;

public class UserExistsException() : ApiException("Username already exists", 409);