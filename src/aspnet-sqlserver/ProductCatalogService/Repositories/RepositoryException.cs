using Microsoft.AspNetCore.Http;

namespace ProductCatalogService.Repositories;

public class RepositoryException<T> : Exception
{
  public string RepositoryName { get; }
  public int StatusCode { get; }

  public RepositoryException()
  {
  }

  public RepositoryException(string message)
      : base(message)
  {
  }

  public RepositoryException(string message, Exception inner)
      : base(message, inner)
  {
  }

  public RepositoryException(string message, Exception? innerException = null, int statusCode = 500)
        : base(message, innerException)
  {
    RepositoryName = typeof(T).Name;
    StatusCode = statusCode;
  }
}