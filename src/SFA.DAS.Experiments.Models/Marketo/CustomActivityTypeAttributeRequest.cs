using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace SFA.DAS.Experiments.Models.Marketo
{
    /// <summary>
    /// CustomActivityTypeAttributeRequest
    /// </summary>
    [DataContract]
    public partial class CustomActivityTypeAttributeRequest :  IEquatable<CustomActivityTypeAttributeRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivityTypeAttributeRequest" /> class.
        /// </summary>
        /// <param name="attributes">List of attributes to add to the activity type.</param>
        public CustomActivityTypeAttributeRequest(List<CustomActivityTypeAttribute> attributes = default(List<CustomActivityTypeAttribute>))
        {
            this.Attributes = attributes;
        }
        
        /// <summary>
        /// List of attributes to add to the activity type
        /// </summary>
        /// <value>List of attributes to add to the activity type</value>
        [DataMember(Name="attributes", EmitDefaultValue=false)]
        public List<CustomActivityTypeAttribute> Attributes { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CustomActivityTypeAttributeRequest {\n");
            sb.Append("  Attributes: ").Append(Attributes).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as CustomActivityTypeAttributeRequest);
        }

        /// <summary>
        /// Returns true if CustomActivityTypeAttributeRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of CustomActivityTypeAttributeRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CustomActivityTypeAttributeRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Attributes == input.Attributes ||
                    this.Attributes != null &&
                    input.Attributes != null &&
                    this.Attributes.SequenceEqual(input.Attributes)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Attributes != null)
                    hashCode = hashCode * 59 + this.Attributes.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
