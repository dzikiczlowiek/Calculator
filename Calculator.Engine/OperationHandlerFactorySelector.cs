using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Calculator.Infrastructure;

using Castle.Facilities.TypedFactory;

namespace Calculator.Engine
{
    public class OperationHandlerFactorySelector : DefaultTypedFactoryComponentSelector
    {
        private readonly IEnumerable<IOperationStrategySpecification> _specifications;

        public OperationHandlerFactorySelector(IEnumerable<IOperationStrategySpecification> specifications)
        {
            _specifications = specifications;
        }

        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            var @operator = (Operator)arguments[0];

            var selector = _specifications.SingleOrDefault(x => x.IsApplicable(@operator));
            if (selector == null)
            {
                throw new InvalidOperationException($"Missing operation for given '{@operator}'.");
            }

            return selector.StrategyType;
        }
    }
}
