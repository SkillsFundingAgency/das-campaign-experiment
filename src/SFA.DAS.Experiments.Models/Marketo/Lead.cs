using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace SFA.DAS.Experiments.Models.Marketo
{
    /// <summary>
    /// Lead record.  Always contains id, but may have any number of other fields, depending on the fields available in the target instance.
    /// </summary>
    [DataContract]
    public partial class Lead :  IEquatable<Lead>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Lead" /> class.
        /// </summary>
        /// <param name="id">Unique integer id of a lead record.</param>
        /// <param name="membership">membership.</param>
        /// <param name="reason">reason.</param>
        /// <param name="status">Status of the operation performed on the record.</param>
        public Lead(int id = default(int))
        {
            this.Id = id;
        }
        
        /// <summary>
        /// Unique integer id of a lead record
        /// </summary>
        /// <value>Unique integer id of a lead record</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int Id { get; set; }

        /// <summary>
        /// Unique integer id of a lead record
        /// </summary>
        /// <value>Unique integer id of a lead record</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets Membership
        /// </summary>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets Membership
        /// </summary>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets Reason
        /// </summary>
        [DataMember(Name = "reasons", EmitDefaultValue = false)]
        public List<Reason> Reason { get; set; }

        /// <summary>
        /// Status of the operation performed on the record
        /// </summary>
        /// <value>Status of the operation performed on the record</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Lead {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
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
            return this.Equals(input as Lead);
        }

        /// <summary>
        /// Returns true if Lead instances are equal
        /// </summary>
        /// <param name="input">Instance of Lead to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Lead input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.FirstName == input.FirstName ||
                    (this.FirstName != null &&
                    this.FirstName.Equals(input.FirstName))
                ) && 
                (
                    this.LastName == input.LastName ||
                    (this.LastName != null &&
                    this.LastName.Equals(input.LastName))
                ) && 
                (
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
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
                hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.FirstName != null)
                    hashCode = hashCode * 59 + this.FirstName.GetHashCode();
                if (this.LastName != null)
                    hashCode = hashCode * 59 + this.LastName.GetHashCode();
                if (this.Email != null)
                    hashCode = hashCode * 59 + this.Email.GetHashCode();
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
