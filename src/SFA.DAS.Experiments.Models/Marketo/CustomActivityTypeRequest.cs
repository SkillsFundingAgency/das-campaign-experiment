using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace SFA.DAS.Experiments.Models.Marketo
{
    /// <summary>
    /// CustomActivityTypeRequest
    /// </summary>
    [DataContract]
    public partial class CustomActivityTypeRequest :  IEquatable<CustomActivityTypeRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivityTypeRequest" /> class.
        /// </summary>
        [JsonConstructor]
        protected CustomActivityTypeRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivityTypeRequest" /> class.
        /// </summary>
        /// <param name="apiName">apiName (required).</param>
        /// <param name="description">description.</param>
        /// <param name="filterName">Human-readable name of the associated filter (required).</param>
        /// <param name="name">Human-readable display name of the activity type (required).</param>
        /// <param name="primaryAttribute">primaryAttribute (required).</param>
        /// <param name="triggerName">Human-readable name of the associated trigger (required).</param>
        public CustomActivityTypeRequest(string apiName = default(string), string description = default(string), string filterName = default(string), string name = default(string), CustomActivityTypeAttribute primaryAttribute = default(CustomActivityTypeAttribute), string triggerName = default(string))
        {
            // to ensure "apiName" is required (not null)
            if (apiName == null)
            {
                throw new InvalidDataException("apiName is a required property for CustomActivityTypeRequest and cannot be null");
            }
            else
            {
                this.ApiName = apiName;
            }

            // to ensure "filterName" is required (not null)
            if (filterName == null)
            {
                throw new InvalidDataException("filterName is a required property for CustomActivityTypeRequest and cannot be null");
            }
            else
            {
                this.FilterName = filterName;
            }

            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new InvalidDataException("name is a required property for CustomActivityTypeRequest and cannot be null");
            }
            else
            {
                this.Name = name;
            }

            // to ensure "primaryAttribute" is required (not null)
            if (primaryAttribute == null)
            {
                throw new InvalidDataException("primaryAttribute is a required property for CustomActivityTypeRequest and cannot be null");
            }
            else
            {
                this.PrimaryAttribute = primaryAttribute;
            }

            // to ensure "triggerName" is required (not null)
            if (triggerName == null)
            {
                throw new InvalidDataException("triggerName is a required property for CustomActivityTypeRequest and cannot be null");
            }
            else
            {
                this.TriggerName = triggerName;
            }

            this.Description = description;
        }
        
        /// <summary>
        /// Gets or Sets ApiName
        /// </summary>
        [DataMember(Name="apiName", EmitDefaultValue=false)]
        public string ApiName { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Human-readable name of the associated filter
        /// </summary>
        /// <value>Human-readable name of the associated filter</value>
        [DataMember(Name="filterName", EmitDefaultValue=false)]
        public string FilterName { get; set; }

        /// <summary>
        /// Human-readable display name of the activity type
        /// </summary>
        /// <value>Human-readable display name of the activity type</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryAttribute
        /// </summary>
        [DataMember(Name="primaryAttribute", EmitDefaultValue=false)]
        public CustomActivityTypeAttribute PrimaryAttribute { get; set; }

        /// <summary>
        /// Human-readable name of the associated trigger
        /// </summary>
        /// <value>Human-readable name of the associated trigger</value>
        [DataMember(Name="triggerName", EmitDefaultValue=false)]
        public string TriggerName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CustomActivityTypeRequest {\n");
            sb.Append("  ApiName: ").Append(ApiName).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  FilterName: ").Append(FilterName).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  PrimaryAttribute: ").Append(PrimaryAttribute).Append("\n");
            sb.Append("  TriggerName: ").Append(TriggerName).Append("\n");
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
            return this.Equals(input as CustomActivityTypeRequest);
        }

        /// <summary>
        /// Returns true if CustomActivityTypeRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of CustomActivityTypeRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CustomActivityTypeRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ApiName == input.ApiName ||
                    (this.ApiName != null &&
                    this.ApiName.Equals(input.ApiName))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.FilterName == input.FilterName ||
                    (this.FilterName != null &&
                    this.FilterName.Equals(input.FilterName))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.PrimaryAttribute == input.PrimaryAttribute ||
                    (this.PrimaryAttribute != null &&
                    this.PrimaryAttribute.Equals(input.PrimaryAttribute))
                ) && 
                (
                    this.TriggerName == input.TriggerName ||
                    (this.TriggerName != null &&
                    this.TriggerName.Equals(input.TriggerName))
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
                if (this.ApiName != null)
                    hashCode = hashCode * 59 + this.ApiName.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.FilterName != null)
                    hashCode = hashCode * 59 + this.FilterName.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.PrimaryAttribute != null)
                    hashCode = hashCode * 59 + this.PrimaryAttribute.GetHashCode();
                if (this.TriggerName != null)
                    hashCode = hashCode * 59 + this.TriggerName.GetHashCode();
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
