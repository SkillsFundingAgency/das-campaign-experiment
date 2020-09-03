# Campaign (Fire It Up) CMS Integration Functions

|               |               |
| ------------- | ------------- |
|![crest](https://assets.publishing.service.gov.uk/static/images/govuk-crest-bb9e22aff7881b895c2ceb41d9340804451c474b883f09fe1b4026e76456f44b.png) |Campaign (Fire It Up) CMS Integration Functions|
| Build | <img alt="Build Status" src="https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/Future%20Engagement/das-campaign?branchName=fire_it_up_redesign" /> |
| Source  | https://github.com/SkillsFundingAgency/das-campaign-experiment  |

## Introduction

These Functions support the cache of content (a Redis instance) that the [Fire it Up](https://github.com/SkillsFundingAgency/das-campaign) site uses.

There are currently three Functions:

- **ContentRefresh** - Performs a complete refresh of the cache from Contentful.
- **ContentPublish** - Updates / Creates a content item in the cache. This Function is triggered by a Contentful Webhook when an item on Contentful is published.
- **ContentRemove** - Removes a content items from the cache. This Function is triggered by a Contentful Webhook when an item on Contentful is unpublished, archived or deleted.



## Developer Setup
- [dotnet 2.1](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows) 

