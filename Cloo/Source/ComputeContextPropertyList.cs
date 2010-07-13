﻿#region License

/*

Copyright (c) 2009 - 2010 Fatjon Sakiqi

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

*/

#endregion

namespace Cloo
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of <c>ComputeContextProperty</c>s.
    /// </summary>
    /// <remarks> A <c>ComputeContextPropertyList</c> is used to specify the properties of a <c>ComputeContext</c>. </remarks>
    /// <seealso cref="ComputeContext"/>
    /// <seealso cref="ComputeContextProperty"/>
    public class ComputeContextPropertyList: IEnumerable<ComputeContextProperty>
    {
        #region Fields

        private IList<ComputeContextProperty> properties;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <c>ComputeContextPropertyList</c> which contains a single item specifying a <c>ComputePlatform</c>.
        /// </summary>
        /// <param name="platform"> A <c>ComputePlatform</c>. </param>
        public ComputeContextPropertyList(ComputePlatform platform)
        {
            properties = new List<ComputeContextProperty>();
            properties.Add(new ComputeContextProperty(ComputeContextPropertyName.Platform, platform.Handle));
        }

        /// <summary>
        /// Creates a new <c>ComputeContextPropertyList</c> which contains the specified <c>ComputeContextProperty</c>'s.
        /// </summary>
        /// <param name="properties"> An enumerable of <c>ComputeContextProperty</c>'s. </param>
        public ComputeContextPropertyList(IEnumerable<ComputeContextProperty> properties)
        {
            this.properties = new List<ComputeContextProperty>(properties);
        }

        #endregion

        #region Public methods

        public ComputeContextProperty GetByName(ComputeContextPropertyName name)
        {
            foreach (ComputeContextProperty property in properties)
                if (property.Name == name)
                    return property;

            return null;
        }

        #endregion

        #region Internal methods

        internal IntPtr[] ToIntPtrArray()
        {
            IntPtr[] result = new IntPtr[2 * properties.Count + 1];
            for (int i = 0; i < properties.Count; i++)
            {
                result[2 * i] = new IntPtr((int)properties[i].Name);
                result[2 * i + 1] = properties[i].Value;
            }
            result[result.Length - 1] = IntPtr.Zero;
            return result;
        }

        #endregion

        #region IEnumerable<ComputeContextProperty> Members

        public IEnumerator<ComputeContextProperty> GetEnumerator()
        {
            return ((IEnumerable<ComputeContextProperty>)properties).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)properties).GetEnumerator();
        }

        #endregion
    }
}