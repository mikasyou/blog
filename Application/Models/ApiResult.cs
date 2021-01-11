using System;

namespace AyaBlog.Models
{
  public class ApiResult<T>
  {
    public int Result { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
  }
}