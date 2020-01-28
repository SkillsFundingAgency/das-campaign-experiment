using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace SFA.DAS.Experiments.Models.Marketo
{
    /// <summary>
    /// CustomActivityRequest
    /// </summary>
    [DataContract]
    public partial class CustomActivityRequest :  IEquatable<CustomActivityRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivityRequest" /> class.
        /// </summary>
        [JsonConstructor]
        protected CustomActivityRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivityRequest" /> class.
        /// </summary>
        /// <param name="input">List of custom activities to insert (required).</param>
        public CustomActivityRequest(List<CustomActivity> input = default(List<CustomActivity>))
        {
            // to ensure "input" is required (not null)
            if (input == null)
            {
                throw new InvalidDataException("input is a required property for CustomActivityRequest and cannot be null");
            }
            else
            {
                this.Input = input;
            }

        }
        
        /// <summary>
        /// List of custom activities to insert
        /// </summary>
        /// <value>List of custom activities to insert</value>
        [DataMember(Name="input", EmitDefaultValue=false)]
        public List<CustomActivity> Input { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CustomActivityRequest {\n");
            sb.Append("  Input: ").Append(Input).Append("\n");
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
            return this.Equals(input as CustomActivityRequest);
        }

        /// <summary>
        /// Returns true if CustomActivityRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of CustomActivityRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CustomActivityRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Input == input.Input ||
                    this.Input != null &&
                    input.Input != null &&
                    this.Input.SequenceEqual(input.Input)
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
                if (this.Input != null)
                    hashCode = hashCode * 59 + this.Input.GetHashCode();
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
