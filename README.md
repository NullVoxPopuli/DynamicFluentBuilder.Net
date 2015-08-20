# DynamicFluentBuilder.Net [![Build Status](https://travis-ci.org/NullVoxPopuli/DynamicFluentBuilder.Net.svg)](https://travis-ci.org/NullVoxPopuli/DynamicFluentBuilder.Net)
The Fluent Builder Pattern without all the manual work!

### Defining a Builder

Given that a class, `foo` exists defined as:

    public class Foo
    {
        public bool IsTrue;
        private readonly string _message;

        public Foo(string message, bool isTrue)
        {
            IsTrue = isTrue;
            _message = message;
        }
    }


A builder for `foo` may look something like this:

    public class FooBuilder : DynamicFluentBuilder<Foo>{}

That's it!
I like to put these is Support/Builders in my Test projects


### Usage

    dynamic _builder = new FooBuilder();

    var foo = _builder
        .WithMessage("hello world")
        .IsTrue(true)
        .Build();

and then `foo` will look like this:

    <#Foo>:
      IsTrue: true
      _message: "hello world"
      Number: 0



### Additional Customization of the Builder


    public class FooBuilder : DynamicFluentBuilder<Foo>
    {
        // Note: these must be named the same as in the Model
        public string SetByDefaultInBuilder = "Dynamic code is fun code."

        public FooBuilder WithBooleansOfValue(bool isTrue, bool booleanField)
        {
            dynamic me = this;

            me.IsTrue(isTrue)
              .BooleanField(booleanField);

            return this;
        }
    }
