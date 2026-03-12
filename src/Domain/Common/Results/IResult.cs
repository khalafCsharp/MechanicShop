using Domain.Common.Results.Errors;

namespace Domain.Common.Results
{
  public interface IResult<out TValue>
  {
    List<Error> Errors { get; }
    bool IsSuccess { get; }
    TValue? Value { get; }
  }
}
