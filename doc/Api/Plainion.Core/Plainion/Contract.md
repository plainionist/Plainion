
# Plainion.Contract

**Namespace:** Plainion

**Assembly:** Plainion.Core

Provides simple but expressive Design-By-Contract convenience APIs.


## Methods

### void Requires(System.Boolean condition,System.String format,System.Object[] args)

Requires that the given condition related to method parameters is true.

#### Exceptions

*System.ArgumentException*
if condition is not met

### void RequiresNotNull(System.Object argument,System.String message)

Requires that the given argument is not null.

#### Exceptions

*System.ArgumentNullException*
if argument is null

### void RequiresNotNullNotEmpty(System.String str,System.String argumentName)

Requires that the given argument is not null and not empty.

#### Exceptions

*System.ArgumentNullException*
if argument is null or empty

### void RequiresNotNullNotWhitespace(System.String str,System.String argumentName)

Requires that the given argument is not null, not empty and not only consists of whitespaces.

#### Exceptions

*System.ArgumentNullException*
if argument is null, empty or consists only of whitespaces

### void RequiresNotNullNotEmpty(System.Collections.Generic.IEnumerable`1[T] collection,System.String argumentName)

### void Invariant(System.Boolean condition,System.String format,System.Object[] args)

Requires that the given condition related to inner state of the class is true.

#### Exceptions

*System.InvalidOperationException*
if condition is not met
