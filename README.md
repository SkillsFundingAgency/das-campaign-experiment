# Campaign (Fire It Up) CMS Integration Functions

|               |               |
| ------------- | ------------- |
|![crest](https://assets.publishing.service.gov.uk/static/images/govuk-crest-bb9e22aff7881b895c2ceb41d9340804451c474b883f09fe1b4026e76456f44b.png) |Campaign (Fire It Up) CMS Integration Functions|
| Build | <img alt="Build Status" src="https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/Future%20Engagement/das-campaign?branchName=fire_it_up_redesign" /> |
| Source  | https://github.com/SkillsFundingAgency/das-campaign-experiment  |

## Functions

These Functions support the cache of content (a Redis instance) that the [Fire it Up](https://github.com/SkillsFundingAgency/das-campaign) site uses to display articles.

There are currently three Functions:

- **ContentRefresh** - Performs a complete refresh of the cache from Contentful.
- **ContentPublish** - Updates / Creates a content item in the cache. This Function is triggered by a Contentful Webhook when an item on Contentful is published.
- **ContentRemove** - Removes a content item from the cache. This Function is triggered by a Contentful Webhook when an item on Contentful is unpublished, archived or deleted.

The connection to Contentful is specified in the json config as follows:

```
"ContentfulOptions": {
    "DeliveryApiKey": "[DeliveryApiKey]",
    "PreviewApiKey": "[PreviewApiKey]]",
    "SpaceId": "8kbr1n52z8s2",
    "UsePreviewApi": false,
    "MaxNumberOfRateLimitRetries": 0,
    "WebhookSecret": "[A secret passed in a header from Contentful]"
}
```

The connection to the Redis cache is specified in the json config as follows:

```
"ConnectionStrings": {
    "Sql": "[Not Relevant]",
    "RedisConnectionString": "[Redis Connection String]"
    "ContentCacheDatabase": "DefaultDatabase=6"
}
```

For **ContentRemove** and **ContentPublish** to function correctly, Contentful's webhooks need to be configured as follows:

- The Function url to POST to must contain the Function Key (obtainable from Azure portal)
- The Webhook must pass a "secret header" named "contentfulWebhookSecret" with a value that matches the "WebhookSecret" in the config above.
- The Payload must be customized as follows:

```
{
  "entryId": "{/payload/sys/id}",
  "entryType": "{/payload/sys/contentType/sys/id}"
}
```

The **ContentRefresh** Function can be executed by either a POST externally, or running in the Azure Portal.  This Function will delete all of the keys in the Redis cache and then repopulate from Contentful.  This function will return a json object describing the status of the operation, eg:

```
{
    "articlesStored": [
        "help-shape-their-career",
        "benefits-apprenticeship",
        "applying-apprenticeship",
        "starting-apprenticeship",
        "upskill",
        "benefits-to-your-organisation",
        "funding-an-apprenticeship-levy-payers",
        "choose-training-provider",
        "the-right-apprenticeship",
        "what-is-an-apprenticeship",
        "hiring-an-apprentice",
        "end-point-assessments",
        "training-your-apprentice",
        "interview",
        "assessment-and-certification"
    ],
    "success": true,
    "exception": null
}
```

## Developer Setup
- [dotnet 2.1](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows) 

