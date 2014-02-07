Project distributed under the BSD license.

MvcAbstractGrid 0.1
===============

MvcAbstractGrid generates an HTML table from a collection of objects.

The problem:

We have a data entity class, e.g.:

  class Machine
	{
		public int Id { get; set; }
		public String Platform { get; set; }
		public String OperatingSystem { get; set; }
	}

We have the following entities:
new Machine {Id = 1, OperatingSystem = "Linux",Platform = "x32"},nenew Machine {Id = 2, OperatingSystem = "Windows",Platform = "x86"}


We want to display a list of entities, but show only some of the properties
and transform others (e.g. show icons instead of OS names).

Identyfikator       |  System operacyjny
=============================================
1                   | <img src="Linux.jpg"/>
2                   | <img src="Windows.jpg"/>

We can't change anything in the Machine class as it's EF Code First generated.

The idea:

  using a custom function to pick the data we want to display
                    |
Entity object list ---> ViewModel object list ---> HTML table
                                               |
                                  automatically generated
                                     by MvcAbstractGrid

In practice, you only need to define a ViewModel class with a constructor
that takes an Entity object, and maps the properties you want.

For each property, you can define the following attributes that define the behaviour
of the generator:
- [DisplayName("name")] - defines the column header
- [DisplayOrder(1)] - defines the order in which the column appears
- [Sortable(true)] - if set to true, the column header will contain arrows for
ascending and descending sort.

You could drop the ViewModel and generate directly from an Entity class, provided you
can add attributes there. This was meant to be used with Entity Framework's CodeFirst,
where the Entity classes are automatically generated and it doesn't make sense to modify
them.

Refer to unit tests for more details on how to use it.
