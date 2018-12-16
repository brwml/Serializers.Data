using System.Runtime.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Serializers.Data.Test
{
    public class PropertyNameTestClass
    {
        public const string AlternatePropertyName = nameof(AlternatePropertyName);

        [IgnoreDataMember]
        public object DoNotFindMe
        {
            get; set;
        }

        [DataMember(Name = AlternatePropertyName)]
        public object PropertyName
        {
            get; set;
        }
    }

    [TestClass]
    public class When_PropertyExistsByName
    {
        [TestMethod]
        public void Then_PropertyIsFound()
        {
            const string name = nameof(PropertyNameTestClass.PropertyName);
            var property = typeof(PropertyNameTestClass).GetPropertyInfo(name.ToLowerInvariant());
            Assert.IsNotNull(property);
            Assert.AreEqual(name, property.Name);
        }
    }

    [TestClass]
    public class When_PropertyHasDataMemberAttribute
    {
        [TestMethod]
        public void Then_PropertyIsFound()
        {
            var property = typeof(PropertyNameTestClass).GetPropertyInfo(PropertyNameTestClass.AlternatePropertyName.ToLowerInvariant());
            Assert.IsNotNull(property);
        }
    }

    [TestClass]
    public class When_PropertyHasIgnoreDataMemberAttribute
    {
        [TestMethod]
        public void Then_PropertyIsNotFound()
        {
            var property = typeof(PropertyNameTestClass).GetPropertyInfo(nameof(PropertyNameTestClass.DoNotFindMe));
            Assert.IsNull(property);
        }
    }

    [TestClass]
    public class When_PropertyNameDoesNotExist
    {
        [TestMethod]
        public void Then_PropertyIsNotFound()
        {
            var name = Any.StringValue(Any.Int32Value(50, 100));
            var property = typeof(PropertyNameTestClass).GetPropertyInfo(name);
            Assert.IsNull(property);
        }
    }
}