
# Plainion.Text.Wildcard

**Namespace:** Plainion.Text

**Assembly:** Plainion.Core

Represents a wildcard running on the
*See:* N:System.Text.RegularExpressions
engine. Supported wildcard characters are "*" (any sequence of any character) and "?" (any but single character). The pattern must match the whole input.


## Constructors

### Constructor(System.String pattern)

### Constructor(System.String pattern,System.Text.RegularExpressions.RegexOptions options)

Initializes a wildcard with the given search pattern and options.


## Methods

### System.String WildcardToRegex(System.String pattern)

### System.Boolean IsMatch(System.String input,System.String pattern)
