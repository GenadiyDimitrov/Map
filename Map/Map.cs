
public enum MapDirection
{
    Keys,
    Values,
}
public class Indexer<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private Dictionary<TKey, TValue> _dictionary;
    public Indexer(Dictionary<TKey, TValue> dictionary)
    {
        _dictionary = dictionary;
    }
    public TValue this[TKey index]
    {
        get { return _dictionary[index]; }
        set { _dictionary[index] = value; }
    }
    public bool TryGetValue(TKey key, out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }


    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class Map<TKey, TValue>
    where TKey : notnull
    where TValue : notnull
{
    #region Fields
    private Dictionary<TKey, TValue> _keys;
    private Dictionary<TValue, TKey> _values;
    #endregion
    #region Properties
    public Indexer<TKey, TValue> Keys { get; private set; }
    public Indexer<TValue, TKey> Values { get; private set; }
    #endregion
    #region Constructor
    public Map()
    {
        _keys = new Dictionary<TKey, TValue>();
        _values = new Dictionary<TValue, TKey>();
        this.Keys = new Indexer<TKey, TValue>(_keys);
        this.Values = new Indexer<TValue, TKey>(_values);
    }
    #endregion

    #region Add
    /// <summary>
    /// Add to collection
    /// </summary>
    /// <returns>true if key nor value are mapped already and the element is successfully inserted; otherwise, false</returns>
    public bool Add(TKey key, TValue value)
    {
        if (!_keys.ContainsKey(key) && !_values.ContainsKey(value))
        {
            _keys.Add(key, value);
            _values.Add(value, key);
            return true;
        }
        return false;
    }
    #endregion
    #region Remove
    /// <summary>
    /// Remove from collection
    /// </summary>
    /// <param name="key">key of the collection</param>
    /// <param name="collection">use to specify keys or values; default will remove from Keys</param>
    /// <returns><see langword="true"/> if the element is successfully found and removed; otherwise, <see langword="false"/></returns>
    /// <exception cref="ArgumentNullException">if key is null</exception>
    /// <exception cref="ArgumentException">if key don't match typeof(TKey/TValue)</exception>
    public bool Remove(object key, MapDirection collection = MapDirection.Keys)
    {
        if (key == null) throw new ArgumentNullException($"{(collection == MapDirection.Keys ? "Key" : "Value")} must not be null");
        return collection == MapDirection.Values ? RemoveValue(key) : RemoveKey(key);
    }
    private bool RemoveKey(object key)
    {
        if (key is TKey forwardKey)
        {
            return RemoveKey(forwardKey);
        }
        else
        {
            throw new ArgumentException($"Key must match type {typeof(TKey)}");
        }
    }
    private bool RemoveValue(object key)
    {
        if (key is TValue reverseKey)
        {
            return RemoveValue(reverseKey);
        }
        else
        {
            throw new ArgumentException($"Value must match type {typeof(TValue)}");
        }
    }

    /// <summary>
    /// Remove from collection
    /// </summary>
    /// <param name="key">key of the collection</param>
    /// <returns><see langword="true"/> if the element is successfully found and removed; otherwise, <see langword="false"/></returns>
    /// <exception cref="ArgumentNullException">if key is null</exception>
    /// <exception cref="ArgumentException">if key don't match typeof(TKey/TValue)</exception>
    public bool RemoveKey(TKey key)
    {
        if (_keys.TryGetValue(key, out TValue value))
        {
            _values.Remove(value);
        }
        return _keys.Remove(key);
    }
    /// <summary>
    /// Remove from collection
    /// </summary>
    /// <param name="key">key of the collection</param>
    /// <returns><see langword="true"/> if the element is successfully found and removed; otherwise, <see langword="false"/></returns>
    /// <exception cref="ArgumentNullException">if key is null</exception>
    /// <exception cref="ArgumentException">if key don't match typeof(TKey/TValue)</exception>
    public bool RemoveValue(TValue value)
    {
        if (_values.TryGetValue(value, out TKey key))
        {
            _keys.Remove(key);
        }
        return _values.Remove(value);
    }
    #endregion
    #region Contains
    public bool ContainsKey(TKey key)
    {
        return _keys.ContainsKey(key);
    }
    public bool ContainsValue(TValue value)
    {
        return _values.ContainsKey(value);
    }
    #endregion
    #region Try get key/value
    public bool TryGetValue(TKey key, out TValue value)
    {
        return _keys.TryGetValue(key, out value);
    }
    public bool TryGetKey(TValue value, out TKey key)
    {
        return _values.TryGetValue(value, out key);
    }
    #endregion


}