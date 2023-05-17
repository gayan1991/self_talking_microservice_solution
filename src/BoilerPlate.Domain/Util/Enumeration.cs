using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace BoilerPlate.Domain.Util
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; }
        public short Id { get; }

        protected Enumeration()
        { }

        protected Enumeration(short id, string name)
        {
            Id = id;
            Name = name;
        }

        #region implementation

        public int CompareTo(object obj)
        {
            if (obj is Enumeration val)
            {
                return Id.CompareTo(val.Id);
            }
            return -1;
        }

        #endregion

        #region override

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration val))
            {
                return false;
            }

            var valTypeMatches = GetType().Equals(obj.GetType());
            var valMatches = Id.Equals(val.Id);

            return valTypeMatches && valMatches;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region static

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var item = Parse<T, int>(value, "value", item => item.Id == value);
            return item;
        }

        public static T FromDisplayName<T>(string name) where T : Enumeration
        {
            var item = Parse<T, string>(name, "value", item => item.Name == name);
            return item;
        }

        public static T Parse<T, K>(K value, string desc, Func<T, bool> predicate) where T : Enumeration
        {
            var item = GetAll<T>().FirstOrDefault(predicate);

            if (item is null)
            {
                throw new InvalidOperationException($"'{value}' is not a valid {desc} in {typeof(T)}");
            }

            return item;
        }

        #endregion
    }
}
