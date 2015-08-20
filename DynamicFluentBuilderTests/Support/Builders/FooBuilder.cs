using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicFluentBuilderTests.Support.Models;
using DynamicFluentBuilder.Net;

namespace DynamicFluentBuilderTests.Support.Builders
{
    public class FooBuilder : DynamicFluentBuilder<Foo>
    {
        // defaults here override defaults set dynamically.
        // dynamically defaults are things like
        // - 0 for ints / doubles
        // - false for booleans
        // - null for strings
        //
        // Note: these must be named the same as in the Model
        //   in this case, Foo


        public string SetByDefaultInBuilder = "Dynamic code is fun code.";

        public FooBuilder WithBooleansOfValue(bool isTrue, bool booleanField)
        {
            dynamic me = this;

            me.IsTrue(isTrue)
              .BooleanField(booleanField);

            return this;
        }
    }
}
