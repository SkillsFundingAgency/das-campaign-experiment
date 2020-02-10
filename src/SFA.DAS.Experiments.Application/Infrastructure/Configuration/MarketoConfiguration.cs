public class MarketoConfiguration
    {
        public virtual string ApiBaseUrl { get; set; }
        public virtual string ApiClientId { get; set; }
        public virtual string ApiClientSecret { get; set; }
        public virtual ExpProgConfiguration ExpProgConfiguration { get; set; }
        public virtual string ApiIdentityBaseUrl { get; set; }

    public virtual ActivityConfig AppStartedConf { get; set; }
    public virtual ActivityConfig AppCompletedConf { get; set; }
    public virtual ActivityConfig CanDetailsUpdatedConf { get; set; }
    public virtual ActivityConfig CanAcountDeleted { get; set; }
    public virtual ActivityConfig CanClosingEmailConf { get; set; }
    public virtual ActivityConfig CanSavedEmailConf { get; set; }

}

    public class ExpProgConfiguration
{
        public virtual string ProgramName { get; set; }

    }

public class ActivityConfig
{
    public virtual string ApiName { get; set; }
    public virtual int ActivityTypeId { get; set; }
    public virtual int EventTypeId { get; set; }
    public virtual string IdField { get; set; }
}