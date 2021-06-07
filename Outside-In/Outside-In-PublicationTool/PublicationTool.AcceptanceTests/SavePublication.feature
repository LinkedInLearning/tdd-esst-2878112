Feature: Save Publication
As a user I want to add mandatory publication data so that I can save a basic publication.

Scenario: Add valid publication
	Given is a publication with title "TDD Advanced" published on "22.06.2020"
	When the publication is stored
	Then a publication "TDD Advanced" published on "22.06.2020" can be found in the data base
	And no errors are reported

Scenario Outline: Missing data
	Given is a publication with title "<title>" published on "<publicationDate>"
	When the publication is stored
	Then a publication "<title>" can not be found in the data base
	And a missing "<propertyName>" is reported

	Examples: 
		| title   | publicationDate | propertyName |
		|         | 22.06.2020      | title        |
		| "Title" |                 | date         |

Scenario Outline: wrong title
	Given is a publication with title "<title>" published on "22.06.2020"
	When the publication is stored
	Then a publication "<title>" can not be found in the data base
	And it is reported that the title is to "<criteria>"

	Examples: 
		| title                                                                                                                      | criteria |
		| a                                                                                                                          | short    |
		| aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | long     |
