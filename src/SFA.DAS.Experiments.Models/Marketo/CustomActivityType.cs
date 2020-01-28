using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SFA.DAS.Experiments.Models.Marketo
{
    /// <summary>
    /// CustomActivityType
    /// </summary>
    [DataContract]
    public partial class CustomActivityType :  IEquatable<CustomActivityType>, IValidatableObject
    {
        /// <summary>
        /// State of the activity type
        /// </summary>
        /// <value>State of the activity type</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Draft for value: draft
            /// </summary>
            [EnumMember(Value = "draft")]
            Draft = 1,

            /// <summary>
            /// Enum Approved for value: approved
            /// </summary>
            [EnumMember(Value = "approved")]
            Approved = 2,

            /// <summary>
            /// Enum Deleted for value: deleted
            /// </summary>
            [EnumMember(Value = "deleted")]
            Deleted = 3,

            /// <summary>
            /// Enum Approvedwithdraft for value: approved with draft
            /// </summary>
            [EnumMember(Value = "approved with draft")]
            Approvedwithdraft = 4

        }

        /// <summary>
        /// State of the activity type
        /// </summary>
        /// <value>State of the activity type</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivityType" /> class.
        /// </summary>
        /// <param name="apiName">API Name of the type.  The API name must be unique and alphanumeric, containing at least one letter.  It is highly recommended to prepend a unique namespace of up to sixteen characters to the API name.  Required on creation.</param>
        /// <param name="attributes">List of attributes for the activity type.  May only be added or update through Create or Update Custom Activity Type Attributes.</param>
        /// <param name="createdAt">Datetime when the activity type was created.</param>
        /// <param name="description">Description of the activity type.</param>
        /// <param name="filterName">Human-readable name for the associated filter of the activity type.  Required on creation.</param>
        /// <param name="id">id.</param>
        /// <param name="name">Human-readable display name of the type.  Required on creation.</param>
        /// <param name="primaryAttribute">primaryAttribute.</param>
        /// <param name="status">State of the activity type.</param>
        /// <param name="triggerName">Human-readable name for the associated trigger of the activity type.  Required on creation.</param>
        /// <param name="updatedAt">Datetime when the activity type was most recently updated.</param>
        public CustomActivityType(string apiName = default(string), List<CustomActivityTypeAttribute> attributes = default(List<CustomActivityTypeAttribute>), string createdAt = default(string), string description = default(string), string filterName = default(string), int id = default(int), string name = default(string), CustomActivityTypeAttribute primaryAttribute = default(CustomActivityTypeAttribute), StatusEnum? status = default(StatusEnum?), string triggerName = default(string), string updatedAt = default(string))
        {
            this.ApiName = apiName;
            this.Attributes = attributes;
            this.CreatedAt = createdAt;
            this.Description = description;
            this.FilterName = filterName;
            this.Id = id;
            this.Name = name;
            this.PrimaryAttribute = primaryAttribute;
            this.Status = status;
            this.TriggerName = triggerName;
            this.UpdatedAt = updatedAt;
        }
        
        /// <summary>
        /// API Name of the type.  The API name must be unique and alphanumeric, containing at least one letter.  It is highly recommended to prepend a unique namespace of up to sixteen characters to the API name.  Required on creation
        /// </summary>
        /// <value>API Name of the type.  The API name must be unique and alphanumeric, containing at least one letter.  It is highly recommended to prepend a unique namespace of up to sixteen characters to the API name.  Required on creation</value>
        [DataMember(Name="apiName", EmitDefaultValue=false)]
        public string ApiName { get; set; }

        /// <summary>
        /// List of attributes for the activity type.  May only be added or update through Create or Update Custom Activity Type Attributes
        /// </summary>
        /// <value>List of attributes for the activity type.  May only be added or update through Create or Update Custom Activity Type Attributes</value>
        [DataMember(Name="attributes", EmitDefaultValue=false)]
        public List<CustomActivityTypeAttribute> Attributes { get; set; }

        /// <summary>
        /// Datetime when the activity type was created
        /// </summary>
        /// <value>Datetime when the activity type was created</value>
        [DataMember(Name="createdAt", EmitDefaultValue=false)]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Description of the activity type
        /// </summary>
        /// <value>Description of the activity type</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Human-readable name for the associated filter of the activity type.  Required on creation
        /// </summary>
        /// <value>Human-readable name for the associated filter of the activity type.  Required on creation</value>
        [DataMember(Name="filterName", EmitDefaultValue=false)]
        public string FilterName { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int Id { get; set; }

        /// <summary>
        /// Human-readable display name of the type.  Required on creation
        /// </summary>
        /// <value>Human-readable display name of the type.  Required on creation</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryAttribute
        /// </summary>
        [DataMember(Name="primaryAttribute", EmitDefaultValue=false)]
        public CustomActivityTypeAttribute PrimaryAttribute { get; set; }

        /// <summary>
        /// Human-readable name for the associated trigger of the activity type.  Required on creation
        /// </summary>
        /// <value>Human-readable name for the associated trigger of the activity type.  Required on creation</value>
        [DataMember(Name="triggerName", EmitDefaultValue=false)]
        public string TriggerName { get; set; }

        /// <summary>
        /// Datetime when the activity type was most recently updated
        /// </summary>
        /// <value>Datetime when the activity type was most recently updated</value>
        [DataMember(Name="updatedAt", EmitDefaultValue=false)]
        public string UpdatedAt { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CustomActivityType {\n");
            sb.Append("  ApiName: ").Append(ApiName).Append("\n");
            sb.Append("  Attributes: ").Append(Attributes).Append("\n");
            sb.Append("  CreatedAt: ").Append(CreatedAt).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  FilterName: ").Append(FilterName).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  PrimaryAttribute: ").Append(PrimaryAttribute).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  TriggerName: ").Append(TriggerName).Append("\n");
            sb.Append("  UpdatedAt: ").Append(UpdatedAt).Append("\n");
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
            return this.Equals(input as CustomActivityType);
        }

        /// <summary>
        /// Returns true if CustomActivityType instances are equal
        /// </summary>
        /// <param name="input">Instance of CustomActivityType to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CustomActivityType input)
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
                    this.Attributes == input.Attributes ||
                    this.Attributes != null &&
                    input.Attributes != null &&
                    this.Attributes.SequenceEqual(input.Attributes)
                ) && 
                (
                    this.CreatedAt == input.CreatedAt ||
                    (this.CreatedAt != null &&
                    this.CreatedAt.Equals(input.CreatedAt))
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
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
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
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
                ) && 
                (
                    this.TriggerName == input.TriggerName ||
                    (this.TriggerName != null &&
                    this.TriggerName.Equals(input.TriggerName))
                ) && 
                (
                    this.UpdatedAt == input.UpdatedAt ||
                    (this.UpdatedAt != null &&
                    this.UpdatedAt.Equals(input.UpdatedAt))
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
                if (this.Attributes != null)
                    hashCode = hashCode * 59 + this.Attributes.GetHashCode();
                if (this.CreatedAt != null)
                    hashCode = hashCode * 59 + this.CreatedAt.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.FilterName != null)
                    hashCode = hashCode * 59 + this.FilterName.GetHashCode();
                hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.PrimaryAttribute != null)
                    hashCode = hashCode * 59 + this.PrimaryAttribute.GetHashCode();
                hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.TriggerName != null)
                    hashCode = hashCode * 59 + this.TriggerName.GetHashCode();
                if (this.UpdatedAt != null)
                    hashCode = hashCode * 59 + this.UpdatedAt.GetHashCode();
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
