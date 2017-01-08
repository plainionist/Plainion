
# Plainion.StringExtensions

**Namespace:** Plainion

**Assembly:** Plainion.Core

Extension methods to System.String


## Methods

### System.Boolean IsTrue(System.String value)

Returns true if the string contains a "true value"

#### Return value

true if the given string equals (ignoring case): "y", "yes", "on" or "true"

### System.Boolean Contains(System.String source,System.String value,System.StringComparison comparison)

String.Contains() implementation supporting StringComparison which allows "contains" checks with ignoring case.

### System.String RemoveAll(System.String str,System.Func`2[System.Char,System.Boolean] predicate)
