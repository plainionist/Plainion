using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using Plainion.Serialization;

namespace Plainion.Core.Tests.Serialization
{
    [TestFixture]
    public class SerializableBindableBaseTests
    {
        [Serializable]
        [DataContract]
        private class DummyEntity : SerializableBindableBase
        {
        }

        [Test]
        public void Serialize_WithDataContractSerializer_NoException()
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(DummyEntity));
                serializer.WriteObject(stream, new DummyEntity());
            }
        }

        [Test]
        public void Serialize_WithBinaryFormatter_NoException()
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new DummyEntity());
            }
        }
    }
}
