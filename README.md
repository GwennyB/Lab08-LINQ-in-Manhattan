# Lab08-LINQ-in-Manhattan
CF 401 Lab-08: LINQ in Manhattan (using LINQ with JSON data; C#)

## Description
This application reads in a JSON data file, converts it to a queryable object, and queries the object for data. It completes the same search/filter twice, using 2 approaches:
  - First, it uses 3 separate methods to...
      ...return ALL values from a common object property (for many entries), and
      ...filter the returned values for blanks, and
      ...filter the returned, non-blank values for duplicates.
  - Second, it uses a single query to perform all 3 filters at once.
Results from each are displayed on console.

## How To Run
After compile, the application runs automatically and displays these results in the console window - no interaction required.
Steps:
  1. Read in data file.
  2. Convert to JSON object (JObject), which is queryable.
  3. Apply filters.
  4. Work with resulting data (in this case, dump into array and print to console).

## Visuals
First filter - return all (partial display - it's very long):
![](assets/filter-one-partial.PNG)

Second filter - remove blanks (partial again):
![](assets/filter-two-partial.PNG)

Third filter - remove duplicates:
![](assets/filter-three.PNG)

Alternate approach - single query (see that it matches the last filtered result exactly):
![](assets/all-in-one.PNG)
