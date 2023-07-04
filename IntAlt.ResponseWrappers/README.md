# IntAlt.ResponseWrappers

This library describes classes used to return responses from endpoints from 
services and microservices.

## Options and Best (?) Practices.

_Best_ is a nebulously defined term. It can be cheapest, easiest, simplist, 
most cpu or power efficient, or all or none of these.

For our purposes, _Best Practices_ are my opinionated views and experience on 
returning values from service and microservice endpoints - especially REST 
services.

## Limitations

One annoying limitation is the need to put a constraint on the generic __T__.
I have no doubt that someone familiar with the Roslyn compiler can explain
why, but C# doesn't like allowing a nullable type that can be either a class
or a struct. Because of this, the default _Response<T>_ is required to be a 
constrained to a class. A separate set of classes, _ResponseStruct<T>_ and 
_DetailedResponseStruct<T>_ are available.

## Service Responses

When developers decide to use HTTP response codes to indicate responses from 
their service endpoints, it can be confusing. If a 404 error indicating that 
the endpoint is not found or does it indicate that the call returned 0 
records, when 1 was expected.

This library was created numerous times throughout my career to deal with 
this situation. It makes the following optionated assumptions:

* HTTP response codes indicate the HTTP protocol success or failure.
    * 2xx response codes indicate that the network responded as expected. 
    Usually, the only HTTP response code will be 200.
    * Response codes are exactly as defined by the RFCs at 
    [HTTP Resources and Specification](https://developer.mozilla.org/en-US/docs/Web/HTTP/Resources_and_specifications)
* The body of the response will have a Code which indicates _service_ 
responses and optionally a result value and / descriptive errors.

## Response Code Values (Recommendations / Opinions)

These are not required or enforced by the library. They are simply best 
practices observed in development.

### Response Code '0'

Response code '0' is a generalized success code. It typically means that the 
request was successful and everything ran through the "_happy path_" as 
expected.

### Positive Response Codes

Positive response codes (greater than 0) also indicate success, although not 
necessarily through the expected path.

For example, an API call to "_change record with identifier __{id}__ so that 
the status is "_PENDING_" could return a positive value indicating that 
nothing was done because the record was ALREADY marked as pending. The result 
after the API call was what the caller wanted, but nothing was actually 
changed.

### Negative Response Codes
Negative value response codes indicate some sort of error. Typically, response
codes from -1 to -99 are generalized, system wide errors, with -1 being 
reserved for "_Something completely unexpected occurred_". Then, other codes 
are grouped by backend services. Examples include:

1. -100 -> -199 Internal Service A
2. -200 -> -299 Internal Service B
3. -300 -> -399 Internal Service C
4. -400 -> -499 Internal Service B

The ranges are irrelevent - experience has shown that grouping can help with 
debugging, since knowing which range belongs to which service can speed up 
debugging a bit. Service B, above, has 2 ranges. This can occur if a service 
is initially given a range, and the number of response codes exceeds that 
range or if the developers wish to specify that no range group will exceed 
100 items.

## Classes

The base class is the ```Response``` class. It is a simple wrapper around a 
response code. The generic class ```Response<T>``` returns a code and, 
optionally, some sort of response value - such as database record. The
```DetailedResponst<T>``` class returns more detailed error information.

### Response class

This is a simple class that just contains a return code. This is useful 
when your response is a simple response code.

### Response<T> and ResponseStruct<T> class

A child class of the Response class. Includes a Result of type T, if 
specified.

### DetailedResponse<T> and DetailedResponseStruct<T> class

Child class of the Response<T> class. In addition to the Result, can contain 
more error detail information. This is useful for responding to a web front 
end where the error messages are human readable.

### Error class

Used by the _DetailedResponse<T>_ and _DetailedResponseStruct<T>_ classes,
the error class is useful to return more detailed information to the caller.
Typically, this will be for returning human readable error information to
a user interface.

## License: MIT
This might change. The intention is as follows:
* The library will ALWAYS be free.
* No support is expressed or implied.
* You are free to use this library however you see fit.
* There are no warrantees expressed or implied. It might not compile. 
It could work perfectly. It could cause the downfall of civilization. 
 Use at your own risk.
* You can fork this library and do whatever you want - even claim the copyright, although with timestamps and such, trying to come after me for royalties, infringements, or anything else won't work.
* You can make requests and even do pull requests with changes. If the changes are warranted and make sense, we will include them. If you make significant changes, you will be included in the copyright if you wish, and you get a portion of all of the proceeds. Just remember, even 100% of zero is still zero.
