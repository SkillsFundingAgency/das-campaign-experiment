using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SFA.DAS.Experiments.Models.Marketo
{
    /// <summary>
    /// CustomActivityTypeAttribute
    /// </summary>
    [DataContract]
    public partial class CustomActivityTypeAttribute :  IEquatable<CustomActivityTypeAttribute>, IValidatableObject
    {
        /// <summary>
        /// Data type of the attribute
        /// </summary>
        /// <value>Data type of the attribute</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DataTypeEnum
        {
            /// <summary>
            /// Enum String for value: string
            /// </summary>
            [EnumMember(Value = "string")]
            String = 1,

            /// <summary>
            /// Enum Boolean for value: boolean
            /// </summary>
            [EnumMember(Value = "boolean")]
            Boolean = 2,

            /// <summary>
            /// Enum Integer for value: integer
            /// </summary>
            [EnumMember(Value = "integer")]
            Integer = 3,

            /// <summary>
            /// Enum Float for value: float
            /// </summary>
            [EnumMember(Value = "float")]
            Float = 4,

            /// <summary>
            /// Enum Link for value: link
            /// </summary>
            [EnumMember(Value = "link")]
            Link = 5,

            /// <summary>
            /// Enum Email for value: email
            /// </summary>
            [EnumMember(Value = "email")]
            Email = 6,

            /// <summary>
            /// Enum Currency for value: currency
            /// </summary>
            [EnumMember(Value = "currency")]
            Currency = 7,

            /// <summary>
            /// Enum Date for value: date
            /// </summary>
            [EnumMember(Value = "date")]
            Date = 8,

            /// <summary>
            /// Enum Datetime for value: datetime
            /// </summary>
            [EnumMember(Value = "datetime")]
            Datetime = 9,

            /// <summary>
            /// Enum Phone for value: phone
            /// </summary>
            [EnumMember(Value = "phone")]
            Phone = 10,

            /// <summary>
            /// Enum Text for value: text
            /// </summary>
            [EnumMember(Value = "text")]
            Text = 11

        }

        /// <summary>
        /// Data type of the attribute
        /// </summary>
        /// <value>Data type of the attribute</value>
        [DataMember(Name="dataType", EmitDefaultValue=false)]
        public DataTypeEnum? DataType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivityTypeAttribute" /> class.
        /// </summary>
        [JsonConstructor]
        protected CustomActivityTypeAttribute() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivityTypeAttribute" /> class.
        /// </summary>
        /// <param name="apiName">API Name of the attribute (required).</param>
        /// <param name="dataType">Data type of the attribute.</param>
        /// <param name="description">Description of the attribute.</param>
        /// <param name="isPrimary">Whether the attribute is the primary attribute of the activity type.  There may only be one primary attribute at a time.</param>
        /// <param name="name">Human-readable display name of the attribute (required).</param>
        public CustomActivityTypeAttribute(string apiName = default(string), DataTypeEnum? dataType = default(DataTypeEnum?), string description = default(string), bool isPrimary = default(bool), string name = default(string))
        {
            // to ensure "apiName" is required (not null)
            if (apiName == null)
            {
                throw new InvalidDataException("apiName is a required property for CustomActivityTypeAttribute and cannot be null");
            }
            else
            {
                this.ApiName = apiName;
            }

            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new InvalidDataException("name is a required property for CustomActivityTypeAttribute and cannot be null");
            }
            else
            {
                this.Name = name;
            }

            this.DataType = dataType;
            this.Description = description;
            this.IsPrimary = isPrimary;
        }
        
        /// <summary>
        /// API Name of the attribute
        /// </summary>
        /// <value>API Name of the attribute</value>
        [DataMember(Name="apiName", EmitDefaultValue=false)]
        public string ApiName { get; set; }

        /// <summary>
        /// Description of the attribute
        /// </summary>
        /// <value>Description of the attribute</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Whether the attribute is the primary attribute of the activity type.  There may only be one primary attribute at a time
        /// </summary>
        /// <value>Whether the attribute is the primary attribute of the activity type.  There may only be one primary attribute at a time</value>
        [DataMember(Name="isPrimary", EmitDefaultValue=false)]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Human-readable display name of the attribute
        /// </summary>
        /// <value>Human-readable display name of the attribute</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CustomActivityTypeAttribute {\n");
            sb.Append("  ApiName: ").Append(ApiName).Append("\n");
            sb.Append("  DataType: ").Append(DataType).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  IsPrimary: ").Append(IsPrimary).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
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
            return this.Equals(input as CustomActivityTypeAttribute);
        }

        /// <summary>
        /// Returns true if CustomActivityTypeAttribute instances are equal
        /// </summary>
        /// <param name="input">Instance of CustomActivityTypeAttribute to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CustomActivityTypeAttribute input)
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
                    this.DataType == input.DataType ||
                    this.DataType.Equals(input.DataType)
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.IsPrimary == input.IsPrimary ||
                    this.IsPrimary.Equals(input.IsPrimary)
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
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
                hashCode = hashCode * 59 + this.DataType.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                hashCode = hashCode * 59 + this.IsPrimary.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
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
