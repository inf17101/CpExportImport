using System;

namespace CpImportExportLibrary.src.Exceptions
{
    /// <summary>
    /// This class is a form of Throwable that indicates the conditions that a reasonable application might want to catch.
    /// This class is a Checked exception, needing to be declared in a method or constructor's throws clause if they can be
    /// thrown by the execution of the method or constructor and propagated outside the method or constructor boundary.
    /// </summary>
    public class ApiRequestException : Exception
    {
        internal ApiRequestException(string exceptionMessage) 
            : base(exceptionMessage) { }

        internal ApiRequestException() { }

        internal ApiRequestException(string exceptionMessage, Exception inner)
            : base(exceptionMessage, inner) { }

    }
}
