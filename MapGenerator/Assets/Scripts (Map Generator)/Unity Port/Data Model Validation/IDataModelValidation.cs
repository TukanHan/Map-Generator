using System.Collections.Generic;

namespace MapGenerator.UnityPort
{
    public interface IDataModelValidation
    {
        IEnumerable<ValidationError> Validate();
    }
}
