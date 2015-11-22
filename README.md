#ASP.NET Web API
<i>article from [MindFireSolutions](https://www.checkoutall.com/Blog/Index/201409280258134478/ASP-NET-WEB-API-Part-1-Concept-of-Web-API)</i>

#What is Web API?
An application programming interface (API) on web through HTTP/ HTTPS. <br/>
If any API provides features for manipulating resources of the application over web, using HTTP methods like **GET, POST, PUT and DELETE** then we can say the application supports web API and can be consumed by other applications, if permitted.

#Concept
Identifying and accessing existing resources, through a **URI(uniform resource identifier)**.<br/> 
we can consider our resource as noun and action needed to be taken on resource can be considered as verb.<br/> 
These verbs can be of four types: 
1. GET 
2. POST
3. PUT
4. DELETE

####Example:
Consider below URI:
http://www.*_*.com/teams/123

#Q. What this URI tells us?
May be, retrieve details of a Mindfire team, where team id is 123. Here team id is identifier of the resource. Because, which action needed to be taken on above resource, needed to be mentioned as one of the four verbs while calling the URI.

**Details for each action:**

1. To get the details of this team, request will say HTTP GET.
2. To update team details, request will say HTTP PUT.
3. To delete team, request will say HTTP DELETE.
4. To create a new team, the request will say HTTP POST.

**Note**: All the above request will be on same URI (mentioned above), except for `HTTP POST`; In that case, we will need to remove the team id from the URI, as it's for creating a new resource.

Also, as for `POST` and `PUT`, service expect details of a team, which must be served while sending requesting; It can be in JSON or XML format and sent as HTTP request message body. An HTTP service sends responses back in JSON or XML format, similar to the request.

##Common HTTP status code:
HTTP service response contains HTTP status code. For example:
- HTTP status code 200 - OK: If the request is successful.
- HTTP status code 404 - Not found: if the team with identifier 123 does not exist.
**Now if we are aware of WCF, then we may wonder why Web API, why not create services with WCF, with that also we can achieve this.**

##So, now question here is what to choose, ASP.NET Web API or WCF?
#### In Short:
ASP.NET Web API is designed and built by keeping HTTP in mind and WCF is designed primarily with SOAP and WS-*. This model is easy enough to implement, instead of requiring to define interfaces, implementation classes, add many attributes.

So, before hitting the keyboard to start with one option, we should think for some time, in which context we need this resource/service and go ahead with one.

However, Just to clear, ASP.NET Web API is NOT supposed to replace WCF. Now lets compare both way by example:

##WCF Way
http://www.*_*.com/teams/123

```
[ServiceContract]
public interface ITeamService
{
[OperationContract]
[WebGet(UriTemplate = "/teams/{id}")]
Team GetTeam (string id);
}


public class TeamService : ITeamService
{
public Team GetTeam(string id)
{
// We can get team details from database
return new Team() { Id = id, Name = "Web API Team" };
}
}


[DataContract]
public class Team
{
[DataMember] 
public int Id { get; set; } 

[DataMember] 
public string Name { get; set; } 
// We can add more properties as needed

}
```

##WebAPI Way
http://www.*_*.com/teams/123

```
public class TeamController : ApiController
{
public Team Get(string id)
{
// We can get team details from database
return new Team() { Id = id, Name = "Web API Team" };
}
}


public class Team
{
public int Id { get; set; }

public string Name { get; set; }
// We can add more properties as needed
}
```
##Other Cases Where it might Look interesting
Few cases in which ASP.NET Web API as the back end may be a better choice:

**Web applications**: ASP.NET Web API will be a good choice for web applications that mostly driven by AJAX request. Silverlight, Flash applications, also applications built with JavaScript(or with many javascript libraries available) can utilize ASP.NET Web API.

**Native applications**: ASP.NET Web API can also be used as a back end for native applications running on mobile devices, where SOAP is not supported. As Web API does not depend upon client machine OS, So native applications running on platforms other than Windows, such as app running on Mac, can also use ASP.NET Web API.

**Internet of Things (IOT)**: IOT devices with Ethernet controllers or GSM modem, can communicate with ASP.NET WebAPI over web.
