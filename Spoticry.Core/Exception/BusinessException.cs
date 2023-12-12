using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spoticry.Core.Validation;

namespace Spoticry.Core.Exception
{
    public class BusinessException : System.Exception
    {
        public List<BusinessValidation> Errors { get; set; } = new List<BusinessValidation>();

        public BusinessException() { }

        public BusinessException(BusinessValidation validation)
        {
            AdicionarError(validation);
        }

        public void AdicionarError(BusinessValidation validation)
        {
            Errors.Add(validation);
        }

        public void ValidateAndThrow()
        {
            if (Errors.Any())
                throw this;
        }
    }
}
