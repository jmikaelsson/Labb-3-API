# Labb 3 - API
This assignment is part of the Webbapplikationer i C#, ASP.NET course at Edugrade.

## Examples for endpoint calls:
### Get all people
GET/people (no parameters)

### Get a specific person by ID, including all their interests and links:
GET/people/{id} (no parameter other than ID)

### Add new person:
POST/people 
```
{ "id": 0,
"firstName": "string", 
"lastName": "string", 
"phoneNumber": 
"833)431499", 
"email": "user@example.com", 
"age": 0 }
```

### Add new interest:
POST/interests 
```
{ "id": 0, 
"title": "string", 
"description": "string" }
```

### Connect a person to a new interest:
POST/personwithinterest 
```
{ "id": 0, 
"personId": {id}, 
"interestId": {id} }
```

### Add a link to a person - interest connection:
POST/links
```
{ "linkId": 0, 
"url": "string", 
"personWithInterestsId": {id} }
```
