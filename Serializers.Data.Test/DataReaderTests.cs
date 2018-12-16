using System;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Serializers.Data.Test
{
    [TestClass]
    public class When_DateTimePropertyMapsExactly
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly object value;

        public When_DateTimePropertyMapsExactly()
        {
            this.value = DateTime.Now;
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_PropertyValueIsSet()
        {
            Assert.AreEqual(this.value, this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public DateTime Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_GuidPropertyMapsExactly
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly object value;

        public When_GuidPropertyMapsExactly()
        {
            this.value = Guid.NewGuid();
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_PropertyValueIsSet()
        {
            Assert.AreEqual(this.value, this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public Guid Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_Int16PropertyMappingFromInt32
    {
        private readonly Exception exception;
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly int value;

        public When_Int16PropertyMappingFromInt32()
        {
            this.value = Any.Int32Value();
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            try
            {
                this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
            }
            catch (Exception e)
            {
                this.exception = e;
            }
        }

        [TestMethod]
        public void Then_ArgumentExceptionIsThrown()
        {
            Assert.IsInstanceOfType(this.exception, typeof(ArgumentException));
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public short Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_Int32PropertyMapsExactly
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly int value;

        public When_Int32PropertyMapsExactly()
        {
            this.value = Any.Int32Value();
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_PropertyValueIsSet()
        {
            Assert.AreEqual(this.value, this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public int Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_Int32PropertyMapsFromInt16
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly short value;

        public When_Int32PropertyMapsFromInt16()
        {
            this.value = Any.Int16Value();
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_PropertyValueIsSet()
        {
            Assert.AreEqual(this.value, this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public int Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_Int32ValueIsNull
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly object value;

        public When_Int32ValueIsNull()
        {
            this.value = DBNull.Value;
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_DefaultValueIsAssigned()
        {
            Assert.AreEqual(0, this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public int Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_PropertyDoesNotExist
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly string value;

        public When_PropertyDoesNotExist()
        {
            this.value = Any.StringValue(Any.Int32Value(50, 500));
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(Any.StringValue(50)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_PropertyValueIsNotSet()
        {
            Assert.IsNull(this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyGet(x => x.FieldCount, Times.Once);
            this.reader.Verify(x => x.GetName(It.IsAny<int>()), Times.Once);
            this.reader.Verify(x => x[It.IsAny<int>()], Times.Never);
        }

        private class TestClass
        {
            public string Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_PropertyMapsExactlyWithDifferenceCaseName
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly string value;

        public When_PropertyMapsExactlyWithDifferenceCaseName()
        {
            this.value = Any.StringValue(Any.Int32Value(50, 500));
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property).ToLowerInvariant()).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_PropertyValueIsSet()
        {
            Assert.AreEqual(this.value, this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public string Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_PropertyTypeDoesNotMatch
    {
        private readonly Exception exception;
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly int value;

        public When_PropertyTypeDoesNotMatch()
        {
            this.value = Any.Int32Value();
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            try
            {
                this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
            }
            catch (Exception e)
            {
                this.exception = e;
            }
        }

        [TestMethod]
        public void Then_ArgumentExceptionIsThrown()
        {
            Assert.IsInstanceOfType(this.exception, typeof(ArgumentException));
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public string Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_StringPropertyMapsExactly
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly string value;

        public When_StringPropertyMapsExactly()
        {
            this.value = Any.StringValue(Any.Int32Value(50, 500));
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_PropertyValueIsSet()
        {
            Assert.AreEqual(this.value, this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public string Property
            {
                get; set;
            }
        }
    }

    [TestClass]
    public class When_StringValueIsNull
    {
        private readonly TestClass instance;
        private readonly Mock<IDataReader> reader;
        private readonly object value;

        public When_StringValueIsNull()
        {
            this.value = DBNull.Value;
            this.reader = new Mock<IDataReader>();
            this.reader.SetupGet(x => x.FieldCount).Returns(1).Verifiable();
            this.reader.Setup(x => x.GetName(0)).Returns(nameof(TestClass.Property)).Verifiable();
            this.reader.Setup(x => x[0]).Returns(this.value).Verifiable();
            this.instance = DataReaderConvert.Deserialize<TestClass>(this.reader.Object);
        }

        [TestMethod]
        public void Then_DefaultValueIsAssigned()
        {
            Assert.IsNull(this.instance.Property);
        }

        [TestMethod]
        public void Then_ReaderMethodsAreCalled()
        {
            this.reader.VerifyAll();
        }

        private class TestClass
        {
            public string Property
            {
                get; set;
            }
        }
    }
}