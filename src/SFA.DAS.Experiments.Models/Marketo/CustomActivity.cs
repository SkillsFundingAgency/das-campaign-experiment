using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Attribute = Marketo.Api.Client.Model.Attribute;

namespace SFA.DAS.Experiments.Models.Marketo
{
    /// <summary>
    /// CustomActivity
    /// </summary>
    [DataContract]
    public partial class CustomActivity :  IEquatable<CustomActivity>, IValidatableObject
    {
        /// <summary>
        /// Status of the operation performed on the record
        /// </summary>
        /// <value>Status of the operation performed on the record</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Created for value: created
            /// </summary>
            [EnumMember(Value = "created")]
            Created = 1,

            /// <summary>
            /// Enum Updated for value: updated
            /// </summary>
            [EnumMember(Value = "updated")]
            Updated = 2,

            /// <summary>
            /// Enum Deleted for value: deleted
            /// </summary>
            [EnumMember(Value = "deleted")]
            Deleted = 3,

            /// <summary>
            /// Enum Skipped for value: skipped
            /// </summary>
            [EnumMember(Value = "skipped")]
            Skipped = 4,

            /// <summary>
            /// Enum Added for value: added
            /// </summary>
            [EnumMember(Value = "added")]
            Added = 5,

            /// <summary>
            /// Enum Removed for value: removed
            /// </summary>
            [EnumMember(Value = "removed")]
            Removed = 6

        }

        /// <summary>
        /// Status of the operation performed on the record
        /// </summary>
        /// <value>Status of the operation performed on the record</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivity" /> class.
        /// </summary>
        [JsonConstructor]
        public CustomActivity() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActivity" /> class.
        /// </summary>
        /// <param name="activityDate">Datetime of the activity (required).</param>
        /// <param name="activityTypeId">Id of the activity type (required).</param>
        /// <param name="apiName">apiName.</param>
        /// <param name="attributes">List of secondary attributes (required).</param>
        /// <param name="errors">Array of errors that occurred if the request was unsuccessful (required).</param>
        /// <param name="id">Integer id of the activity.  For instances which have been migrated to Activity Service, this field may not be present, and should not be treated as unique. (required).</param>
        /// <param name="leadId">Id of the lead associated to the activity (required).</param>
        /// <param name="marketoGUID">Unique id of the activity (128 character string).</param>
        /// <param name="primaryAttributeValue">Value of the primary attribute (required).</param>
        /// <param name="status">Status of the operation performed on the record.</param>
        public CustomActivity(string activityDate = default(string), int activityTypeId = default(int), string apiName = default(string), List<Attribute> attributes = default(List<Attribute>), List<Error> errors = default(List<Error>), long id = default(long), long leadId = default(long), string marketoGUID = default(string), string primaryAttributeValue = default(string), StatusEnum? status = default(StatusEnum?))
        {
            // to ensure "activityDate" is required (not null)
            if (activityDate == null)
            {
                throw new InvalidDataException("activityDate is a required property for CustomActivity and cannot be null");
            }
            else
            {
                this.ActivityDate = activityDate;
            }

            // to ensure "activityTypeId" is required (not null)
            if (activityTypeId == null)
            {
                throw new InvalidDataException("activityTypeId is a required property for CustomActivity and cannot be null");
            }
            else
            {
                this.ActivityTypeId = activityTypeId;
            }

            // to ensure "attributes" is required (not null)
            if (attributes == null)
            {
                throw new InvalidDataException("attributes is a required property for CustomActivity and cannot be null");
            }
            else
            {
                this.Attributes = attributes;
            }

            // to ensure "errors" is required (not null)
            if (errors == null)
            {
                throw new InvalidDataException("errors is a required property for CustomActivity and cannot be null");
            }
            else
            {
                this.Errors = errors;
            }

            // to ensure "id" is required (not null)
            if (id == null)
            {
                throw new InvalidDataException("id is a required property for CustomActivity and cannot be null");
            }
            else
            {
                this.Id = id;
            }

            // to ensure "leadId" is required (not null)
            if (leadId == null)
            {
                throw new InvalidDataException("leadId is a required property for CustomActivity and cannot be null");
            }
            else
            {
                this.LeadId = leadId;
            }

            // to ensure "primaryAttributeValue" is required (not null)
            if (primaryAttributeValue == null)
            {
                throw new InvalidDataException("primaryAttributeValue is a required property for CustomActivity and cannot be null");
            }
            else
            {
                this.PrimaryAttributeValue = primaryAttributeValue;
            }

            this.ApiName = apiName;
            this.MarketoGUID = marketoGUID;
            this.Status = status;
        }
        
        /// <summary>
        /// Datetime of the activity
        /// </summary>
        /// <value>Datetime of the activity</value>
        [DataMember(Name="activityDate", EmitDefaultValue=false)]
        public string ActivityDate { get; set; }

        /// <summary>
        /// Id of the activity type
        /// </summary>
        /// <value>Id of the activity type</value>
        [DataMember(Name="activityTypeId", EmitDefaultValue=false)]
        public int ActivityTypeId { get; set; }

        /// <summary>
        /// Gets or Sets ApiName
        /// </summary>
        [DataMember(Name="apiName", EmitDefaultValue=false)]
        public string ApiName { get; set; }

        /// <summary>
        /// List of secondary attributes
        /// </summary>
        /// <value>List of secondary attributes</value>
        [DataMember(Name="attributes", EmitDefaultValue=false)]
        public List<Attribute> Attributes { get; set; }

        /// <summary>
        /// Array of errors that occurred if the request was unsuccessful
        /// </summary>
        /// <value>Array of errors that occurred if the request was unsuccessful</value>
        [DataMember(Name="errors", EmitDefaultValue=false)]
        public List<Error> Errors { get; set; }

        /// <summary>
        /// Integer id of the activity.  For instances which have been migrated to Activity Service, this field may not be present, and should not be treated as unique.
        /// </summary>
        /// <value>Integer id of the activity.  For instances which have been migrated to Activity Service, this field may not be present, and should not be treated as unique.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long Id { get; set; }

        /// <summary>
        /// Id of the lead associated to the activity
        /// </summary>
        /// <value>Id of the lead associated to the activity</value>
        [DataMember(Name="leadId", EmitDefaultValue=false)]
        public long LeadId { get; set; }

        /// <summary>
        /// Unique id of the activity (128 character string)
        /// </summary>
        /// <value>Unique id of the activity (128 character string)</value>
        [DataMember(Name="marketoGUID", EmitDefaultValue=false)]
        public string MarketoGUID { get; set; }

        /// <summary>
        /// Value of the primary attribute
        /// </summary>
        /// <value>Value of the primary attribute</value>
        [DataMember(Name="primaryAttributeValue", EmitDefaultValue=false)]
        public string PrimaryAttributeValue { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CustomActivity {\n");
            sb.Append("  ActivityDate: ").Append(ActivityDate).Append("\n");
            sb.Append("  ActivityTypeId: ").Append(ActivityTypeId).Append("\n");
            sb.Append("  ApiName: ").Append(ApiName).Append("\n");
            sb.Append("  Attributes: ").Append(Attributes).Append("\n");
            sb.Append("  Errors: ").Append(Errors).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  LeadId: ").Append(LeadId).Append("\n");
            sb.Append("  MarketoGUID: ").Append(MarketoGUID).Append("\n");
            sb.Append("  PrimaryAttributeValue: ").Append(PrimaryAttributeValue).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
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
            return this.Equals(input as CustomActivity);
        }

        /// <summary>
        /// Returns true if CustomActivity instances are equal
        /// </summary>
        /// <param name="input">Instance of CustomActivity to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CustomActivity input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ActivityDate == input.ActivityDate ||
                    (this.ActivityDate != null &&
                    this.ActivityDate.Equals(input.ActivityDate))
                ) && 
                (
                    this.ActivityTypeId == input.ActivityTypeId ||
                    this.ActivityTypeId.Equals(input.ActivityTypeId)
                ) && 
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
                    this.Errors == input.Errors ||
                    this.Errors != null &&
                    input.Errors != null &&
                    this.Errors.SequenceEqual(input.Errors)
                ) && 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.LeadId == input.LeadId ||
                    this.LeadId.Equals(input.LeadId)
                ) && 
                (
                    this.MarketoGUID == input.MarketoGUID ||
                    (this.MarketoGUID != null &&
                    this.MarketoGUID.Equals(input.MarketoGUID))
                ) && 
                (
                    this.PrimaryAttributeValue == input.PrimaryAttributeValue ||
                    (this.PrimaryAttributeValue != null &&
                    this.PrimaryAttributeValue.Equals(input.PrimaryAttributeValue))
                ) && 
                (
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
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
                if (this.ActivityDate != null)
                    hashCode = hashCode * 59 + this.ActivityDate.GetHashCode();
                hashCode = hashCode * 59 + this.ActivityTypeId.GetHashCode();
                if (this.ApiName != null)
                    hashCode = hashCode * 59 + this.ApiName.GetHashCode();
                if (this.Attributes != null)
                    hashCode = hashCode * 59 + this.Attributes.GetHashCode();
                if (this.Errors != null)
                    hashCode = hashCode * 59 + this.Errors.GetHashCode();
                hashCode = hashCode * 59 + this.Id.GetHashCode();
                hashCode = hashCode * 59 + this.LeadId.GetHashCode();
                if (this.MarketoGUID != null)
                    hashCode = hashCode * 59 + this.MarketoGUID.GetHashCode();
                if (this.PrimaryAttributeValue != null)
                    hashCode = hashCode * 59 + this.PrimaryAttributeValue.GetHashCode();
                hashCode = hashCode * 59 + this.Status.GetHashCode();
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
