# Mapping Application with ArcGIS API for JavaScript

This repository contains a simple mapping application implemented using the ArcGIS API for JavaScript. The application allows users to search for buildings with an Object ID less than a specified number and displays the results in a Kendo grid. Users can also click on a button in the grid to zoom to the selected building on the map.

## Requirements

To run the application, you need the following:

- ArcGIS API for JavaScript
- ArcGIS API key for authentication
- Web server to host the application

## Installation
1. Clone the repository to your local machine: git clone [https://github.com/yourusername/your-repository.git](https://github.com/MariamHassanElDeeb/QSIT_Tasks.git)

2. Set up a web server to host the application files. You can use any web server of your choice (e.g., Apache, Nginx).

3. Obtain an ArcGIS API key from the ArcGIS Developer website (https://developers.arcgis.com) by creating an account and generating a key.

4. Open the `index.html` file in a text editor and replace the value of `esriConfig.apiKey` with your ArcGIS API key:
`esriConfig.apiKey = "YOUR_API_KEY";`

6. Save the index.html file.

7. Start the web server and access the application through your web browser.

##Usage
Upon loading the application, you will see a map with a search input field and a "Search" button.

Enter a number in the search input field representing the maximum Object ID you want to search for.

Click the "Search" button to initiate the search.

The application will query the feature layer with the specified criteria and display the results in a Kendo grid below the map. The grid will show the columns "Incident Name," "First Name," and "Inspection Date." The "Inspection Date" will be in a readable date format.

Each row in the grid will have a "zoom" button. Clicking on this button will zoom the map to the selected building.

