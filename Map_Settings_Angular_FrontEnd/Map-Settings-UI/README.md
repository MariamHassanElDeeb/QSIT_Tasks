# Map Settings Task

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 15.1.2.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

## Usage

This web application, implemented using Angular and .NET Core technologies, allows you to manage and save configurations to a database. The application provides a single-page interface where you can input and retrieve configuration values. Here's how you can use the application:

1. Start the application by running the Angular front-end and .NET Core back-end servers.

2. Upon loading the application, you will see a single-page interface displaying various configuration options with an authentication.

3. Map Settings:
   - Cluster radius: Specify the radius for clustering map features.
   - Geo-Fencing: Define the parameters for creating geofences on the map.

4. Duplication Event Configuration:
   - Time Buffer: Set the time buffer to detect duplicate events.
   - Location Buffer: Define the spatial buffer for identifying duplicate events.

5. End Event Duration:
   - Duration: Specify the duration for an event to be considered completed.

6. Map Type:
   - Features: Choose the features to be displayed on the map.
   - Basemap: Select the basemap to use as a background.

7. Map Sub Type:
   - Dynamic, Cached, Imagery, Topographic

8. The back end of the application ensures secure Web APIs, protecting the data and allowing CRUD operations to be performed on the database.

9. The front end of the application includes the following features:
   - Map sub types are dynamically loaded based on the selected map type.
   - Validators are implemented for input fields, ensuring that the specified criteria.

10. Once you have configured the desired values, click the "Create" button to persist the configurations to the database.

## Demo Video
https://github.com/MariamHassanElDeeb/QSIT_Tasks/assets/126347210/141dbf8d-1a75-46c5-8a88-521a6e7491f3


