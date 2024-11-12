# Seq.App.Webex.Room

dotnet publish -c Release -o ./obj/publish

dotnet pack -c Release -o artifacts --no-build

Alert Sample:
{
"EventType": 2716299265,
"Timestamp": "2024-11-12T03:23:35.8030888Z",
"Data": {
"Id": "event-64b9602802c908dd0000000000000000",
"LocalTimestamp": "2024-11-12T03:23:35.8030888+00:00",
"Level": 4,
"MessageTemplate": "Alert condition triggered by {NamespacedAlertTitle}",
"RenderedMessage": "Alert condition triggered by \u0022Health Check Alert\u0022",
"Exception": null,
"Properties": {
"NamespacedAlertTitle": "Health Check Alert",
"Alert": {
"Id": "alert-131",
"Title": "Health Check Alert",
"Url": "http://localhost:80/#/alerts/alert-131",
"OwnerUsername": null,
"SignalExpressionDescription": "Errors",
"Query": "select count(*) as count from stream group by time(1m) having count \u003E 0 limit 100 for background",
"HavingClause": "count \u003E 0",
"TimeGrouping": "1 minute"
},
"Source": {
"RangeStart": "2024-11-12T03:22:05.8030888Z",
"RangeEnd": "2024-11-12T03:23:05.8030888Z",
"ResultsUrl": "http://localhost:80/#/events?q=select%20count%28%2A%29%20as%20count%20from%20stream%20group%20by%20time%281m%29%20having%20count%20%3E%200%20limit%20100%20for%20background\u0026from=2024-11-12T03:22:05.8030888Z\u0026to=2024-11-12T03:23:05.8030888Z\u0026intersect=signal-m33301",
"Results": [
[
"time",
"count"
],
[
"2024-11-12T03:22:05.8030888Z",
3
]
],
"ContributingEvents": [
[
"id",
"timestamp",
"message"
],
[
"event-3ff89c8402c908dde25e040000000000",
"2024-11-12T03:22:34.1418116Z",
"Health check hangfire-check with status Unhealthy completed after 4.2746ms with message \u00275 server registered, expected minimum 6\u0027"
]
]
},
"SuppressedUntil": "2024-11-12T03:24:35.8030888Z",
"Failures": null
}
},
"Id": "event-64b9602802c908dd0000000000000000",
"TimestampUtc": "2024-11-12T03:23:35.8030888Z"
}

Event Sample:
{
"EventType": 3738132214,
"Timestamp": "2024-11-12T03:25:34.2198653Z",
"Data": {
"Id": "event-ab4e577d02c908dd015f040000000000",
"LocalTimestamp": "2024-11-12T03:25:34.2198653+00:00",
"Level": 4,
"MessageTemplate": "Health check {Method} {TargetUrl} {Outcome} with status code {StatusCode} in {Elapsed:0.000} ms",
"RenderedMessage": "Health check \u0022GET\u0022 \u0022http://host.docker.internal:9991/health\u0022 \u0022failed\u0022 with status code 503 in 53.544 ms",
"Exception": null,
"Properties": {
"@st": "2024-11-12T03:25:34.1663216Z",
"@sk": "Internal",
"Application": "Seq.Input.HealthCheck",
"ContentLength": 732,
"ContentType": "application/json",
"Data": "Unhealthy",
"Elapsed": 53.5437,
"HealthCheckTitle": "Health Check Trigger",
"Method": "GET",
"Outcome": "failed",
"StatusCode": 503,
"TargetUrl": "http://host.docker.internal:9991/health"
}
},
"Id": "event-ab4e577d02c908dd015f040000000000",
"TimestampUtc": "2024-11-12T03:25:34.2198653Z"
}