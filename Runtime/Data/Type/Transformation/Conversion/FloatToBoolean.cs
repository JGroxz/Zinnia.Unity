﻿namespace Zinnia.Data.Type.Transformation.Conversion
{
    using Malimbe.PropertySerializationAttribute;
    using Malimbe.XmlDocumentationAttribute;
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Transforms a float value to a boolean based on a defined threshold.
    /// </summary>
    /// <example>
    /// PositiveBounds[0.3, 0.8] -> 0f = false
    /// PositiveBounds[0.3, 0.8] -> 1f = false
    /// PositiveBounds[0.3, 0.8] -> 0.5f = true
    /// </example>
    public class FloatToBoolean : Transformer<float, bool, FloatToBoolean.UnityEvent>
    {
        /// <summary>
        /// Defines the event with the transformed <see cref="bool"/> value.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<bool>
        {
        }

        /// <summary>
        /// The bounds in which the float must be to be considered a positive boolean.
        /// </summary>
        [Serialized]
        [field: DocumentedByXml]
        protected FloatRange PositiveBounds { get; set; } = new FloatRange(0f, 1f);

        /// <summary>
        /// Sets the positive bounds from a given Vector2.
        /// </summary>
        /// <param name="positiveBounds">The new positive bounds.</param>
        public virtual void SetPositiveBounds(Vector2 positiveBounds)
        {
            PositiveBounds = new FloatRange(positiveBounds);
        }

        /// <summary>
        /// Transforms the given input float to the equivalent bool value.
        /// </summary>
        /// <param name="input">The value to transform.</param>
        /// <returns>The transformed value.</returns>
        protected override bool Process(float input)
        {
            return PositiveBounds.Contains(input);
        }
    }
}