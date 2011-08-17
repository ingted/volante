namespace Volante
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Common interface for all links
    /// </summary>
    public interface IGenericLink
    {
        /// <summary> Get number of the linked objects 
        /// </summary>
        /// <returns>the number of related objects
        /// 
        /// </returns>
        int Size();

        /// <summary> Get related object by index without loading it.
        /// Returned object can be used only to get it OID or to compare with other objects using
        /// <code>equals</code> method
        /// </summary>
        /// <param name="i">index of the object in the relation
        /// </param>
        /// <returns>stub representing referenced object
        /// 
        /// </returns>
        IPersistent GetRaw(int i);

        /// <summary>
        /// Set owner object for this link. Owner is persistent object contaning this link.
        /// This method is mostly used by storage itself, but can also used explicityl by programmer if
        /// link component of one persistent object is assigned to component of another persistent object
        /// </summary>
        /// <param name="owner">link owner</param>
        void SetOwner(IPersistent owner);

        /// <summary>
        /// Replace all direct references to linked objects with stubs. 
        /// This method is needed tyo avoid memory exhaustion in case when 
        /// there is a large numebr of objectys in databasse, mutually
        /// refefencing each other (each object can directly or indirectly 
        /// be accessed from other objects).
        /// </summary>
        void Unpin();

        /// <summary>
        /// Replace references to elements with direct references.
        /// It will impove spped of manipulations with links, but it can cause
        /// recursive loading in memory large number of objects and as a result - memory
        /// overflow, because garabge collector will not be able to collect them
        /// </summary>
        void Pin();
    }

    /// <summary> Interface for one-to-many relation. There are two types of relations:
    /// embedded (when references to the relarted objects are stored in lreation
    /// owner object itself) and standalone (when relation is separate object, which contains
    /// the reference to the relation owner and relation members). Both kinds of relations
    /// implements Link interface. Embedded relation is created by Storage.createLink method
    /// and standalone relation is represented by Relation persistent class created by
    /// Storage.createRelation method.
    /// </summary>
    public interface ILink<T> : IList<T>, IGenericLink where T : class,IPersistent
    {
        /// <summary>Number of the linked objects 
        /// </summary>
        int Length
        {
            get;
            set;
        }

        /// <summary> Get related object by index
        /// </summary>
        /// <param name="i">index of the object in the relation
        /// </param>
        /// <returns>referenced object
        /// 
        /// </returns>
        T Get(int i);

        /// <summary> Replace i-th element of the relation
        /// </summary>
        /// <param name="i">index in the relartion
        /// </param>
        /// <param name="obj">object to be included in the relation     
        /// 
        /// </param>
        void Set(int i, T obj);

        /// <summary> Remove object with specified index from the relation
        /// </summary>
        /// <param name="i">index in the relartion
        /// 
        /// </param>
        void Remove(int i);

        /// <summary> Add all elements of the array to the relation
        /// </summary>
        /// <param name="arr">array of obects which should be added to the relation
        /// 
        /// </param>
        void AddAll(T[] arr);

        /// <summary> Add specified elements of the array to the relation
        /// </summary>
        /// <param name="arr">array of obects which should be added to the relation
        /// </param>
        /// <param name="from">index of the first element in the array to be added to the relation
        /// </param>
        /// <param name="length">number of elements in the array to be added in the relation
        /// 
        /// </param>
        void AddAll(T[] arr, int from, int length);

        /// <summary> Add all object members of the other relation to this relation
        /// </summary>
        /// <param name="link">another relation
        /// 
        /// </param>
        void AddAll(ILink<T> link);

        /// <summary> Get relation members as array of objects
        /// </summary>
        /// <returns>created array</returns>
        T[] ToArray();

        /// <summary> 
        /// Return array with relation members. Members are not loaded and 
        /// size of the array can be greater than actual number of members. 
        /// </summary>
        /// <returns>array of object with relation members used in implementation of Link class
        /// </returns>
        Array ToRawArray();


        /// <summary> Get relation members as array with specifed element type
        /// </summary>
        /// <param name="elemType">element type of created array</param>
        /// <returns>created array</returns>
        Array ToArray(Type elemType);

        /// <summary>Check if i-th element of Link is the same as specified obj
        /// </summary>
        /// <param name="i"> element index</param>
        /// <param name="obj">specified object</param>
        /// <returns><code>true</code> if i-th element of Link reference the same object as "obj"</returns>
        bool ContainsElement(int i, T obj);
    }
}