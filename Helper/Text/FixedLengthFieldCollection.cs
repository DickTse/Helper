using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Helper.Text
{
    /// <summary>
    /// A collection of <see cref="IFixedLengthField"/> objects defined in a <see cref="FixedLengthFieldString"/> object.
    /// </summary>
    public class FixedLengthFieldCollection : ICollection<IFixedLengthField>
    {
        List<IFixedLengthField> fields = new List<IFixedLengthField>();

        /// <summary>
        /// Number of fields defined in the collection.
        /// </summary>
        public int Count { get { return fields.Count; } }

        /// <summary>
        /// Indicator of whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Sum of length of all <see cref="FixedLengthField"/> objects in the collection.
        /// </summary>
        public int TotalFieldLength { get { return fields.Sum(f => f.Length); } }

        /// <summary>
        /// Add a field into the collection.
        /// </summary>
        /// <param name="item">The <see cref="IFixedLengthField"/> object that is going to be added into the collection.</param>
        public void Add(IFixedLengthField item)
        {
            fields.Add(item);
        }

        /// <summary>
        /// Remove all <see cref="IFixedLengthField"/> objects from the collection.
        /// </summary>
        public void Clear()
        {
            ICollection<string> s = new List<string>();
        }

        /// <summary>
        /// Determine whether the <see cref="FixedLengthFieldCollection"/> collection contains a specified <see cref="IFixedLengthField"/> object.
        /// </summary>
        /// <param name="item">A <see cref="IFixedLengthField"/> object to be found.</param>
        public bool Contains(IFixedLengthField item)
        {
            return fields.Contains(item);
        }

        /// <summary>
        /// Copy elements from the <see cref="FixedLengthFieldCollection"/> collection to a designated array, starting from a particular index.
        /// </summary>
        /// <param name="array">The destinated array.</param>
        /// <param name="arrayIndex">The index of the element that the copy starting from.</param>
        public void CopyTo(IFixedLengthField[] array, int arrayIndex)
        {
            fields.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the colletion.</returns>
        public IEnumerator<IFixedLengthField> GetEnumerator()
        {
            return fields.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the colletion.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return fields.GetEnumerator();
        }

        /// <summary>
        /// Remove the first occurrance of a specific object from the <see cref="FixedLengthFieldCollection"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="FixedLengthFieldCollection"/>.</param>
        /// <returns>
        /// true if the item was successfully removed from the <see cref="FixedLengthFieldCollection"/>; otherwise, false.
        /// This method also returns false if the item is not found in the original <see cref="FixedLengthFieldCollection"/>.
        /// </returns>
        public bool Remove(IFixedLengthField item)
        {
            return fields.Remove(item);
        }

        /// <summary>
        /// Get or set the value from or to a <see cref="IFixedLengthField"/> object with a given field name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public dynamic this[string name]
        {
            get
            {
                dynamic dynamicObject = fields.Single(f => f.Name == name);
                return dynamicObject.Value;
            }
            set
            {
                dynamic dynamicObject = fields.Single(f => f.Name == name);
                dynamicObject.Value = value;
            }
        }
    }
}