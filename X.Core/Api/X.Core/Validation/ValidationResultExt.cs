using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.Core.Validation
{
    public static class ValidationResultExt
    {
        /// <summary>
        /// Adds to validation result list if isError = true
        /// </summary>
        /// <param name="validationResult"></param>
        /// <param name="isError"></param>
        /// <param name="message"></param>
        public static void Error(this ValidationResult validationResult, bool isError, string message)
        {
            if (isError)
            {
                validationResult.ResultList.Add(new ValidationResultItem()
                {
                    Description = message,
                    Level = ValidationResultLevelEnum.Error
                });
            }
        }
    }
}
