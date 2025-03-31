namespace MyProject.Core.Exceptions;

public class ItemNotFoundException() : ApiException("Item not found", 400);