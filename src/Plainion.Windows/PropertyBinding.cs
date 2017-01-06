using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace Plainion.Windows
{
    public static class PropertyBinding
    {
        // weakly tracks all the anonymous event handlers
        // see: http://stackoverflow.com/questions/15225547/weakeventmanager-holds-reference-to-subscriber
        private static ConditionalWeakTable<INotifyPropertyChanged,List<EventHandler<PropertyChangedEventArgs>>> myBindings =
            new ConditionalWeakTable<INotifyPropertyChanged, List<EventHandler<PropertyChangedEventArgs>>>();

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
            List<EventHandler<PropertyChangedEventArgs>> handlers;
            if( !myBindings.TryGetValue( source.Owner, out handlers ) )
            {
                handlers = new List<EventHandler<PropertyChangedEventArgs>>();
                myBindings.Add( source.Owner, handlers );
            }

            handlers.Add( handler );

            PropertyChangedEventManager.AddHandler( source.Owner, handler, source.PropertyName );
        }

        public static void Observe<T>(Expression<Func<T>> source, EventHandler<PropertyChangedEventArgs> handler)
        {
            var prop = BindableProperty.Create(source);
            BindHandler(prop, handler);
        }
    }
}
