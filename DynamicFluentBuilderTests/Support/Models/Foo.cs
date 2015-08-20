using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicFluentBuilderTests.Support.Models
{
    public class Foo
    {
        public bool BooleanField;

        public bool IsTrue;
        private readonly string _message;
        protected int Number;

        public string SetByDefaultInBuilder;

        public Foo(string message, int number, bool isTrue)
        {
            IsTrue = isTrue;
            _message = message;
            Number = number;
        }

        public int GetNumber()
        {
            return Number;
        }

        public string GetMessage()
        {
            return _message;
        }
    }
}
