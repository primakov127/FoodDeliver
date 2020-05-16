using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Helpers
{
    public abstract class PropertyValidateModel : PropertyChangedBase, IDataErrorInfo
    {
        // check for general model error    
        public string Error { get { return null; } }

        // check for property errors    
        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this)
                        {
                            MemberName = columnName
                        }
                        , validationResults))
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

        public bool IsValid
        {
            get
            {
                var properties = GetType().GetProperties().Where(prop => prop.DeclaringType.Name == GetType().Name);

                foreach(var property in properties)
                {
                    if (this[property.Name] != null)
                    {
                        return false;
                    }
                }

                return true;

            }
        }
    }
}
