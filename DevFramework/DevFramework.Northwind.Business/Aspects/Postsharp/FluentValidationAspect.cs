using FluentValidation;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;

namespace DevFramework.Northwind.Business.Aspects.Postsharp
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        Type _validatortype;

        public FluentValidationAspect(Type validatorType)
        {
            _validatortype = validatorType;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatortype);
            var entityType = _validatortype.BaseType.GetGenericArguments()[0];
            var entities = args.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidatorTool.FluentValidate(validator, entity);
            }

        }
    }
}
