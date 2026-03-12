using Domain.Common.Results.Errors;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Domain.Common.Results
{
  public class Result<TValue> : IResult<TValue>
  {
    private TValue? _value = default;
    private bool _isSuccess = false;

    private readonly List<Error> _errors = new List<Error>();
    public List<Error> Errors => _errors;

    public bool IsSuccess => _isSuccess;
    public bool IsError => !_isSuccess;

    public Error? TopError => _errors.Count > 0 ? _errors[0] : null;

    public TValue? Value => _value;


    [JsonConstructor]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("For serializer only.", true)]
    public Result(TValue? value, List<Error>? errors, bool isSuccess)
    {
      if (isSuccess)
      {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        _isSuccess = true;
      }
      else
      {
        if (errors == null || errors.Count == 0)
        {
          throw new ArgumentException("Provide at least one error.", nameof(errors));
        }

        _errors = errors;
      }
    }

    private Result(Error error)
    {
      _errors = [error];
    }

    private Result(List<Error> errors)
    {
      if (errors is null || errors.Count == 0)
      {
        throw new ArgumentException("Cannot create an ErrorOr<TValue> from an empty collection of errors. Provide at least one error.", nameof(errors));
      }

      _errors = errors;
    }

    private Result(TValue value)
    {
      if (value is null)
      {
        throw new ArgumentNullException(nameof(value));
      }

      _value = value;
      _isSuccess = true;
    }
    public static implicit operator Result<TValue>(TValue value)
       => new(value);

    public static implicit operator Result<TValue>(Error error)
        => new(error);

    public static implicit operator Result<TValue>(List<Error> errors)
        => new(errors);

    public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
     => IsSuccess ? onValue(Value!) : onError(Errors);
  }
}
