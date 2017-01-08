
# Plainion.Collections.Index`2

**Namespace:** Plainion.Collections

**Assembly:** Plainion.Core

Key/Value data structure which realizes an "update on read" which means that when a requested value for a given key does not exist it is created using the provided value creator.


## Constructors

### Constructor(System.Func`2[TKey,TValue] valueCreator)


## Properties

###  Item

###  Keys

###  Values

### System.Int32 Count


## Methods

###  GetEnumerator()

### System.Boolean ContainsKey(TKey key)

### System.Boolean TryGetValue(TKey key,TValue& value)

### void Add(TKey key)

### System.Boolean Remove(TKey key)

### void Clear()
