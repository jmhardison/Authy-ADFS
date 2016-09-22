![](https://lmdev.visualstudio.com/_apis/public/build/definitions/b42a8cc4-b6ef-4e51-bf82-5f8cff165dcf/13/badge)
# Authy-ADFS
## Description

Authy-ADFS is a custom authentication module for Microsoft Active Directory Federation Services (ADFS) 
running on Windows 2012 R2 or higher.

This project provides staff that is leveraging ADFS for single sign-on to leverage the Authy two factor platform
to increase authentication security across all services and applications that are federated.

## DataStores

Authy-ADFS supports mutiple data stores for recording and referencing user data. The configuration file
need updated to reflect one of these on installation.

  * 0 = Azure DocumentDB
  * 1 = Active Directory Attributes
  * 2 = Azure SQL or SQL Server

It is recommended to use either Active Directory Attributes or DocumentDB for simplicity.


##Important Note
This repo is not complete yet. Shortly the full code will be checked in and the release process will
be configured to automatically build and publish a github release of a signed copy for installation.
