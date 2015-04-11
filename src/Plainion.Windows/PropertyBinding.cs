using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Data;

namespace Plainion.Windows
{
    public static class PropertyBinding
    {
        private static List<WeakReference<EventHandler<PropertyChangedEventArgs>>> myBindings =
            new List<WeakReference<EventHandler<PropertyChangedEventArgs>>>();

        internal static int RegisteredBindingsCount { get { return myBindings.Count; } }

        /// <summary>
        /// Binds two properties where both declaring types implement INotifyPropertyChanged with BindingMode.TwoWay.
        /// </summary>
        public static void Bind<T>( Expression<Func<T>> source, Expression<Func<T>> target )
        {
            Bind( source, target, BindingMode.TwoWay );
        }

        /// <summary>
        /// Binds two properties where both declaring types implement INotifyPropertyChanged.
        /// Supported BindingModes: OneWay, OneWayToSource, TwoWay
        /// </summary>
        public static void Bind<T>( Expression<Func<T>> source, Expression<Func<T>> target, BindingMode mode )
        {
            Bind( BindableProperty.Create( source ), BindableProperty.Create( target ), mode );
        }

        /// <summary>
        /// Binds two properties where both declaring types implement INotifyPropertyChanged.
        /// Supported BindingModes: OneWay, OneWayToSource, TwoWay
        /// </summary>
        public static void Bind( BindableProperty source, BindableProperty target, BindingMode mode )
        {
            Contract.Requires( mode == BindingMode.OneWay || mode == BindingMode.OneWayToSource || mode == BindingMode.TwoWay,
                "BindingMode not supported: " + mode );

            if( mode == BindingMode.TwoWay || mode == BindingMode.OneWay )
            {
                BindHandler( source, ( s, e ) => target.SetValue( source.GetValue() ) );
            }

            if( mode == BindingMode.TwoWay || mode == BindingMode.OneWayToSource )
            {
                BindHandler( target, ( s, e ) => source.SetValue( target.GetValue() ) );
            }
        }

        private static void BindHandler( BindableProperty source, EventHandler<PropertyChangedEventArgs> handler )
        {
            foreach( var binding in myBindings.ToArray() )
            {
                EventHandler<PropertyChangedEventArgs> deadHandler;
                if( !binding.TryGetTarget( out deadHandler ) )
                {
                    myBindings.Remove( binding );
                }
            }

            myBindings.Add( new WeakReference<EventHandler<PropertyChangedEventArgs>>( handler ) );

            PropertyChangedEventManager.AddHandler( source.Owner, handler, source.PropertyName );
        }
    }
}
