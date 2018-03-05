using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;

namespace VPackage.Json.Test
{
    [TestClass]
    public class JSONSerializerTest
    {
        #region Serialize test
        [TestMethod]
        public void Serialize_AllCorrect_SerializeValue ()
        {
            string actual = JSONSerializer.Serialize<Person>(new Person()
            {
                Name = "John",
                Age = 26
            });

            string excepted = "{\"age\":26,\"name\":\"John\"}";

            Assert.AreEqual(excepted, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serialize_ArgumentNull_ArgumentNullException ()
        {
            JSONSerializer.Serialize<Person>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AttributeMissingException))]
        public void Serialize_TypeNotImplementingDataContract_AttributeMissingException ()
        {
            JSONSerializer.Serialize<Animal>(new Animal()
            {
                Name = "Fluffy",
                Age = 3
            });
        }

        #endregion

        #region Deserialization test
        [TestMethod]
        public void Deserialization_AllCorrect_Object ()
        {
            Person p = new Person() { Name = "John", Age = 26 };
            string serialized = JSONSerializer.Serialize<Person>(p);

            Assert.IsTrue(p == JSONSerializer.Deserialize<Person>(serialized));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Deserialization_ArgumentNull_ArgumentNullException ()
        {
            JSONSerializer.Deserialize<Person>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AttributeMissingException))]
        public void Deserialization_TypeNotImplementingDataContract_AttributeMissingException()
        {
            JSONSerializer.Deserialize<Animal>("ffhreufiu");
        }
        #endregion

        #region Classes
        // C'est classes sont la pour tester le bon fonctionnement de la classe

        internal class Animal
        {
            private string name;
            private int age;

            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    name = value;
                }
            }

            public int Age
            {
                get
                {
                    return age;
                }

                set
                {
                    age = value;
                }
            }
        }

        [DataContract]
        internal class Person
        {
            [DataMember]
            private string name;
            [DataMember]
            private int age;

            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    name = value;
                }
            }

            public int Age
            {
                get
                {
                    return age;
                }

                set
                {
                    age = value;
                }
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public static bool operator == (Person a, Person b)
            {
                return a.age == b.age && a.name == b.name;
            }

            public static bool operator != (Person a, Person b)
            {
                return !(a == b);
            }
        }
        #endregion
    }
}
