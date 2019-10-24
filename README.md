# AQ Permits Online Technical Documentation

AQ Permits Online processing consists of 
  1. Move permit files from staging to permanent folder and update the files info in the database.
      * SSIS package and Stored Procedures
  2. Add file download links to [Permit Detail Page.](https://www.deq.state.or.us/msd/profilerReports/traacs.asp?id=01-0005-BS-01)
      * Classic ASP and Stored Procedure
  3. New [Search Filter](https://www.deq.state.or.us/aq/aqpermitsonline/SearchFilter.asp) and [Search Results](https://www.deq.state.or.us/aq/aqpermitsonline/SearchResult.asp?sourcenumber=&sourcename=&streetaddress=&city=&zip=&county=&deqregion=&permitnumber=&permittype=&documenttype=&yearissued=&currentdocumentsonly=1)  pages.
      * Classic ASP and Stored Procedure
      
## Getting Started

Update Classic ASP pages:
[Permit Detail Page](http://deqwebdev:333/msd/profilerReports/TRAACs.asp?id=01-0005-BS-01), copy the TRAACs.asp file to \\deqwebdev\DEQOnlineDEV\msd\ProfilerReports\TRAACs.asp
[Search Filter Page](http://deqwebdev:333/aq/aqpermitsonline/SearchFilter.asp), copy the SearchFilter.asp file to \\deqwebdev\DEQOnlineDEV\aq\aqpermitsonline.asp
[Search Result Page](http://deqwebdev:333/aq/aqpermitsonline/SearchResult.asp?sourcenumber=&sourcename=&streetaddress=&city=&zip=&county=&deqregion=&permitnumber=&permittype=&documenttype=&yearissued=&currentdocumentsonly=1), copy the SearchResults.asp file to \\deqwebdev\DEQOnlineDEV\aq\aqpermitsonline\SearchResult.asp
### Table of contents

- [Application URLs](#applicationurls)
- [Technologies](#technologies)
- [Scanned Permit Files Processing](#scanfilesprocessing)

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
Give examples
```

### Installing

A step by step series of examples that tell you how to get a development env running

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
