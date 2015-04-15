using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using Plainion;

namespace Plainion.Windows
{
    /// <summary>
    /// Markup extension to merge all Styles given via ResourceKeys into single Style instance.
    /// </summary>
    /// <remarks>
    /// Initial version taken from: http://web.archive.org/web/20101125040337/http://bea.stollnitz.com/blog/?p=384
    /// </remarks>
    [MarkupExtensionReturnType( typeof( Style ) )]
    public class MultiStyleExtension : MarkupExtension
    {
        /// <summary>
        /// Space separated list of resource keys
        /// </summary>
        public string ResourceKeys { get; set; }

        /// <summary>
        /// Returns a style that merges all styles with the keys specified by ResourceKeys property.
        /// </summary>
        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            Contract.RequiresNotNull( ResourceKeys, "ResourceKeys" );

            var resourceKeys = ResourceKeys.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

            Contract.Requires( resourceKeys.Length > 0, "No input resource keys specified." );

            var resultStyle = new Style();

            foreach( var currentResourceKey in resourceKeys )
            {
                var currentStyle = new StaticResourceExtension( currentResourceKey ).ProvideValue( serviceProvider ) as Style;

                Contract.Invariant( currentStyle != null, "Could not find style with resource key " + currentResourceKey + "." );

                AddTo( currentStyle, resultStyle );
            }

            return resultStyle;
        }

        private void AddTo( Style element, Style result )
        {
            if( result.TargetType.IsAssignableFrom( element.TargetType ) )
            {
                result.TargetType = element.TargetType;
            }

            if( element.BasedOn != null )
            {
                AddTo( element.BasedOn, result );
            }

            foreach( var currentSetter in element.Setters )
            {
                result.Setters.Add( currentSetter );
            }

            foreach( var currentTrigger in element.Triggers )
            {
                result.Triggers.Add( currentTrigger );
            }

            // This code is only needed when using DynamicResources.
            foreach( var key in element.Resources.Keys )
            {
                result.Resources[ key ] = element.Resources[ key ];
            }
        }
    }
}
