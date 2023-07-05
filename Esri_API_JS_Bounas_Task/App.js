// This code sets up a web map application using the ArcGIS JavaScript API.
// It creates a map view, adds layers, and provides functionality to search and display results.
//<------------The code------------>
// Require the necessary modules from the ArcGIS API
require([
  "esri/config",
  "esri/Map",
  "esri/views/MapView",
  "esri/layers/FeatureLayer",
  "esri/widgets/Home",
  "esri/widgets/Compass",
], function (esriConfig, Map, MapView, FeatureLayer, Home, Compass) {
  // Get the search button element from the DOM
  let searchbutton = document.getElementById("searchButton");
  // Set the ArcGIS API key for authentication
  esriConfig.apiKey =
    "AAPKba3bbc08b7114954a898ecb8fc1c333ef_SlqNNt0nffDLAVJcnh1UYtCmqXzJbqayzkZgfWwMSxKYG-HxWuY5_KApvcFiiI";
  // Create a new map object with a basemap layer
  const map = new Map({
    basemap: "arcgis-topographic", // Basemap layer service
  });
  // Create a new MapView object and set its properties
  const view = new MapView({
    map: map,
    spatialReference: { wkid: 3857 },
    center: [-88.186111, 41.748489], // Longitude, latitude
    zoom: 13, // Zoom level
    container: "baseMap", // Div element
  });
  // Create a Home widget for returning to the initial view
  let homeWidget = new Home({
    view: view,
  });
  // Create a Compass widget for rotating the view
  let compass = new Compass({
    view: view,
  });
  // Create a FeatureLayer for displaying damage assessment data
  const DamageAssessmentLayer = new FeatureLayer({
    url: "https://sampleserver6.arcgisonline.com/arcgis/rest/services/DamageAssessmentStatePlane/MapServer/0",
  });
  // Add the DamageAssessmentLayer to the map
  map.add(DamageAssessmentLayer);
  // Add event listener to the search button
  searchbutton.addEventListener("click", getUserQuery);
  // Function to get the minimum and maximum IDs from the DamageAssessmentLayer
  async function getMinAndMaxIds() {
    let query = DamageAssessmentLayer.createQuery();
    let result = await DamageAssessmentLayer.queryFeatures(query);
    let ids = result.features
      .map((feature) => {
        return feature.attributes.objectid;
      })
      .sort((a, b) => a - b);
    return [ids[0], ids[ids.length - 1]];
  }
  // Function to handle user query
  async function getUserQuery() {
    let [minid, maxid] = await getMinAndMaxIds();
    let userInput = document.getElementById("objectidInput").value;
    if (!(userInput >= minid && userInput <= maxid)) {
      alert(`Please enter a number between ${minid} and ${maxid}`);
    } else {
      let query = DamageAssessmentLayer.createQuery();
      query.where = `objectid<${userInput}`;
      query.returnGeometry = true;
      query.outFields = ["incidentnm", "firstname", "inspdate", "objectid"];
      query.outSpatialReference = view.spatialReference;
      DamageAssessmentLayer.queryFeatures(query).then(function (response) {
        let res = response.features;
        displayResults(res);
      });
    }
  }
  // Function to convert a timestamp to a readable date format
  function convertTimestampToDate(timestamp) {
    return new Date(timestamp).toLocaleString();
  }
  // Function to display the query results
  function displayResults(features) {
    console.log(features);
    // Convert query results to an array of objects compatible with the Kendo grid
    let gridData = features.map(function (feature) {
      return {
        incidentnm: feature.attributes.incidentnm,
        firstname: feature.attributes.firstname,
        inspdate: convertTimestampToDate(feature.attributes.inspdate),
        id: feature.attributes.objectid,
      };
    });

    // Initialize the Kendo grid
    $("#gridElement")
      .kendoGrid({
        dataSource: {
          data: gridData,
        },
        height: 400,
        sortable: true,
        columns: [
          { field: "incidentnm", title: "Incident Name" },
          { field: "firstname", title: "First Name" },
          { field: "inspdate", title: "Inspection Date" },
          {
            command: { text: "zoom", click: zoom },
            title: " ",
          },
        ],
      })
      .data("kendoGrid");
  }
  // Function to handle zooming to a feature
  function zoom(e) {
    e.preventDefault();
    let dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    let query = DamageAssessmentLayer.createQuery();
    DamageAssessmentLayer.queryFeatures(query).then(function (results) {
      const features = results.features;
      let geometries = features.find(
        (g) => g.attributes.objectid === dataItem.id
      ).geometry;
      view.goTo({
        target: geometries,
        zoom: 30,
      });
    });
  }
  // Add the homeWidget and compass to the view's UI
  view.ui.add(homeWidget, "top-left");
  view.ui.add(compass, "top-left");
});
