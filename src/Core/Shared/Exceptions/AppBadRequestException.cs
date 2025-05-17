using System;

namespace TTM.Core.Shared.Exceptions;
public class AppBadRequestException(string message) : Exception(message)
{
}
