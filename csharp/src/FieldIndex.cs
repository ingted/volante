namespace Perst
{
    using System;
    using System.Collections;
	
    /// <summary> Interface of indexed field. 
    /// Index is used to provide fast access to the object by the value of indexed field. 
    /// Objects in the index are stored ordered by the value of indexed field. 
    /// It is possible to select object using exact value of the key or 
    /// select set of objects which key belongs to the specified interval 
    /// (each boundary can be specified or unspecified and can be inclusive or exclusive)
    /// Key should be of scalar, String, DateTime or peristent object type.
    /// </summary>
    public interface FieldIndex : IPersistent, IEnumerable, ICollection
    {
        /// <summary> Access element by key
        /// </summary>
        IPersistent this[object key] 
        {
            get;
            set;
        }       

        /// <summary> Get object by key (exact match)     
        /// </summary>
        /// <param name="key">specified key wrapper. It should match with type of the index and should be inclusive.
        /// </param>
        /// <returns>object with this value of the key or <code>null</code> if key nmot found
        /// </returns>
        /// <exception cref="Perst.StorageError">StorageError(StorageError.ErrorCode.KEY_NOT_UNIQUE) exception if there are more than 
        /// one objects in the index with specified value of the key.
        /// 
        /// </exception>
        IPersistent get(Key key);

        /// <summary> Get object by key (exact match)     
        /// </summary>
        /// <param name="key">specified key value. It should match with type of the index and should be inclusive.
        /// </param>
        /// <returns>object with this value of the key or <code>null</code> if key nmot found
        /// </returns>
        /// <exception cref="Perst.StorageError">StorageError(StorageError.ErrorCode.KEY_NOT_UNIQUE) exception if there are more than 
        /// one objects in the index with specified value of the key.
        /// 
        /// </exception>
        IPersistent get(object key);

        /// <summary> Get objects which key value belongs to the specified range.
        /// Either from boundary, either till boundary either both of them can be <code>null</code>.
        /// In last case the method returns all objects from the index.
        /// </summary>
        /// <param name="from">low boundary. If <code>null</code> then low boundary is not specified.
        /// Low boundary can be inclusive or exclusive. 
        /// </param>
        /// <param name="till">high boundary. If <code>null</code> then high boundary is not specified.
        /// High boundary can be inclusive or exclusive. 
        /// </param>
        /// <returns>array of objects which keys belongs to the specified interval, ordered by key value
        /// 
        /// </returns>
        IPersistent[] get(Key from, Key till);

        /// <summary> Get objects which key value belongs to the specified inclusive range.
        /// Either from boundary, either till boundary either both of them can be <code>null</code>.
        /// In last case the method returns all objects from the index.
        /// </summary>
        /// <param name="from">inclusive low boundary. If <code>null</code> then low boundary is not specified.
        /// </param>
        /// <param name="till">inclusive high boundary. If <code>null</code> then high boundary is not specified.
        /// </param>
        /// <returns>array of objects which keys belongs to the specified interval, ordered by key value
        /// 
        /// </returns>
        IPersistent[] get(object from, object till);

        /// <summary> 
        /// Check if index contains specified object
        /// </summary>
        /// <param name="obj">object to be searched in the index. Object should contain indexed field. 
        /// </param>
        /// <returns><code>true</code> if object is present in the index, <code>false</code> otherwise
        /// </returns>
        bool contains(IPersistent obj);

        /// <summary> Put new object in the index. 
        /// </summary>
        /// <param name="obj">object to be inserted in index. Object should contain indexed field. 
        /// Object can be not yet peristent, in this case its forced to become persistent by assigning OID to it.
        /// </param>
        /// <returns><code>true</code> if object is successfully inserted in the index, 
        /// <code>false</code> if index was declared as unique and there is already object with such value
        /// of the key in the index. 
        /// 
        /// </returns>
        bool put(IPersistent obj);

        /// <summary>
        /// Associate new object with the key specified by object field value. 
        /// If there is already object with such key in the index, 
        /// then it will be removed from the index and new value associated with this key.
        /// </summary>
        /// <param name="obj">object to be inserted in index. Object should contain indexed field. 
        /// Object can be not yet peristent, in this case
        /// its forced to become persistent by assigning OID to it.
        /// </param>
        void set(IPersistent obj);

        /// <summary>
        /// Assign to the integer indexed field unique autoicremented value and 
        /// insert object in the index. 
        /// </summary>
        /// <param name="obj">object to be inserted in index. Object should contain indexed field
        /// of integer (<code>int</code> or <code>long</code>) type.
        /// This field is assigned unique value (which will not be reused while 
        /// this index exists) and object is marked as modified.
        /// Object can be not yet peristent, in this case
        /// its forced to become persistent by assigning OID to it.
        /// </param>
        /// <exception cref="Perst.StorageError"><code>StorageError(StorageError.ErrorCode.INCOMPATIBLE_KEY_TYPE)</code> 
        /// is thrown when indexed field has type other than <code>int</code> or <code>long</code></exception>
        void append(IPersistent obj);


        /// <summary> Remove object from the index
        /// </summary>
        /// <param name="obj">object removed from the index. Object should contain indexed field. 
        /// </param>
        /// <exception cref="Perst.StorageError">StorageError(StorageError.ErrorCode.KEY_NOT_FOUND) exception if there is no such key in the index
        /// 
        /// </exception>
        void  remove(IPersistent obj);

        /// <summary> Get number of objects in the index
        /// </summary>
        /// <returns>number of objects in the index
        /// 
        /// </returns>
        int size();

        /// <summary> Remove all objects from the index
        /// </summary>
        void  clear();

        /// <summary> Get all objects in the index as array orderd by index key
        /// </summary>
        /// <returns>array of specified type contaning objects in the index ordered by key value
        /// 
        /// </returns>
        IPersistent[] ToArray();

        /// <summary> Get all objects in the index as array of specified type orderd by index key
        /// </summary>
        /// <param name="elemType">type of array element</param>
        /// <returns>array of objects in the index ordered by key value
        /// </returns>
        Array ToArray(Type elemType);

        /// <summary>
        /// Get iterator for traversing objects in the index with key belonging to the specified range. 
        /// You should not update/remove or add members to the index during iteration
        /// </summary>
        /// <param name="from">Low boundary. If <code>null</code> then low boundary is not specified.
        /// Low boundary can be inclusive or exclusive.</param>
        /// <param name="till">High boundary. If <code>null</code> then high boundary is not specified.
        /// High boundary can be inclusive or exclusive.</param>
        /// <param name="order"><code>IterationOrder.AscentOrder</code> or <code>IterationOrder.DescentOrder</code></param>
        /// <returns>selection iterator</returns>
        ///
        IEnumerator GetEnumerator(Key from, Key till, IterationOrder order);

        /// <summary>
        /// Get enumerable collection of objects in the index with key belonging to the specified range. 
        /// You should not update/remove or add members to the index during iteration
        /// </summary>
        /// <param name="from">Low boundary. If <code>null</code> then low boundary is not specified.
        /// Low boundary can be inclusive or exclusive.</param>
        /// <param name="till">High boundary. If <code>null</code> then high boundary is not specified.
        /// High boundary can be inclusive or exclusive.</param>
        /// <param name="order"><code>IterationOrder.AscentOrder</code> or <code>IterationOrder.DescentOrder</code></param>
        /// <returns>enumerable collection</returns>
        ///
        IEnumerable range(Key from, Key till, IterationOrder order);

        /// <summary>
        /// Get enumerable collection of objects in the index with key belonging to the specified range. 
        /// You should not update/remove or add members to the index during iteration
        /// </summary>
        /// <param name="from">Inclusive low boundary. If <code>null</code> then low boundary is not specified.</param>
        /// <param name="till">Inclusive high boundary. If <code>null</code> then high boundary is not specified.</param>
        /// <param name="order"><code>IterationOrder.AscentOrder</code> or <code>IterationOrder.DescentOrder</code></param>
        /// <returns>enumerable collection</returns>
        ///
        IEnumerable range(object from, object till, IterationOrder order);

        /// <summary>
        /// Get enumerable ascent ordered collection of objects in the index with key belonging to the specified range. 
        /// You should not update/remove or add members to the index during iteration
        /// </summary>
        /// <param name="from">Inclusive low boundary. If <code>null</code> then low boundary is not specified.</param>
        /// <param name="till">Inclusive high boundary. If <code>null</code> then high boundary is not specified.</param>
        /// <returns>enumerable collection</returns>
        ///
        IEnumerable range(object from, object till);

        /// <summary>
        /// Get iterator for traversing all entries in the index 
        /// You should not update/remove or add members to the index during iteration
        /// </summary>
        /// <returns>entry terator</returns>
        ///
        IDictionaryEnumerator GetDictionaryEnumerator();
        
        /// <summary>
        /// Get iterator for traversing entries in the index with key belonging to the specified range. 
        /// You should not update/remove or add members to the index during iteration
        /// </summary>
        /// <param name="from">Low boundary. If <code>null</code> then low boundary is not specified.
        /// Low boundary can be inclusive or exclusive.</param>
        /// <param name="till">High boundary. If <code>null</code> then high boundary is not specified.
        /// High boundary can be inclusive or exclusive.</param>
        /// <param name="order"><code>AscanrOrder</code> or <code>DescentOrder</code></param>
        /// <returns>selection iterator</returns>
        ///
        IDictionaryEnumerator GetDictionaryEnumerator(Key from, Key till, IterationOrder order);

    }
}