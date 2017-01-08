
# Plainion.Serialization.SerializableBindableBase

**Namespace:** Plainion.Serialization

**Assembly:** Plainion.Core

Serializable base class with INotifyPropertyChanged support.

## Remarks

BindableBase from Prism cannot be used with DataContractSerializer because it does not have DataContractAttribute applied which is mandatory. (Same for BinaryFormatter)


## Constructors

### Constructor()


## Events

### System.ComponentModel.PropertyChangedEventHandler PropertyChanged


## Methods

### System.Boolean SetProperty(T& storage,T value,System.String propertyName)
