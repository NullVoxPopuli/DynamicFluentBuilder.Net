﻿using System;
using DynamicFluentBuilderTests.Support.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicFluentBuilderTests
{
    [TestClass]
    public class FooBuilderTests
    {
        private dynamic _builder;

        public FooBuilderTests()
        {
            _builder = new FooBuilder();
        }


        [TestMethod]
        public void Build_Succeeds()
        {
            var model = _builder.Build();

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void Build_SetsConstructorDefaults()
        {
            var model = _builder.Build();

            Assert.AreEqual(
                expected: false,
                actual: model.IsTrue);

            Assert.AreEqual(
                expected: 0,
                actual: model.GetNumber());

            Assert.AreEqual(
                expected: null,
                actual: model.GetMessage());
        }

        [TestMethod]
        public void Build_SetsFieldDefaults()
        {
            var model = _builder.Build();

            Assert.AreEqual(
                expected: false,
                actual: model.BooleanField);
        }

        [TestMethod]
        public void With_StringParameter()
        {
            var text = "hello world";

            var model = _builder
                .WithMessage(text)
                .Build();

            Assert.AreEqual(
                expected: text,
                actual: model.GetMessage());
        }

        [TestMethod]
        public void With_EachParameter()
        {
            var text = "hello there!";
            var boolean = true;
            var number = 1337;

            var model = _builder
                .WithMessage(text)
                .WithNumber(number)
                .IsTrue(boolean)
                .Build();

            Assert.AreEqual(
                expected: boolean,
                actual: model.IsTrue);

            Assert.AreEqual(
                expected: number,
                actual: model.GetNumber());

            Assert.AreEqual(
                expected: text,
                actual: model.GetMessage());
        }

        [TestMethod]
        public void With_NonConstructor()
        {
            var boolean = true;

            var model = _builder
                .BooleanField(boolean)
                .Build();

            Assert.AreEqual(
                expected: boolean,
                actual: model.BooleanField);
        }

        [TestMethod]
        public void Build_SetsDefaultViaBuilderDefinedDefault()
        {
            var model = _builder.Build();

            var expected = _builder.SetByDefaultInBuilder;

            Assert.AreEqual(
                expected: expected,
                actual: model.SetByDefaultInBuilder);
        }

    }
}