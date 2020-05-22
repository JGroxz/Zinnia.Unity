﻿namespace Zinnia.Data.Type.Transformation.Conversion
{
    using Malimbe.PropertySerializationAttribute;
    using Malimbe.XmlDocumentationAttribute;
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Transforms a normalized float value to a float based on a defined range.
    /// </summary>
    /// <example>
    /// Range[0f, 10f] -> 0.5f = 5f
    /// Range[0f, 10f] -> 1f = 10f
    /// Range[1f, 10f] -> 0f = 1f
    /// </example>
    public class NormalizedFloatToFloat : Transformer<float, float, NormalizedFloatToFloat.UnityEvent>
    {
        /// <summary>
        /// Defines the event with the transformed <see cref="float"/> value.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<float>
        {
        }

        /// <summary>
        /// The range in which to consider the minimum and maximum value for de-normalizing.
        /// </summary>
        [Serialized]
        [field: DocumentedByXml]
        protected FloatRange Range { get; set; } = new FloatRange(0f, 1f);

        /// <summary>
        /// Sets the <see cref="Range"/> from a given Vector2.
        /// </summary>
        /// <param name="range">The new range.</param>
        public virtual void SetRange(Vector2 range)
        {
            Range = new FloatRange(range);
        }

        /// <summary>
        /// Transforms the given input normalized float to the equivalent float value.
        /// </summary>
        /// <param name="input">The value to transform.</param>
        /// <returns>The transformed value.</returns>
        protected override float Process(float input)
        {
            return Mathf.Lerp(Range.minimum, Range.maximum, input);
        }
    }
}