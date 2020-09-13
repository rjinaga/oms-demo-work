namespace Navtech.Oms.WebApi.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.ModelBinding;
    
    public class ModelStateException : Exception
    {
        public ModelStateException(IEnumerable<ModelError> modelErrors)
        {
            ModelErrors = modelErrors;
        }

        public IEnumerable<ModelError> ModelErrors { get; }
    }
}
