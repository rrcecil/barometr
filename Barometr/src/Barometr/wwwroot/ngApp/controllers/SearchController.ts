
namespace Barometr.Controllers {

    export class SearchController {
        public result;
        public bars;
        public map;
        public mapDiv;
        public lat;
        public lng;
        public geocode;
        public zip;
        public mapOptions;

        constructor(public ngGPlacesAPI, public $http: ng.IHttpService, public $q: ng.IQService, public $scope: ng.IScope, public SearchService: Barometr.Services.SearchService, public $document: ng.IDocumentService) {
            this.displayMap();
        }

        public displayMap(location?) {
            this.mapOptions = {
                center: location || new google.maps.LatLng(29.790128, -95.402796),
                zoom: 13
            };
            this.result = document.getElementById('map');
            console.log(this.result);
            this.mapDiv = angular.element(this.result);
            this.map = new google.maps.Map(this.mapDiv[0], this.mapOptions);
        }

        public getLatLong(zipCode) {
            var deferred = this.$q.defer();
            var geocoder = new google.maps.Geocoder();

            //get zipcode from the view and pass it into geocode
            geocoder.geocode({ 'address': zipCode }, (res, stat) => {
                if (stat == google.maps.GeocoderStatus.OK) {
                    //assign response to lat and long
                    var result = res[0];
                    var geometry = result.geometry.location;
                    deferred.resolve(geometry);
                    return;
                } else {
                    deferred.reject("Couldn't find that address! Google Maps say: " + stat);
                }
            });
            return deferred.promise;
        }

        public getBars(zipCode) {
            
            this.SearchService.getBars(zipCode);
            console.log(this.SearchService.bars);
           
        }

        public addBar(bar) {
            bar.disabled = true;
            this.$http.post('api/userBars', bar).then((res) => res);
        }
    }
}






