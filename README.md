# DotnetUrlEscapingBug

## EscapeWeb
Contains a small Api Project, with one controller which reflects the ``id`` 
provided by the URL


## EscapeTest
Contains a xunit test to reproduce the possible bug.

Please note, that all tests will UrlEscape the Uri-Parameter.
All three tests should behave the same.


### Get_With_Keys_In_Query
This test has no errors, everything works as expected


### Get_With_Keys_In_Path_TestServer
In this test only the ``a%2Fb`` as id works as expected. 
The Testserver returns a ``404`` for the id ``a/b``. 


### Get_With_Keys_In_Path_Server
In this test only the ``a%2Fb`` as id works as expected. 
The Server returns an escaped value, which is strange, because ``a%2fb`` 
and ``a%252fb`` will return the same value.
